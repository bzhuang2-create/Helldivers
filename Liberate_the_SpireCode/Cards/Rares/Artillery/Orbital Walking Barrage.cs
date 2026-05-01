using BaseLib.Extensions;
using BaseLib.Utils;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards.Commons.Artillery;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards.Rares.Artillery.Shells;
using Liberate_the_Spire.Liberate_the_SpireCode.Character;
using Liberate_the_Spire.Liberate_the_SpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Cards.Rares.Artillery;

[Pool(typeof(Liberate_the_SpireCardPool))]
public class Orbital_Walking_Barrage() : Liberate_the_SpireCard(3, CardType.Power, CardRarity.Rare, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<WalkingPower>(1)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Orbital_Walking_Barrage orbital = this;
        
        WalkingPower? power = await PowerCmd.Apply<WalkingPower>(choiceContext, orbital.Owner.Creature, orbital.DynamicVars.Power<WalkingPower>().BaseValue, orbital.Owner.Creature, orbital);
    }

    protected override void OnUpgrade()
    {
        this.EnergyCost.UpgradeBy(-1);
    }
}
