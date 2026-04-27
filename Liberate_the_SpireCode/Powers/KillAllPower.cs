using BaseLib.Abstracts;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards.Starter_Deck;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Relics;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Powers;


public sealed class KillAllPower() : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override string CustomPackedIconPath => 
        System.IO.Path.Join(MainFile.ResPath, "images", "powers", "kill_all_power.png");
    
    public override string CustomBigIconPath => 
        System.IO.Path.Join(MainFile.ResPath, "images", "powers", "kill_all_power.png");
    
    public override async Task AfterDeath(PlayerChoiceContext choiceContext, Creature target, bool wasRemovalPrevented, float deathAnimLength)
    {
        KillAllPower power = this;
        if (target.Side == power.Owner.Side)
            return;
        for (int i = 0; i < power.Amount; i++)
        {
            power.Flash();
        
            ArgumentNullException.ThrowIfNull(power.Owner.Player, "Player NULL");
            
            CardModel stim = power.CombatState.CreateCard<Stim>(power.Owner.Player);
            stim.SetToFreeThisTurn();
            CardCmd.PreviewCardPileAdd(await CardPileCmd.AddGeneratedCardToCombat(stim, PileType.Hand, power.Owner.Player));
        }
    }
    
    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        KillAllPower power = this;
        if (side != power.Owner.Side)
            return;
        await PowerCmd.Remove((PowerModel) power);
    }
}

