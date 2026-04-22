using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using Liberate_the_Spire.Liberate_the_SpireCode.Character;
using Liberate_the_Spire.Liberate_the_SpireCode.Extensions;
using Godot;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Relics;

[Pool(typeof(Liberate_the_SpireRelicPool))]
public abstract class Liberate_the_SpireRelic : CustomRelicModel
{
    public override string PackedIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".RelicImagePath();

    protected override string PackedIconOutlinePath =>
        $"{Id.Entry.RemovePrefix().ToLowerInvariant()}_outline.png".RelicImagePath();

    protected override string BigIconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".BigRelicImagePath();
}