using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using Liberate_the_Spire.Liberate_the_SpireCode.Character;
using Liberate_the_Spire.Liberate_the_SpireCode.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Cards.Uncommons.FireGas;


[Pool(typeof(Liberate_the_SpireCardPool))]
public class Re_Educator() : Liberate_the_SpireCard(0, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<PoisonPower>(3), new PowerVar<AcidPower>(1)];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => new List<CardKeyword> 
        {CardKeyword.Exhaust, Character.Liberate_the_Spire.HelldiverKeywords.Resupply};
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Re_Educator card = this;
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        ArgumentNullException.ThrowIfNull(CombatState, "cardPlay.Target");
        ArgumentNullException.ThrowIfNull(card.CombatState, "Grenade combatState");
        await CreatureCmd.TriggerAnim(card.Owner.Creature, "Cast", card.Owner.Character.CastAnimDelay);
        
        //await Cmd.CustomScaledWait(0.2f, 0.4f);
        
        
        PoisonPower? poisonPower = await PowerCmd.Apply<PoisonPower>
            (choiceContext, play.Target, card.DynamicVars.Poison.BaseValue, card.Owner.Creature, (CardModel) card);
        
        AcidPower? acidPower = await PowerCmd.Apply<AcidPower>
            (choiceContext, play.Target, card.DynamicVars.Power<AcidPower>().BaseValue, card.Owner.Creature, (CardModel) card);
    }
    
    protected override void OnUpgrade()
    {
        this.DynamicVars.Poison.UpgradeValueBy(1M);
        this.DynamicVars.Power<AcidPower>().UpgradeValueBy(1M);
        this.AddKeyword(CardKeyword.Retain);
    }
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => 
        [(HoverTipFactory.FromPower<PoisonPower>()), HoverTipFactory.FromPower<AcidPower>()];
}