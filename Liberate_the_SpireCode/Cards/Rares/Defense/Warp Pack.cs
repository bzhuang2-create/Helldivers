using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards;
using Liberate_the_Spire.Liberate_the_SpireCode.Character;
using Liberate_the_Spire.Liberate_the_SpireCode.Extensions;
using Liberate_the_Spire.liberate_the_SpireCode.Powers;
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

namespace Liberate_the_Spire.Liberate_the_SpireCode.Cards.Rares.Defense;



[Pool(typeof(Liberate_the_SpireCardPool))]
public class Warp_Pack() : Liberate_the_SpireCard(3, CardType.Power, CardRarity.Rare, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<IntangiblePower>(2)];

    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Warp_Pack warp = this;
        await CreatureCmd.TriggerAnim(warp.Owner.Creature, "Cast", warp.Owner.Character.CastAnimDelay);
        IntangiblePower? intangiblePower = await PowerCmd.Apply<IntangiblePower>
            (choiceContext, warp.Owner.Creature, warp.DynamicVars.Power<IntangiblePower>().BaseValue, warp.Owner.Creature, (CardModel) warp);
        
        WarpPower? power = await PowerCmd.Apply<WarpPower>
            (choiceContext, warp.Owner.Creature, 1, warp.Owner.Creature,  warp);
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        (IEnumerable<IHoverTip>)new List<IHoverTip> {(HoverTipFactory.FromPower<IntangiblePower>()), (HoverTipFactory.FromCard<Wound>()) };


    protected override void OnUpgrade()
    {
        this.EnergyCost.UpgradeBy(-1);
    }
    
}