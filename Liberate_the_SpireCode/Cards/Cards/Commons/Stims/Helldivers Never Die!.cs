using BaseLib.Utils;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards.Starter_Deck;
using Liberate_the_Spire.Liberate_the_SpireCode.Character;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Cards.Commons.Stims;



[Pool(typeof(Liberate_the_SpireCardPool))]
public class Helldivers_Never_Die() : Liberate_the_SpireCard(1, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new HealVar(5), new CardsVar(2)];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => new List<CardKeyword> 
        {CardKeyword.Exhaust};
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Helldivers_Never_Die card = this;
        
        await CreatureCmd.Heal(card.Owner.Creature, card.DynamicVars.Heal.BaseValue);
        
        for (int i = 0; i < card.DynamicVars.Cards.BaseValue; i++)
        {
            ArgumentNullException.ThrowIfNull(card.CombatState, "card.CombatState");
            
            CardModel stim = card.CombatState.CreateCard<Stim>(card.Owner);
            
            if (card.IsUpgraded)
            {
                CardCmd.Upgrade(stim);
            }
            
            CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardToCombat(stim, PileType.Draw, card.Owner, CardPilePosition.Random));
        }
    }
    
    protected override void OnUpgrade()
    {
        //this.DynamicVars.Energy.UpgradeValueBy(-1);
        //this.ExtraHoverTips.Append(HoverTipFactory.FromCard(Stim));
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        (IEnumerable<IHoverTip>) new List<IHoverTip> {(HoverTipFactory.FromCard(ModelDb.Card<Stim>(), this.IsUpgraded))};
}