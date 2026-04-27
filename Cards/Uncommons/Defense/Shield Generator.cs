using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards;
using Liberate_the_Spire.Liberate_the_SpireCode.Character;
using Liberate_the_Spire.Liberate_the_SpireCode.Extensions;
using Liberate_the_Spire.liberate_the_SpireCode.Powers;
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
public class Shield_Generator_Pack() : Liberate_the_SpireCard(2, CardType.Power, CardRarity.Uncommon, TargetType.Self)
{   
    public override bool GainsBlock => true;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(2,ValueProp.Move), new PowerVar<BubblePower>(4)];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => new List<CardKeyword> 
        {Character.Liberate_the_Spire.HelldiverKeywords.Recharge};
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Shield_Generator_Pack pack = this;
        Decimal num = await CreatureCmd.GainBlock(pack.Owner.Creature, pack.DynamicVars.Block, play);
        
        BubblePower? power = await PowerCmd.Apply<BubblePower>(choiceContext,pack.Owner.Creature, pack.DynamicVars.Power<BubblePower>().BaseValue, pack.Owner.Creature,  pack);
        
    }
    
    protected override void OnUpgrade()
    {
        this.DynamicVars.Block.UpgradeValueBy(2);
        this.DynamicVars.Power<BubblePower>().UpgradeValueBy(1);
    }
    
}