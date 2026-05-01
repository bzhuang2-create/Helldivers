using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards;
using Liberate_the_Spire.Liberate_the_SpireCode.Character;
using Liberate_the_Spire.Liberate_the_SpireCode.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Cards.Commons.Grenades;



[Pool(typeof(Liberate_the_SpireCardPool))]
public class Gas_Grenade() : Liberate_the_SpireCard(1, CardType.Skill, CardRarity.Common, TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<PoisonPower>(3)];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => new List<CardKeyword> 
        {CardKeyword.Exhaust, Character.Liberate_the_Spire.HelldiverKeywords.Resupply, Character.Liberate_the_Spire.HelldiverKeywords.Grenade};
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Gas_Grenade gren = this;
        //ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        ArgumentNullException.ThrowIfNull(CombatState, "combatState");
        ArgumentNullException.ThrowIfNull(gren.CombatState, "Grenade combatState");
        await CreatureCmd.TriggerAnim(gren.Owner.Creature, "Cast", gren.Owner.Character.CastAnimDelay);
        
        gren.SpawnVfx();
        await Cmd.CustomScaledWait(0.2f, 0.4f);
        
        foreach (Creature hittableEnemy in gren.CombatState.HittableEnemies)
        {
            PoisonPower? poisonPower = await PowerCmd.Apply<PoisonPower>(choiceContext, hittableEnemy, gren.DynamicVars.Poison.BaseValue, gren.Owner.Creature, (CardModel) gren);
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
        
        foreach (Creature hittableEnemy in (IEnumerable<Creature>) this.CombatState.HittableEnemies)
            combatVfxContainer.AddChildSafely((Node?) NSmokePuffVfx.Create(hittableEnemy, NSmokePuffVfx.SmokePuffColor.Green));
    }
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [(HoverTipFactory.FromPower<PoisonPower>())];
    
    protected override void OnUpgrade()
    {
        this.DynamicVars.Poison.UpgradeValueBy(1M);
    }
}