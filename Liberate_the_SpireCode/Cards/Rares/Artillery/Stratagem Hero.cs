using BaseLib.Extensions;
using BaseLib.Utils;
using Liberate_the_Spire.Liberate_the_SpireCode.Character;
using Liberate_the_Spire.liberate_the_SpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Cards.Rares.Artillery;


[Pool(typeof(Liberate_the_SpireCardPool))]
public class Stratagem_Hero() : Liberate_the_SpireCard(0, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => 
        [new EnergyVar(3), new PowerVar<VulnerablePower>(3)];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords =>
        [CardKeyword.Exhaust];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Stratagem_Hero orbital = this;
        
        await PlayerCmd.GainEnergy(orbital.DynamicVars.Energy.IntValue, orbital.Owner);
        EnergyNextTurnPower? energyNextTurnPower = await PowerCmd.Apply<EnergyNextTurnPower>
            (choiceContext, orbital.Owner.Creature, orbital.DynamicVars.Energy.IntValue, orbital.Owner.Creature, orbital);

        VulnerablePower? vulnerable = await PowerCmd.Apply<VulnerablePower>
            (choiceContext, orbital.Owner.Creature, orbital.DynamicVars.Power<VulnerablePower>().BaseValue, orbital.Owner.Creature, orbital);
    }
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [EnergyHoverTip];
    
    protected override void OnUpgrade()
    {
        this.DynamicVars.Energy.UpgradeValueBy(1);
    }
}