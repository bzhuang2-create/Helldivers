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

namespace Liberate_the_Spire.Liberate_the_SpireCode.Cards.Rares.Grenades;




[Pool(typeof(Liberate_the_SpireCardPool))]
public class Seaf_Artillery() : Liberate_the_SpireCard(2, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<WeakPower>(2), new CardsVar(2)];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => new List<CardKeyword> 
        {CardKeyword.Exhaust};

    private bool is_upgraded = false;
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        Seaf_Artillery SEAF = this;

        if (is_upgraded)
        {
            
        }
    }
    
    protected override void OnUpgrade()
    {
        is_upgraded = true;
    }
    
    //protected override IEnumerable<IHoverTip> ExtraHoverTips => new List<IHoverTip> {(HoverTipFactory.FromPower<WeakPower>())};
    //Consider changing this later
}