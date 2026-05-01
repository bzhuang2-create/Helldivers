using BaseLib.Utils;
using Liberate_the_Spire.Liberate_the_SpireCode.Character;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Cards.Rares.Stims;


[Pool(typeof(Liberate_the_SpireCardPool))]
public class Stim_Pistol() : Liberate_the_SpireCard(0, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    protected override HashSet<CardTag> CanonicalTags => 
        new HashSet<CardTag>() {Character.Liberate_the_Spire.HelldiverTags.Stim};
    
    protected override IEnumerable<DynamicVar> CanonicalVars => 
        [new HealVar(2), new PowerVar<RegenPower>(1), new CardsVar(1)];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => 
        [CardKeyword.Exhaust];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Stim_Pistol stim = this;
        
        await CreatureCmd.TriggerAnim(stim.Owner.Creature, "Cast", stim.Owner.Character.CastAnimDelay);
        await CreatureCmd.Heal(stim.Owner.Creature, stim.DynamicVars.Heal.BaseValue);
        await PowerCmd.Apply<RegenPower>(choiceContext, stim.Owner.Creature, stim.DynamicVars["RegenPower"].BaseValue, stim.Owner.Creature, null);
        await CardPileCmd.Draw(choiceContext, stim.DynamicVars.Cards.BaseValue, stim.Owner);
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Heal.UpgradeValueBy(1);
        this.DynamicVars.Cards.UpgradeValueBy(1);
    }
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => 
        [(HoverTipFactory.FromPower<RegenPower>())];
}

