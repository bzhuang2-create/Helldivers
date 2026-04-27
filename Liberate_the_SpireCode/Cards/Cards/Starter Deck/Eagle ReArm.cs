using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards;
using Liberate_the_Spire.Liberate_the_SpireCode.Character;
using Liberate_the_Spire.Liberate_the_SpireCode.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Cards.Starter_Deck;


[Pool(typeof(Liberate_the_SpireCardPool))]
public class Eagle_Re_Arm() : Liberate_the_SpireCard(1, CardType.Skill, CardRarity.Basic, TargetType.None)
{   
    //public override IEnumerable<CardKeyword> CanonicalKeywords => new List<CardKeyword> {Character.Liberate_the_Spire.HelldiverKeywords.Resupply};
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Eagle_Re_Arm supply = this;
        IEnumerable<CardModel> list = (IEnumerable<CardModel>) PileType.Exhaust.GetPile(supply.Owner).Cards.Where
            <CardModel>((Func<CardModel, bool>) (c => c.CanonicalKeywords.Contains(Character.Liberate_the_Spire.HelldiverKeywords.ReArm))).ToList<CardModel>();
        foreach (CardModel card in list)
        {
            CardPileAddResult cardPileAddResult = await CardPileCmd.Add(card, PileType.Draw);
        }
        //await CardPileCmd.Shuffle(choiceContext, supply.Owner);
    }

    protected override void OnUpgrade()
    {
        this.EnergyCost.UpgradeBy(-1);
    }
}