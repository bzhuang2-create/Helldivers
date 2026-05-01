using Liberate_the_Spire.Liberate_the_SpireCode.Cards.Starter_Deck;
using Liberate_the_Spire.Liberate_the_SpireCode.Character;
using BaseLib.Utils;
using Liberate_the_Spire.Liberate_the_SpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;


namespace Liberate_the_Spire.Liberate_the_SpireCode.Cards.Rares.Stims;


[Pool(typeof(Liberate_the_SpireCardPool))]
public class For_Super_Earth___() : Liberate_the_SpireCard(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => new List<CardKeyword> {CardKeyword.Exhaust};

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        For_Super_Earth___ card = this;
        
        MiniloguePower? power = await PowerCmd.Apply<MiniloguePower>(choiceContext, card.Owner.Creature, 1, card.Owner.Creature, card);
    }
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        (IEnumerable<IHoverTip>) new List<IHoverTip> {(HoverTipFactory.FromPower<StrengthPower>())};

    protected override void OnUpgrade()
    {
        this.EnergyCost.UpgradeBy(-1);
    }
}