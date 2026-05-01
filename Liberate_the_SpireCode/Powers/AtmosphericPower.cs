using BaseLib.Abstracts;
using BaseLib.Hooks;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Powers;


public class AtmosphericPower : CustomPowerModel
{
  public override PowerType Type => PowerType.Buff;

  public override PowerStackType StackType => PowerStackType.Counter;
  
  public override string CustomPackedIconPath => 
    System.IO.Path.Join(MainFile.ResPath, "images", "powers", "atmospheric_power.png");
    
  public override string CustomBigIconPath => 
    System.IO.Path.Join(MainFile.ResPath, "images", "powers", "atmospheric_power.png");

  private decimal attack_boost_per_stack = (decimal)(1.0 / 100.0);

  public override Decimal ModifyDamageMultiplicative(Creature? target, Decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
  {
    //if (dealer != this.Owner || !props.IsPoweredAttack())
    if (dealer != this.Owner)
      return 1M;
    
    if(cardSource == null || !cardSource.Keywords.Contains(Character.Liberate_the_Spire.HelldiverKeywords.Artillery))
      return 1M;

    if (target == cardSource.Owner.Creature)
      return 1M;
    
    Decimal percent_increase = attack_boost_per_stack;
    Decimal percent_total = 1 + percent_increase * this.Amount;
    return percent_total;
  }
}