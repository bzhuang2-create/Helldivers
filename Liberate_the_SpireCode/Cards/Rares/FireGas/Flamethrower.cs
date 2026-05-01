using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using Liberate_the_Spire.Liberate_the_SpireCode.Character;
using Liberate_the_Spire.liberate_the_SpireCode.Powers;
using Liberate_the_Spire.Liberate_the_SpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Cards.Rares.FireGas;


[Pool(typeof(Liberate_the_SpireCardPool))]
public class Flamethrower() : Liberate_the_SpireCard(2, CardType.Skill, CardRarity.Rare, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<BurnPower>(8), new BlockVar(0, ValueProp.Move)];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords =>
        [CardKeyword.Exhaust];

    public override bool GainsBlock => true;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Flamethrower flam = this;
        ArgumentNullException.ThrowIfNull(play.Target, "null target");
        ArgumentNullException.ThrowIfNull(CombatState, "combatState");
        ArgumentNullException.ThrowIfNull(flam.CombatState, "Grenade combatState");
        await CreatureCmd.TriggerAnim(flam.Owner.Creature, "Cast", flam.Owner.Character.CastAnimDelay);

        await Cmd.CustomScaledWait(0.2f, 0.4f);

        BurnPower? burnPower = await PowerCmd.Apply<BurnPower>
            (choiceContext, play.Target, flam.DynamicVars.Power<BurnPower>().BaseValue, flam.Owner.Creature, flam);

        Decimal block = await CreatureCmd.GainBlock
            (flam.Owner.Creature, play.Target.GetPowerAmount<BurnPower>(), flam.DynamicVars.Block.Props, play);
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Power<BurnPower>().UpgradeValueBy(4);
    }
    
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => 
        [(HoverTipFactory.FromPower<BurnPower>())];
}