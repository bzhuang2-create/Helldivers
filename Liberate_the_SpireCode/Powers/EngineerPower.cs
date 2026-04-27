// Decompiled with JetBrains decompiler
// Type: MegaCrit.Sts2.Core.Models.Powers.OutbreakPower
// Assembly: sts2, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 26D08E84-7ECC-4355-92BF-3038BC49E80C
// Assembly location: c:\program files (x86)\steam\steamapps\common\Slay the Spire 2\data_sts2_windows_x86_64\sts2.dll

using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Models;

#nullable enable
namespace Liberate_the_Spire.Liberate_the_SpireCode.Powers;




public sealed class EngineerPower : CustomPowerModel
{
  public const int grenadeThreshold = 2;

  public override PowerType Type => PowerType.Buff;

  public override PowerStackType StackType => PowerStackType.Counter;

  public override int DisplayAmount => this.GetInternalData<EngineerPower.Data>().timesPlayed;

  protected override IEnumerable<DynamicVar> CanonicalVars => [new RepeatVar(3)];


  protected override object InitInternalData() => new EngineerPower.Data();

  //protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<EngineerPower>()];

  public override string CustomPackedIconPath => 
    System.IO.Path.Join(MainFile.ResPath, "images", "powers", "engineer_power.png");
    
  public override string CustomBigIconPath => 
    System.IO.Path.Join(MainFile.ResPath, "images", "powers", "engineer_power.png");
  
  public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
  {
    EngineerPower Epower = this;
    EngineerPower.Data? data;
    if (cardPlay.Card.Owner.Creature != Epower.Owner)
      data = null;

    else if (!(cardPlay.Card.Keywords.Contains(Liberate_the_Spire.Liberate_the_SpireCode.Character.Liberate_the_Spire.HelldiverKeywords.Grenade)))
    {
      data = null;
    }
    else
    {
      data = Epower.GetInternalData<EngineerPower.Data>();
      ++data.timesPlayed;
      if (data.timesPlayed >= 2)
      {
        Epower.InvokeDisplayAmountChanged();
        //Epower.Flash();
        
        ArgumentNullException.ThrowIfNull(Epower.Owner.Player, "Player null");
        
        IEnumerable<CardModel> cardModels = await CardPileCmd.Draw(choiceContext, Epower.Amount, Epower.Owner.Player);
        
        data.timesPlayed -= 2;
      }
      Epower.InvokeDisplayAmountChanged();
      data = null;
    }
  }

  private class Data
  {
    public int timesPlayed;
  }
}
