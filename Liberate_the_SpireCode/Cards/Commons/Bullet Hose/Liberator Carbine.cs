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
public class Liberator_Carbine() : Liberate_the_SpireCard(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
{   
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(2, ValueProp.Move), new CardsVar(1), new RepeatVar(3)];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => new List<CardKeyword> {CardKeyword.Exhaust, Character.Liberate_the_Spire.HelldiverKeywords.Resupply};
    
    //protected override IEnumerable<CardRarityOddsType> => RegularEncounter;
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Liberator_Carbine lib = this;
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        AttackCommand attackCommand = await DamageCmd.Attack(lib.DynamicVars.Damage.BaseValue).FromCard(lib).
            WithHitCount(lib.DynamicVars.Repeat.IntValue).Targeting(play.Target).WithHitFx("vfx/vfx_attack_blunt", tmpSfx: "blunt_attack.mp3").Execute(choiceContext);
        IEnumerable<CardModel> cardModels = await CardPileCmd.Draw(choiceContext, lib.DynamicVars.Cards.BaseValue, lib.Owner);
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Damage.UpgradeValueBy(1M);
        this.DynamicVars.Cards.UpgradeValueBy(1M);
    }
}