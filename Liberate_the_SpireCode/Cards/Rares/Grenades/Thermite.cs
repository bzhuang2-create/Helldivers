using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using HarmonyLib;
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

namespace Liberate_the_Spire.Liberate_the_SpireCode.Cards.Rares.Grenades;




[Pool(typeof(Liberate_the_SpireCardPool))]
public class Thermite() : Liberate_the_SpireCard(1, CardType.Skill, CardRarity.Rare, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(15, ValueProp.Move), new PowerVar<VulnerablePower>(3), new PowerVar<BurnPower>(6)];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => new List<CardKeyword> 
        {CardKeyword.Exhaust, Character.Liberate_the_Spire.HelldiverKeywords.Grenade};
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Thermite gren = this;
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        ArgumentNullException.ThrowIfNull(gren.CombatState, "Combatstate");
        await CreatureCmd.TriggerAnim(gren.Owner.Creature, "Cast", gren.Owner.Character.CastAnimDelay);
        
        AttackCommand attackCommand = await DamageCmd.Attack(gren.DynamicVars.Damage.BaseValue).FromCard((CardModel) gren).
            WithHitCount(1).Targeting(play.Target).WithHitFx("vfx/vfx_attack_blunt", tmpSfx: "blunt_attack.mp3").Execute(choiceContext);
        
        VulnerablePower? vul = await PowerCmd.Apply<VulnerablePower>(choiceContext, play.Target, 
            gren.DynamicVars.Vulnerable.BaseValue, gren.Owner.Creature, (CardModel) gren);
        
        BurnPower? burn = await PowerCmd.Apply<BurnPower>(choiceContext, play.Target,
            gren.DynamicVars.Power<BurnPower>().BaseValue, play.Target, gren);
    }
    
    
    protected override void OnUpgrade()
    {
        this.AddKeyword(Character.Liberate_the_Spire.HelldiverKeywords.Resupply);
        this.ExtraHoverTips.AddItem(HoverTipFactory.FromKeyword(Character.Liberate_the_Spire.HelldiverKeywords.Resupply));
    }
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => new List<IHoverTip> {(HoverTipFactory.FromPower<VulnerablePower>()), HoverTipFactory.FromPower<BurnPower>()};
}