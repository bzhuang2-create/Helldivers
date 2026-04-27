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


public sealed class BubblePower : CustomPowerModel
{
  public override PowerType Type => PowerType.Buff;

  public override PowerStackType StackType => PowerStackType.Counter;
  
  public override string CustomPackedIconPath => 
    System.IO.Path.Join(MainFile.ResPath, "images", "powers", "bubble_power.png");
    
  public override string CustomBigIconPath => 
    System.IO.Path.Join(MainFile.ResPath, "images", "powers", "bubble_power.png");
  
  
  public override async Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
  {
    BubblePower power = this;
    if (side != power.Owner.Side)
      return;
    power.Flash();
    Decimal num = await CreatureCmd.GainBlock(power.Owner, (Decimal) power.Amount, ValueProp.Unpowered, null);
  }

  protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    (IEnumerable<IHoverTip>) new List<IHoverTip> {(HoverTipFactory.FromCard<Wound>())};


  
}
