using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards;
using Liberate_the_Spire.Liberate_the_SpireCode.Character;
using Liberate_the_Spire.Liberate_the_SpireCode.Extensions;
using Liberate_the_Spire.liberate_the_SpireCode.Powers;
using Liberate_the_Spire.Liberate_the_SpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Cards.Uncommons.Grenades;



[Pool(typeof(Liberate_the_SpireCardPool))]
public class Trench_Engineer() : Liberate_the_SpireCard(2, CardType.Power, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<EngineerPower>(1), new CardsVar(1)];

    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Trench_Engineer armor = this;
        await CreatureCmd.TriggerAnim(armor.Owner.Creature, "Cast", armor.Owner.Character.CastAnimDelay);
        
        EngineerPower? power = await PowerCmd.Apply<EngineerPower>
            (choiceContext, armor.Owner.Creature, armor.DynamicVars.Power<EngineerPower>().BaseValue, armor.Owner.Creature,  armor);
    }


    protected override void OnUpgrade()
    {
        this.EnergyCost.UpgradeBy(-1);
    }
    
}