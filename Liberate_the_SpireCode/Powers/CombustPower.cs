using BaseLib.Abstracts;
using Liberate_the_Spire.liberate_the_SpireCode.Powers;
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



public sealed class CombustPower() : CustomPowerModel
{
    public override string CustomPackedIconPath => 
        System.IO.Path.Join(MainFile.ResPath, "images", "powers", "combust_power.png");
    
    public override string CustomBigIconPath => 
        System.IO.Path.Join(MainFile.ResPath, "images", "powers", "combust_power.png");
    
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        [HoverTipFactory.FromPower<BurnPower>()];

    public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature? applier,
        CardModel? cardSource)
    {
        CombustPower acid = this;
        if (!(power is BurnPower) || cardSource == null)
        {
            return;
        }
        
        BurnPower? poisonPower = await PowerCmd.Apply<BurnPower>(choiceContext, acid.Owner, (Decimal) acid.Amount, null, null);
    }
    
    
    
    public override async Task AfterSideTurnStartLate(CombatSide side, ICombatState combatState)
    {
        CombustPower power = this;
        if (side != power.Owner.Side)
            return;
        int iterations = combatState.CreaturesOnCurrentSide.Count(creature => creature.HasPower(power.Id));
        
        for (int i = 0; i < iterations; ++i)
        {
            
            if (power.Owner.IsAlive)
                await PowerCmd.Decrement((PowerModel) power);
            else
                await Cmd.CustomScaledWait(0.1f, 0.25f);
        }
    }
}

