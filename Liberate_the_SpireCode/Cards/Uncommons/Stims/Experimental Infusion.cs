using BaseLib.Extensions;
using BaseLib.Utils;
using Liberate_the_Spire.Liberate_the_SpireCode.Character;
using Liberate_the_Spire.Liberate_the_SpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Cards.Uncommons.Stims;


[Pool(typeof(Liberate_the_SpireCardPool))]
public class Experimental_Infusion() : Liberate_the_SpireCard(1, CardType.Power, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<InfusionPower>(1)];
    
        
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Experimental_Infusion stim = this;
        
        InfusionPower? power = await PowerCmd.Apply<InfusionPower>
            (choiceContext,stim.Owner.Creature, stim.DynamicVars.Power<InfusionPower>().BaseValue, stim.Owner.Creature, stim);
    }
    
    protected override void OnUpgrade()
    {
        this.EnergyCost.UpgradeBy(-1);
    }
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => 
        [(HoverTipFactory.FromPower<PlatingPower>())];
}