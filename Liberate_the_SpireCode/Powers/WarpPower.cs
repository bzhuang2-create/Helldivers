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

#nullable enable
namespace Liberate_the_Spire.liberate_the_SpireCode.Powers;

public sealed class Warp_Power : CustomPowerModel
{
  public override PowerType Type => PowerType.Debuff;

  public override PowerStackType StackType => PowerStackType.Single;

  protected override IEnumerable<DynamicVar> CanonicalVars =>
    new List<DynamicVar> { (new DamageVar("SelfDamage", 5M, ValueProp.Unblockable | ValueProp.Unpowered)) };

  
  public override string CustomPackedIconPath => 
    System.IO.Path.Join(MainFile.ResPath, "images", "powers", "warp_power.png");
    
  public override string CustomBigIconPath => 
    System.IO.Path.Join(MainFile.ResPath, "images", "powers", "warp_power.png");
  
  
  public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
  {
    Warp_Power warp = this;

    if (side != CombatSide.Player)
    {
      return;
    }
    
    NCombatRoom? instance = NCombatRoom.Instance;
    if (instance != null)
      instance.CombatVfxContainer.AddChildSafely((Node?)NFireSmokePuffVfx.Create(warp.Owner));
    await Cmd.CustomScaledWait(0.2f, 0.4f);
    DamageVar dynamicVar = (DamageVar)warp.DynamicVars["SelfDamage"];

    IEnumerable<DamageResult> damageResults =
      await CreatureCmd.Damage(choiceContext, warp.Owner, dynamicVar.BaseValue, dynamicVar.Props, warp.Owner,
        (CardModel?)null);
    
    ArgumentNullException.ThrowIfNull(warp.Owner.Player, "No player?");
    CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardToCombat(warp.CombatState.CreateCard<Wound>(warp.Owner.Player), PileType.Draw, true));
    
    //IncrementSelfDamage();
  }

  protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    (IEnumerable<IHoverTip>) new List<IHoverTip> {(HoverTipFactory.FromCard<Wound>())};

  public void IncrementSelfDamage()
  {
    this.AssertMutable();
    ++this.DynamicVars["SelfDamage"].BaseValue;
  }
  
}
