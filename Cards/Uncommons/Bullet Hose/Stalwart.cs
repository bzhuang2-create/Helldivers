using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Cards.Uncommons.Bullet_Hose;


public class Stalwart() : Liberate_the_SpireCard(2, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(2, ValueProp.Move), new RepeatVar(6)];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => new List<CardKeyword> {CardKeyword.Exhaust, Character.Liberate_the_Spire.HelldiverKeywords.Resupply};


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Stalwart mg = this;
        //ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        ArgumentNullException.ThrowIfNull(CombatState, "CombatState");
        
        AttackCommand attackCommand = await DamageCmd.Attack(mg.DynamicVars.Damage.BaseValue).FromCard(mg).
            WithHitCount(mg.DynamicVars.Repeat.IntValue).TargetingAllOpponents(CombatState).WithHitFx("vfx/vfx_attack_blunt", tmpSfx: "blunt_attack.mp3").Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Repeat.UpgradeValueBy(1);
    }
}