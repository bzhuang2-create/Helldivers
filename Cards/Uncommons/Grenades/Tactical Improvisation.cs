using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using HarmonyLib;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards;
using Liberate_the_Spire.Liberate_the_SpireCode.Character;
using Liberate_the_Spire.Liberate_the_SpireCode.Extensions;
using Liberate_the_Spire.liberate_the_SpireCode.Powers;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.ValueProps;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Cards.UnCommons.Grenades;



[Pool(typeof(Liberate_the_SpireCardPool))]
public class Tactical_Improvisation() : Liberate_the_SpireCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{   
    public override bool GainsBlock => true;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(6,  ValueProp.Move)];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Tactical_Improvisation tac = this;
        Decimal num = await CreatureCmd.GainBlock(tac.Owner.Creature, tac.DynamicVars.Block, play);
        
        CardSelectorPrefs prefs = new CardSelectorPrefs(tac.SelectionScreenPrompt, 1);
        CardModel? card1 = (await CardSelectCmd.FromHand(choiceContext, tac.Owner, prefs, (Func<CardModel, bool>) 
            (card => !card.Keywords.Contains(Character.Liberate_the_Spire.HelldiverKeywords.Grenade)), (AbstractModel) tac)).FirstOrDefault<CardModel>();
        if (card1 == null)
            return;
        
        card1.AddKeyword(Character.Liberate_the_Spire.HelldiverKeywords.Grenade);
        card1.AddKeyword(CardKeyword.Exhaust);

        //card1.CanonicalKeywords.AddItem(Character.Liberate_the_Spire.HelldiverKeywords.Grenade);
        
        if (card1.Keywords.Contains(Character.Liberate_the_Spire.HelldiverKeywords.Resupply))
        {
            card1.RemoveKeyword(Character.Liberate_the_Spire.HelldiverKeywords.Resupply);
        }
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Block.UpgradeValueBy(3);
    }
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => 
        new List<IHoverTip> {HoverTipFactory.FromKeyword((CardKeyword.Exhaust)), HoverTipFactory.FromKeyword(Character.Liberate_the_Spire.HelldiverKeywords.Resupply)};
}