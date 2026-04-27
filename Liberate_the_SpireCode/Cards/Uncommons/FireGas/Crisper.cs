using BaseLib.Extensions;
using BaseLib.Utils;
using Liberate_the_Spire.Liberate_the_SpireCode.Character;
using Liberate_the_Spire.liberate_the_SpireCode.Powers;
using Liberate_the_Spire.Liberate_the_SpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Cards.Uncommons.FireGas;


[Pool(typeof(Liberate_the_SpireCardPool))]
public class Crisper() : Liberate_the_SpireCard(0, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<BurnPower>(2), new PowerVar<CombustPower>(1)];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => new List<CardKeyword> 
        {CardKeyword.Exhaust, Character.Liberate_the_Spire.HelldiverKeywords.Resupply};
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Crisper card = this;
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        ArgumentNullException.ThrowIfNull(CombatState, "cardPlay.Target");
        ArgumentNullException.ThrowIfNull(card.CombatState, "Grenade combatState");
        await CreatureCmd.TriggerAnim(card.Owner.Creature, "Cast", card.Owner.Character.CastAnimDelay);

        BurnPower? poisonPower = await PowerCmd.Apply<BurnPower>
            (choiceContext, play.Target, card.DynamicVars.Power<BurnPower>().BaseValue, card.Owner.Creature, (CardModel) card);
        
        CombustPower? acidPower = await PowerCmd.Apply<CombustPower>
            (choiceContext, play.Target, card.DynamicVars.Power<CombustPower>().BaseValue, card.Owner.Creature, (CardModel) card);
    }
    
    protected override void OnUpgrade()
    {
        this.DynamicVars.Power<BurnPower>().UpgradeValueBy(1M);
        this.DynamicVars.Power<CombustPower>().UpgradeValueBy(1M);
        this.AddKeyword(CardKeyword.Retain);
    }
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => 
        [(HoverTipFactory.FromPower<BurnPower>()), HoverTipFactory.FromPower<CombustPower>()];
}


