using BaseLib.Extensions;
using BaseLib.Utils;
using Liberate_the_Spire.Liberate_the_SpireCode.Character;
using Liberate_the_Spire.liberate_the_SpireCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Cards.Uncommons.FireGas;



[Pool(typeof(Liberate_the_SpireCardPool))]
public class Pyromaniac() : Liberate_the_SpireCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => 
        [new PowerVar<StrengthPower>(1), new RepeatVar(5)];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords =>
        [CardKeyword.Exhaust];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        Pyromaniac pyro = this;
        ArgumentNullException.ThrowIfNull(CombatState, "combatState");
        ArgumentNullException.ThrowIfNull(pyro.CombatState, "Grenade combatState");
        
        int total_stack = (CombatState != null ? CombatState.Enemies.Where<Creature>
            (c => c.IsAlive).Sum((c => c.GetPowerAmount<BurnPower>())) : 0);
        
        int apply = total_stack / this.DynamicVars.Repeat.IntValue;

        StrengthPower? strength = await PowerCmd.Apply<StrengthPower>
            (choiceContext, pyro.Owner.Creature, apply, pyro.Owner.Creature, pyro);
    }
    
    protected override void OnUpgrade()
    {
        this.DynamicVars.Repeat.UpgradeValueBy(-1);
    }
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => 
        [(HoverTipFactory.FromPower<BurnPower>()), HoverTipFactory.FromPower<StrengthPower>()];
}