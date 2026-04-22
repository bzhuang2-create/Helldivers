using BaseLib.Abstracts;
using Liberate_the_Spire.Liberate_the_SpireCode.Extensions;
using Godot;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards.Commons;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Character;

public class Liberate_the_SpireCardPool : CustomCardPoolModel
{
    public override string Title => Liberate_the_Spire.CharacterId; //This is not a display name.

    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();


    /* These HSV values will determine the color of your card back.
    They are applied as a shader onto an already colored image,
    so it may take some experimentation to find a color you like.
    Generally they should be values between 0 and 1. */
    public override float H => 1f; //Hue; changes the color.
    public override float S => 1f; //Saturation
    public override float V => 1f; //Brightness

    //Alternatively, leave these values at 1 and provide a custom frame image.
    /*public override Texture2D CustomFrame(CustomCardModel card)
    {
        //This will attempt to load Liberate_the_Spire/images/cards/frame.png
        return PreloadManager.Cache.GetTexture2D("cards/frame.png".ImagePath());
    }*/

    //Color of small card icons
    public override Color DeckEntryCardColor => new("ffffff");

    public override bool IsColorless => false;
    
}
