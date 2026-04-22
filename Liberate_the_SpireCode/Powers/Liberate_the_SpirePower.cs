using BaseLib.Abstracts;
using BaseLib.Extensions;
using Liberate_the_Spire.Liberate_the_SpireCode.Extensions;
using Godot;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Powers;

public abstract class Liberate_the_SpirePower : CustomPowerModel
{
    //Loads from Liberate_the_Spire/images/powers/your_power.png
    public override string CustomPackedIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PowerImagePath();
    public override string CustomBigIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigPowerImagePath();
}