using BaseLib.Abstracts;
using Futo.FutoCode.Extensions;
using Godot;

namespace Futo.FutoCode.Character;

public class FutoRelicPool : CustomRelicPoolModel
{
    public override Color LabOutlineColor => Futo.Color;

    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}