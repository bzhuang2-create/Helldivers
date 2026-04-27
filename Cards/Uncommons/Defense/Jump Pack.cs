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
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.ValueProps;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Cards.Uncommons.Defense;


[Pool(typeof(Liberate_the_SpireCardPool))]
public class Jump_Pack() : Liberate_the_SpireCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{   
    public override bool GainsBlock => true;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(6,  ValueProp.Move), new CardsVar(2)];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => new List<CardKeyword> 
        {Character.Liberate_the_Spire.HelldiverKeywords.Recharge};

    private bool FirstTimeDrawnWithUnplayable = false;
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Jump_Pack pack = this;
        Decimal num = await CreatureCmd.GainBlock(pack.Owner.Creature, pack.DynamicVars.Block, play);
        
        IEnumerable<CardModel> cardModels = await CardPileCmd.Draw(choiceContext, pack.DynamicVars.Cards.BaseValue, pack.Owner);
        this.AddKeyword(CardKeyword.Unplayable);
        FirstTimeDrawnWithUnplayable = true;
    }
    
    public override Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
    {
        if (card != this)
            return Task.CompletedTask;

        if (!FirstTimeDrawnWithUnplayable)
        {
            card.RemoveKeyword(CardKeyword.Unplayable);
            return Task.CompletedTask;
            
        }else if (FirstTimeDrawnWithUnplayable)
        {
            FirstTimeDrawnWithUnplayable = false;
            card.AddKeyword(CardKeyword.Unplayable);
            
            //Yes, this is bootleg lmao
            return Task.CompletedTask;
        }

        //throw new ArgumentException("Bruh moment");
        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Block.UpgradeValueBy(4);
        this.DynamicVars.Cards.UpgradeValueBy(1);
    }
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        (IEnumerable<IHoverTip>)new List<IHoverTip> {(HoverTipFactory.FromKeyword(Character.Liberate_the_Spire.HelldiverKeywords.Recharge))};
}