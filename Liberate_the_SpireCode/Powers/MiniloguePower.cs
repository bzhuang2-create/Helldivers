using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Powers;


public sealed class MiniloguePower() : CustomPowerModel
{
    public override string CustomPackedIconPath => 
        System.IO.Path.Join(MainFile.ResPath, "images", "powers", "minilogue_power.png");
    
    public override string CustomBigIconPath => 
        System.IO.Path.Join(MainFile.ResPath, "images", "powers", "minilogue_power.png");
    
    public const string strengthAppliedKey = "StrengthApplied";

    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType
    {
        get
        {
            return this.DynamicVars["StrengthApplied"].IntValue != 0 ? PowerStackType.Counter : PowerStackType.None;
        }
    }

    public override int DisplayAmount => this.DynamicVars["StrengthApplied"].IntValue;

    public override bool IsInstanced => true;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        (DynamicVar)new PowerVar<StrengthPower>(1M),
        new("StrengthApplied", 0M)
    ];

    protected override object InitInternalData() => (object) new MiniloguePower.Data();

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [(HoverTipFactory.FromPower<StrengthPower>())];


    public override Task BeforeDamageReceived(PlayerChoiceContext choiceContext, Creature target, decimal amount, ValueProp props,
        Creature? dealer, CardModel? cardSource)
    {
          if(cardSource == null || cardSource.Owner.Creature != this.Owner)
                return Task.CompletedTask;
          this.GetInternalData<MiniloguePower.Data>().amountsForDamageInstance.Add(cardSource, this.DynamicVars.Strength.IntValue);
          return Task.CompletedTask;
    }

    public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props,
        Creature? dealer, CardModel? cardSource)
    {
          MiniloguePower power = this;
          int amount;
          if (cardSource == null || cardSource.Owner.Creature != this.Owner || !power.GetInternalData<MiniloguePower.Data>()
                .amountsForDamageInstance.Remove(cardSource, out amount))
            return;
          power.Flash();
          StrengthPower? strength = await PowerCmd.Apply<StrengthPower>(choiceContext, power.Owner, (Decimal)amount,
            power.Owner, null, true);
          power.DynamicVars["StrengthApplied"].BaseValue += power.DynamicVars.Strength.IntValue;
          power.InvokeDisplayAmountChanged();
          return;
    }

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        MiniloguePower power = this; 
        if (side != power.Owner.Side)
          return; 
        await PowerCmd.Remove((PowerModel) power); 
        StrengthPower? strengthPower = await PowerCmd.Apply<StrengthPower>(choiceContext, power.Owner, 
            -power.DynamicVars["StrengthApplied"].BaseValue, power.Owner,  null, true);
    }

    private class Data
    {
        public readonly Dictionary<CardModel, int> amountsForDamageInstance = new Dictionary<CardModel, int>();
    }
}