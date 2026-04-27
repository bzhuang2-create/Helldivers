using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Cards.Uncommons.Bullet_Hose;


public class Ammunition_Redesign() : Liberate_the_SpireCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new PowerVar<StrengthPower>(2M)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        Ammunition_Redesign cardSource = this;
        NPowerUpVfx.CreateNormal(cardSource.Owner.Creature);
        StrengthPower? strengthPower = await PowerCmd.Apply<StrengthPower>
            (choiceContext, cardSource.Owner.Creature, cardSource.DynamicVars.Power<StrengthPower>().BaseValue, cardSource.Owner.Creature, (CardModel) cardSource);
        
        cardSource.EnergyCost.UpgradeBy(1);
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Strength.UpgradeValueBy(1);
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<StrengthPower>()];
}