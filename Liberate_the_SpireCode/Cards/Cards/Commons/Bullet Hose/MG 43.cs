using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards;
using Liberate_the_Spire.Liberate_the_SpireCode.Character;
using Liberate_the_Spire.Liberate_the_SpireCode.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.ValueProps;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Cards.Commons.Bullet_Hose;


[Pool(typeof(Liberate_the_SpireCardPool))]
public class MG_43() : Liberate_the_SpireCard(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new DamageVar(4, ValueProp.Move), new RepeatVar(2), new PowerVar<WeakPower>(1)];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => new List<CardKeyword> {CardKeyword.Exhaust, Character.Liberate_the_Spire.HelldiverKeywords.Resupply};

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        MG_43 mg = this;
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        AttackCommand attackCommand = await DamageCmd.Attack(mg.DynamicVars.Damage.BaseValue).FromCard(mg).
            WithHitCount(mg.DynamicVars.Repeat.IntValue).Targeting(play.Target).WithHitFx("vfx/vfx_attack_blunt", tmpSfx: "blunt_attack.mp3").Execute(choiceContext);

        WeakPower? weak = await PowerCmd.Apply<WeakPower>(choiceContext, play.Target, mg.DynamicVars.Power<WeakPower>().BaseValue, mg.Owner.Creature, mg);
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Damage.UpgradeValueBy(1M);
    }
}