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

namespace Liberate_the_Spire.Liberate_the_SpireCode.Cards.Uncommons.Grenades;



[Pool(typeof(Liberate_the_SpireCardPool))]
public class One_Two() : Liberate_the_SpireCard(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(8, ValueProp.Move)];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => new List<CardKeyword> 
        {CardKeyword.Exhaust, Character.Liberate_the_Spire.HelldiverKeywords.Resupply};
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        One_Two gp = this;
        ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        ArgumentNullException.ThrowIfNull(CombatState, "combatstate");
        AttackCommand attackCommand = await DamageCmd.Attack(gp.DynamicVars.Damage.BaseValue).FromCard((CardModel) gp)
            .Targeting(play.Target).WithHitFx("vfx/vfx_attack_blunt", tmpSfx: "blunt_attack.mp3").Execute(choiceContext);
        
        //ArgumentNullException.ThrowIfNull(gp.Owner.Creature, "gp.Owner.Creature");
        FreeGrenadePower? power = await PowerCmd.Apply<FreeGrenadePower>(choiceContext,gp.Owner.Creature, 2M, gp.Owner.Creature, gp);
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Damage.UpgradeValueBy(3M);
    }
    
    //THIS IS A TEST AND SHOULD BE REMOVED
    //protected override IEnumerable<IHoverTip> ExtraHoverTips => new List<IHoverTip> {(HoverTipFactory.FromPower<FreeGrenadePower>())};
}