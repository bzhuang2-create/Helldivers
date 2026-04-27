using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards.Rares.Artillery.Shells;
using Liberate_the_Spire.Liberate_the_SpireCode.Character;
using Liberate_the_Spire.Liberate_the_SpireCode.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Cards.Rares.Artillery;


[Pool(typeof(Liberate_the_SpireCardPool))]
public class Seaf_Artillery() : Liberate_the_SpireCard(2, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    public override IEnumerable<CardKeyword> CanonicalKeywords => new List<CardKeyword> 
        {CardKeyword.Exhaust};
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Seaf_Artillery SEAF = this;
        for (int i = 0; i < 5; i++)
        {
            CardModel? card = CardFactory.GetDistinctForCombat(SEAF.Owner, [ModelDb.Card<Mini_Nuke>(), ModelDb.Card<High_Yield_Explosive>(), ModelDb.Card<Explosive>(), ModelDb.Card<Napalm>(), ModelDb.Card<Gas>(), 
                    ModelDb.Card<Static_Field>(), ModelDb.Card<Smoke>()], 
                1, SEAF.Owner.RunState.Rng.CombatCardGeneration).FirstOrDefault<CardModel>();
            if (card == null)
                return;
            
            if (SEAF.IsUpgraded)
            {
                CardCmd.Upgrade(card);
            }
            
            CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Draw, SEAF.Owner, CardPilePosition.Random));
        }
    }
}