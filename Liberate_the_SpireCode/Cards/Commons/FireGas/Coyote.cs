using BaseLib.Extensions;
using BaseLib.Utils;
using Liberate_the_Spire.Liberate_the_SpireCode.Character;
using Liberate_the_Spire.liberate_the_SpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Cards.Commons.FireGas;



[Pool(typeof(Liberate_the_SpireCardPool))]
public class Coyote() : Liberate_the_SpireCard(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new DamageVar(4, ValueProp.Move), new RepeatVar(2), new PowerVar<BurnPower>(1)];

    public override IEnumerable<CardKeyword> CanonicalKeywords =>
        [CardKeyword.Exhaust, Character.Liberate_the_Spire.HelldiverKeywords.Resupply];

    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Coyote card = this;
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        AttackCommand attackCommand = await DamageCmd.Attack(card.DynamicVars.Damage.BaseValue).FromCard(card).
            WithHitCount(card.DynamicVars.Repeat.IntValue).Targeting(play.Target).WithHitFx("vfx/vfx_attack_blunt", tmpSfx: "blunt_attack.mp3").Execute(choiceContext);

        for (int i = 0; i < card.DynamicVars.Repeat.IntValue; i++)
        {
            BurnPower? burn = await PowerCmd.Apply<BurnPower>(choiceContext, play.Target, card.DynamicVars.Power<BurnPower>().BaseValue, card.Owner.Creature, card);
        }
    }
    
    protected override void OnUpgrade()
    {
        this.DynamicVars.Power<BurnPower>().UpgradeValueBy(1M);
    }
}