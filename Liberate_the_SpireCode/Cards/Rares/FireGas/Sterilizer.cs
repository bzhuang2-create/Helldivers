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
using MegaCrit.Sts2.Core.MonsterMoves.Intents;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Cards.Rares.FireGas;

[Pool(typeof(Liberate_the_SpireCardPool))]
public class Sterilizer() : Liberate_the_SpireCard(2, CardType.Skill, CardRarity.Rare, TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<PoisonPower>(5), new PowerVar<WeakPower>(3)];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords =>
        [CardKeyword.Exhaust];
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Sterilizer flam = this;
        //ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        ArgumentNullException.ThrowIfNull(CombatState, "combatState");
        ArgumentNullException.ThrowIfNull(flam.CombatState, "Grenade combatState");
        await CreatureCmd.TriggerAnim(flam.Owner.Creature, "Cast", flam.Owner.Character.CastAnimDelay);
        
        flam.SpawnVfx();
        await Cmd.CustomScaledWait(0.2f, 0.4f);
        
        Creature[] array = CombatState.HittableEnemies.ToArray<Creature>();
        
        foreach (Creature hittableEnemy in flam.CombatState.HittableEnemies)
        {
            PoisonPower? poisonPower = await PowerCmd.Apply<PoisonPower>
                (choiceContext, hittableEnemy, flam.DynamicVars.Poison.BaseValue, flam.Owner.Creature, (CardModel) flam);
            WeakPower? weakPower = await PowerCmd.Apply<WeakPower>
                (choiceContext, hittableEnemy, flam.DynamicVars.Poison.BaseValue, flam.Owner.Creature, (CardModel) flam);

            hittableEnemy?.Monster?.RollMove((IEnumerable<Creature>) array);
            
            NCombatRoom.Instance?.GetCreatureNode(hittableEnemy)?.RefreshIntents();
        }
    }
    
    private void SpawnVfx()
    {
        Node? combatVfxContainer = NCombatRoom.Instance?.CombatVfxContainer;
        if (combatVfxContainer == null)
            return;
        NSmokyVignetteVfx? child = NSmokyVignetteVfx.Create(new Color(0.8f, 0.8f, 0.3f, 0.66f), new Color(0.0f, 4f, 0.0f, 0.33f));
        combatVfxContainer.AddChildSafely((Node?) child);
        
        ArgumentNullException.ThrowIfNull(this.CombatState, "CombatState");

        foreach (Creature hittableEnemy in (IEnumerable<Creature>)this.CombatState.HittableEnemies)
            combatVfxContainer.AddChildSafely((Node?)NSmokePuffVfx.Create(hittableEnemy,
                NSmokePuffVfx.SmokePuffColor.Green));
    }
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [(HoverTipFactory.FromPower<PoisonPower>()), HoverTipFactory.FromPower<WeakPower>(), HoverTipFactory.FromPower<AcidPower>()];
    
    protected override void OnUpgrade()
    {
        this.DynamicVars.Poison.UpgradeValueBy(1M);
        this.DynamicVars.Weak.UpgradeValueBy(1M);
    }
}