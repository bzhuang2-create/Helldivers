using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards;
using Liberate_the_Spire.Liberate_the_SpireCode.Character;
using Liberate_the_Spire.Liberate_the_SpireCode.Extensions;
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
public class Lure_Mine() : Liberate_the_SpireCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<VulnerablePower>(2)];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => new List<CardKeyword> 
        {CardKeyword.Exhaust, Character.Liberate_the_Spire.HelldiverKeywords.Resupply, Character.Liberate_the_Spire.HelldiverKeywords.Grenade};
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Lure_Mine gren = this;
        //ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        ArgumentNullException.ThrowIfNull(gren.CombatState, "Combatstate");
        await CreatureCmd.TriggerAnim(gren.Owner.Creature, "Cast", gren.Owner.Character.CastAnimDelay);
        
        IReadOnlyList<VulnerablePower> vulnerablePowerList = await PowerCmd.Apply<VulnerablePower>(choiceContext,
            gren.CombatState.HittableEnemies, gren.DynamicVars.Vulnerable.BaseValue, gren.Owner.Creature, gren);
    }
    
    
    protected override void OnUpgrade()
    {
        this.DynamicVars.Vulnerable.UpgradeValueBy(1M);
    }
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => new List<IHoverTip> {(HoverTipFactory.FromPower<VulnerablePower>())};
}