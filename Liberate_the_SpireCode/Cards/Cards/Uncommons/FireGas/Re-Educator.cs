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
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<PoisonPower>(3)];
    
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
            (choiceContext, card.Owner.Creature, 1, card.Owner.Creature, (CardModel) card);
    }
    
    protected override void OnUpgrade()
    {
        this.DynamicVars.Poison.UpgradeValueBy(1M);
        this.AddKeyword(CardKeyword.Retain);
    }
}



public class Gas_Grenade() : Liberate_the_SpireCard(1, CardType.Attack, CardRarity.Common, TargetType.AllEnemies)
{
    


    private void SpawnVfx()
    {
        Node? combatVfxContainer = NCombatRoom.Instance?.CombatVfxContainer;
        if (combatVfxContainer == null)
            return;
        NSmokyVignetteVfx? child = NSmokyVignetteVfx.Create(new Color(0.8f, 0.8f, 0.3f, 0.66f), new Color(0.0f, 4f, 0.0f, 0.33f));
        combatVfxContainer.AddChildSafely((Node?) child);
        
        ArgumentNullException.ThrowIfNull(this.CombatState, "CombatState");
        
        foreach (Creature hittableEnemy in (IEnumerable<Creature>) this.CombatState.HittableEnemies)
            combatVfxContainer.AddChildSafely((Node?) NSmokePuffVfx.Create(hittableEnemy, NSmokePuffVfx.SmokePuffColor.Green));
    }
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => new List<IHoverTip> {(HoverTipFactory.FromPower<PoisonPower>())};
    

}