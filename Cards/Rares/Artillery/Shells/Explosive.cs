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

namespace Liberate_the_Spire.Liberate_the_SpireCode.Cards.Rares.Artillery.Shells;




public class Explosive() : Liberate_the_SpireCard(1, CardType.Attack, CardRarity.Token, TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(10, ValueProp.Move)];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => new List<CardKeyword> 
        {CardKeyword.Exhaust, Character.Liberate_the_Spire.HelldiverKeywords.Artillery};
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Explosive shell = this;
        //ArgumentNullException.ThrowIfNull(play.Target, "cardPlay.Target");
        ArgumentNullException.ThrowIfNull(CombatState, "CombatState null");
        
        AttackCommand attackCommand = await DamageCmd.Attack(shell.DynamicVars.Damage.BaseValue).FromCard((CardModel) shell)
            .TargetingAllOpponents(CombatState).WithHitFx("vfx/vfx_attack_blunt", tmpSfx: "blunt_attack.mp3").Execute(choiceContext);
        
    }
    
    protected override void OnUpgrade()
    {
        this.DynamicVars.Damage.UpgradeValueBy(5M);
    }
}