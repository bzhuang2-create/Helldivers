using BaseLib.Extensions;
using BaseLib.Utils;
using Liberate_the_Spire.Liberate_the_SpireCode.Character;
using Liberate_the_Spire.liberate_the_SpireCode.Powers;
using Liberate_the_Spire.Liberate_the_SpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Cards.Uncommons.Artillery;


[Pool(typeof(Liberate_the_SpireCardPool))]
public class Atmospheric_Monitoring() : Liberate_the_SpireCard(2, CardType.Power, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<AtmosphericPower>(10)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Atmospheric_Monitoring power = this;
        AtmosphericPower? atmosphericPower = await PowerCmd.Apply<AtmosphericPower>
            (choiceContext, power.Owner.Creature, power.DynamicVars.Power<AtmosphericPower>().BaseValue, power.Owner.Creature, power);
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Power<AtmosphericPower>().UpgradeValueBy(5);
    }
}
