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

namespace Liberate_the_Spire.Liberate_the_SpireCode.Cards.Uncommons.Grenades;

    

[Pool(typeof(Liberate_the_SpireCardPool))]
public class Incendiary_Impact_Grenade() : Liberate_the_SpireCard(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(2, ValueProp.Move), new PowerVar<BurnPower>(3)];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => new List<CardKeyword> 
        {CardKeyword.Exhaust, Character.Liberate_the_Spire.HelldiverKeywords.Resupply, Character.Liberate_the_Spire.HelldiverKeywords.Grenade};
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Incendiary_Impact_Grenade gren = this;
        ArgumentNullException.ThrowIfNull(gren.CombatState, "CombatState null");
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        
        await CreatureCmd.TriggerAnim(gren.Owner.Creature, "Cast", gren.Owner.Character.CastAnimDelay);
        
        await Cmd.CustomScaledWait(0.2f, 0.4f);

        AttackCommand attackCommand = await DamageCmd.Attack(gren.DynamicVars.Damage.BaseValue).FromCard((CardModel) gren)
            .Targeting(play.Target).WithHitFx("vfx/vfx_attack_blunt", tmpSfx: "blunt_attack.mp3").Execute(choiceContext);
        
        BurnPower? burn = await PowerCmd.Apply<BurnPower>(choiceContext, play.Target,
            gren.DynamicVars.Power<BurnPower>().BaseValue, play.Target, gren);
    }
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => new List<IHoverTip> {(HoverTipFactory.FromPower<BurnPower>())};
    
    protected override void OnUpgrade()
    {
        this.DynamicVars.Power<BurnPower>().UpgradeValueBy(2M);
    }
}