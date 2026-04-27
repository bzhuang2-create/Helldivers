using BaseLib.Utils;
using Liberate_the_Spire.Liberate_the_SpireCode.Character;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Cards.Commons.Bullet_Hose;


public class Prone() : Liberate_the_SpireCard(1, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    public override bool GainsBlock => true;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(6,  ValueProp.Move), new PowerVar<StrengthPower>(2)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Prone card = this;
        Decimal block = await CreatureCmd.GainBlock(card.Owner.Creature, card.DynamicVars.Block, play);
        
        //ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        
        SetupStrikePower? setupStrikePower = await PowerCmd.Apply<SetupStrikePower>
            (choiceContext, card.Owner.Creature, card.DynamicVars.Strength.BaseValue, card.Owner.Creature, (CardModel) card);
    }
    
    protected override void OnUpgrade()
    {
        this.DynamicVars.Block.UpgradeValueBy(2M);
        this.DynamicVars.Strength.UpgradeValueBy(1M);
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [HoverTipFactory.FromPower<StrengthPower>()];
}