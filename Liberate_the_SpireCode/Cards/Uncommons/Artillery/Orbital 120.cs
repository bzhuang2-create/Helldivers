using BaseLib.Utils;
using HarmonyLib;
using Liberate_the_Spire.Liberate_the_SpireCode.Character;
using Liberate_the_Spire.Liberate_the_SpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Cards.Uncommons.Artillery;

[Pool(typeof(Liberate_the_SpireCardPool))]
public class Orbital_120() : Liberate_the_SpireCard(2, CardType.Attack, CardRarity.Uncommon, TargetType.RandomEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(12, ValueProp.Move)];

    public override IEnumerable<CardKeyword> CanonicalKeywords =>
        [Character.Liberate_the_Spire.HelldiverKeywords.Artillery];

    private bool played = false;

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Orbital_120 orbital = this;
        ArgumentNullException.ThrowIfNull(orbital.CombatState, "CombatState null");
        
        AttackCommand attackCommand = await DamageCmd.Attack(orbital.DynamicVars.Damage.BaseValue).FromCard(orbital)
            .TargetingRandomOpponents(orbital.CombatState, true).WithHitFx("vfx/vfx_attack_blunt", tmpSfx: "blunt_attack.mp3").Execute(choiceContext);

        if (!played)
        {
            orbital.AddKeyword(Character.Liberate_the_Spire.HelldiverKeywords.Continuous);
            played = true;
        }
    }

    public override async Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
    {
        if (card != this)
        {
            return;
        }
        if (played)
        {
            await CardCmd.AutoPlay(choiceContext, card, null);
        }
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Damage.UpgradeValueBy(2);
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips => 
        [HoverTipFactory.FromKeyword(Character.Liberate_the_Spire.HelldiverKeywords.Continuous)];
}