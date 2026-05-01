using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Powers;


public sealed class InfusionPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override string CustomPackedIconPath => 
        System.IO.Path.Join(MainFile.ResPath, "images", "powers", "infusion_power.png");
    
    public override string CustomBigIconPath => 
        System.IO.Path.Join(MainFile.ResPath, "images", "powers", "infusion_power.png");
    
    public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay play)
    {
        InfusionPower power = this;
        if (play.Card.Owner.Creature != power.Owner)
        {
            return;
        }
        else if(!(play.Card.Tags.Contains(Character.Liberate_the_Spire.HelldiverTags.Stim)))
        {
            return;
        }
        else
        {
            ArgumentNullException.ThrowIfNull(power.Owner.Player, "Player null");
            Decimal num = await CreatureCmd.GainBlock(power.Owner, (Decimal) power.Amount, ValueProp.Unpowered, null);
            PlatingPower? plating =
                await PowerCmd.Apply<PlatingPower>(choiceContext, power.Owner, power.Amount, power.Owner, null);
        }
    }
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => 
        [(HoverTipFactory.FromPower<PlatingPower>())];
}