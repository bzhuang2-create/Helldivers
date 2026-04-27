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
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.ValueProps;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Cards.Rares.Grenades;



[Pool(typeof(Liberate_the_SpireCardPool))]
public class Grenade_Launcher() : Liberate_the_SpireCard(3, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies)
{   
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(10, ValueProp.Move), new CardsVar((int)2)];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => new List<CardKeyword> {CardKeyword.Exhaust, Character.Liberate_the_Spire.HelldiverKeywords.Resupply};
    
    //protected override IEnumerable<CardRarityOddsType> => RegularEncounter;
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Grenade_Launcher GL = this;
        ArgumentNullException.ThrowIfNull(CombatState, "cardPlay.Target");
        AttackCommand attackCommand = await DamageCmd.Attack(GL.DynamicVars.Damage.BaseValue).FromCard((CardModel) GL).
            WithHitCount(1).TargetingAllOpponents(CombatState).WithHitFx("vfx/vfx_attack_blunt", tmpSfx: "blunt_attack.mp3").Execute(choiceContext);

        ArgumentNullException.ThrowIfNull(GL.Owner, "cardPlay.Target");
        
        for (int i = 0; i < GL.DynamicVars.Cards.BaseValue; i++)
        {
            CardModel? card = CardFactory.GetDistinctForCombat(GL.Owner, GL.Owner.Character.CardPool.GetUnlockedCards(GL.Owner.UnlockState, 
                    GL.Owner.RunState.CardMultiplayerConstraint).Where<CardModel>(c => c.CanonicalKeywords.Contains(Liberate_the_SpireCode.Character
                .Liberate_the_Spire.HelldiverKeywords.Grenade)), (int)GL.DynamicVars.Cards.BaseValue, GL.Owner.RunState.Rng.CombatCardGeneration).FirstOrDefault<CardModel>();
            
            if (card == null)
                return;
            CardPileAddResult combat = await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, card.Owner);
        }
        
        FreeGrenadePower? power = await PowerCmd.Apply<FreeGrenadePower>(choiceContext, GL.Owner.Creature, 3M, GL.Owner.Creature, GL);
    }

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
        (IEnumerable<IHoverTip>)new List<IHoverTip> {(HoverTipFactory.FromKeyword(Character.Liberate_the_Spire.HelldiverKeywords.Grenade))};
    
    protected override void OnUpgrade()
    {
        this.DynamicVars.Damage.UpgradeValueBy(3M);
        this.DynamicVars.Cards.UpgradeValueBy(1);
    }
}