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
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Cards.Starter_Deck;


[Pool(typeof(Liberate_the_SpireCardPool))]
public class Stim() : Liberate_the_SpireCard(1, CardType.Skill, CardRarity.Basic, TargetType.Self)
{   
    protected override IEnumerable<DynamicVar> CanonicalVars => [new HealVar(5), new PowerVar<RegenPower>(2)];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => new List<CardKeyword> {CardKeyword.Exhaust, Character.Liberate_the_Spire.HelldiverKeywords.Resupply};
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Stim stim = this;
        //ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        await CreatureCmd.TriggerAnim(stim.Owner.Creature, "Cast", stim.Owner.Character.CastAnimDelay);
        await CreatureCmd.Heal(stim.Owner.Creature, stim.DynamicVars.Heal.BaseValue);
        await PowerCmd.Apply<RegenPower>(choiceContext, stim.Owner.Creature, stim.DynamicVars["RegenPower"].BaseValue, stim.Owner.Creature, null);
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Heal.UpgradeValueBy(2M);
        this.DynamicVars.Power<RegenPower>().UpgradeValueBy(1M);
    }
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => new List<IHoverTip> {(HoverTipFactory.FromPower<RegenPower>())};
}