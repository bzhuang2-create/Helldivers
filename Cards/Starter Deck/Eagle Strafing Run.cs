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
using MegaCrit.Sts2.Core.ValueProps;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Cards.Starter_Deck;




[Pool(typeof(Liberate_the_SpireCardPool))]
public class Eagle_Strafing_Run() : Liberate_the_SpireCard(2, CardType.Attack, CardRarity.Basic, TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(8, ValueProp.Move)];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => new List<CardKeyword> {CardKeyword.Exhaust, Character.Liberate_the_Spire.HelldiverKeywords.ReArm};
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Eagle_Strafing_Run lib = this;
        //ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        ArgumentNullException.ThrowIfNull(CombatState, "cardPlay.Target");
        AttackCommand attackCommand = await DamageCmd.Attack(lib.DynamicVars.Damage.BaseValue).FromCard((CardModel) lib).TargetingAllOpponents(CombatState)
            .WithHitFx("vfx/vfx_attack_blunt", tmpSfx: "blunt_attack.mp3").Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Damage.UpgradeValueBy(5M);
    }
}