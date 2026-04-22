using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards;
using Liberate_the_Spire.Liberate_the_SpireCode.Character;
using Liberate_the_Spire.Liberate_the_SpireCode.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Runs;
using MegaCrit.Sts2.Core.ValueProps;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Cards.Commons.Defense;


[Pool(typeof(Liberate_the_SpireCardPool))]
public class Dive() : Liberate_the_SpireCard(0, CardType.Skill, CardRarity.Common, TargetType.Self)
{   
    public override bool GainsBlock => true;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(5,  ValueProp.Move)];
    
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Dive pack = this;
        Decimal num = await CreatureCmd.GainBlock(pack.Owner.Creature, pack.DynamicVars.Block, play);
    }

    protected override void OnUpgrade()
    {
        this.DynamicVars.Block.UpgradeValueBy(2);
    }
}