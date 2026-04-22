using BaseLib.Abstracts;
using Liberate_the_Spire.Liberate_the_SpireCode.Extensions;
using Godot;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Character;

public class Liberate_the_SpireRelicPool : CustomRelicPoolModel
{
    public override Color LabOutlineColor => Liberate_the_Spire.Color;

    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}