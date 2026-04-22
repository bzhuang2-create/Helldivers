// Decompiled with JetBrains decompiler
// Type: MegaCrit.Sts2.Core.Models.Powers.InfernoPower
// Assembly: sts2, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 623673A3-2F6A-4E15-A560-4F44F2297867
// Assembly location: c:\program files (x86)\steam\steamapps\common\Slay the Spire 2\data_sts2_windows_x86_64\sts2.dll

using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using Liberate_the_Spire.Liberate_the_SpireCode;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;

#nullable enable
namespace Liberate_the_Spire.liberate_the_SpireCode.Powers;


public sealed class BurnPower : CustomPowerModel
{
  public override PowerType Type => PowerType.Debuff;

  public override PowerStackType StackType => PowerStackType.Counter;
  
  public override string CustomPackedIconPath => 
    System.IO.Path.Join(MainFile.ResPath, "images", "powers", "burn_power.png");
    
  public override string CustomBigIconPath => 
    System.IO.Path.Join(MainFile.ResPath, "images", "powers", "burn_power.png");

  private decimal attack_penalty_per_stack = (decimal)(2.5 / 100);
  private int TriggerCount
  {
    get
    {
      return Math.Min(this.Amount, 1 + this.Owner.CombatState.GetOpponentsOf(this.Owner).Where<Creature>((Func<Creature, bool>) 
        (c => c.IsAlive)).Sum<Creature>((Func<Creature, int>) (a => a.GetPowerAmount<AccelerantPower>())));
    }
  }
  
  public override Decimal ModifyDamageMultiplicative(
    Creature? target,
    Decimal amount,
    ValueProp props,
    Creature? dealer,
    CardModel? cardSource)
  {
    if (dealer != this.Owner || !props.IsPoweredAttack())
      return 1M;
    Decimal percent_decrease = attack_penalty_per_stack;
    Decimal percent_left = 1 - percent_decrease * amount;
    return percent_left;
  }
  
  public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
  {
    BurnPower power = this;
    if (side != power.Owner.Side)
      return;
    int iterations = power.TriggerCount;
    for (int i = 0; i < iterations; ++i)
    {
      IEnumerable<DamageResult> damageResults = await CreatureCmd.Damage((PlayerChoiceContext) new ThrowingPlayerChoiceContext(), power.Owner, 
        (Decimal) power.Amount, ValueProp.Unblockable | ValueProp.Unpowered, (Creature?) null, (CardModel?) null);
      if (power.Owner.IsAlive)
        await PowerCmd.Decrement((PowerModel) power);
      else
        await Cmd.CustomScaledWait(0.1f, 0.25f);
    }
  }
}
