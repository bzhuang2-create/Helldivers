using BaseLib.Utils;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards.Starter_Deck;
using Liberate_the_Spire.Liberate_the_SpireCode.Character;
using Liberate_the_Spire.Liberate_the_SpireCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Cards.Uncommons.Stims;


[Pool(typeof(Liberate_the_SpireCardPool))]
public class Kill_em_All_() : Liberate_the_SpireCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<KillAllPower>(1),  new CardsVar(1)];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => new List<CardKeyword> 
        {CardKeyword.Exhaust};
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Kill_em_All_ kill = this;
        
        KillAllPower? power = await PowerCmd.Apply<KillAllPower>
            (choiceContext, kill.Owner.Creature, 1M, (kill.Owner.Creature), kill);
        
        foreach (CardModel card in PileType.Draw.GetPile(kill.Owner).Cards.Where<CardModel>((Func<CardModel, bool>) 
                     (c => c.Type == CardType.Attack && !c.Keywords.Contains(CardKeyword.Unplayable))).ToList<CardModel>().StableShuffle<CardModel>
                     (kill.Owner.RunState.Rng.Shuffle).Take<CardModel>(kill.DynamicVars.Cards.IntValue))
        {
            if (!CombatManager.Instance.IsOverOrEnding)
            {
                if (card.TargetType == TargetType.AnyEnemy)
                {
                    ArgumentNullException.ThrowIfNull(kill.CombatState, "combatState");
                    
                    Creature? target = kill.Owner.RunState.Rng.CombatTargets.NextItem<Creature>((IEnumerable<Creature>) kill.CombatState.HittableEnemies);
                    await CardCmd.AutoPlay(choiceContext, card, target);
                }
                else
                    await CardCmd.AutoPlay(choiceContext, card, null);
            }
            else
                break;
        }
        
    }

    protected override void OnUpgrade() => this.EnergyCost.UpgradeBy(-1);
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        (IEnumerable<IHoverTip>) new List<IHoverTip> {(HoverTipFactory.FromCard<Stim>())};
}

