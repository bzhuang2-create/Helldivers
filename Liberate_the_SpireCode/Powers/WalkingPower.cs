using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Powers;


public sealed class WalkingPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override string CustomPackedIconPath => 
        System.IO.Path.Join(MainFile.ResPath, "images", "powers", "walking_power.png");
    
    public override string CustomBigIconPath => 
        System.IO.Path.Join(MainFile.ResPath, "images", "powers", "walking_power.png");

    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay play)
    {
        WalkingPower power = this;
        if (play.Card.Owner.Creature != power.Owner)
        {
            return;
        }
        else if(!(play.Card.Keywords.Contains(Liberate_the_Spire.Liberate_the_SpireCode.Character.Liberate_the_Spire.HelldiverKeywords.Artillery)))
        {
            return;
        }
        else
        {
            ArgumentNullException.ThrowIfNull(power.Owner.Player, "Player null");
            IEnumerable<CardModel> cardModels = await CardPileCmd.Draw(choiceContext, power.Amount, power.Owner.Player);
        }
    }
}