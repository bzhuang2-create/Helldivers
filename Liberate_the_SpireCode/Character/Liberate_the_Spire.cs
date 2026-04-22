using BaseLib.Abstracts;
using BaseLib.Patches.Content;
using BaseLib.Utils.NodeFactories;
using Liberate_the_Spire.Liberate_the_SpireCode.Extensions;
using Godot;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards.Commons.Defense;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards.Commons.Grenades;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards.Rares.Defense;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards.Rares.Grenades;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards.Starter_Deck;
using Liberate_the_Spire.liberate_the_SpireCode.Powers;
using Liberate_the_Spire.Liberate_the_SpireCode.Relics;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Relics;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Character;


public class Liberate_the_Spire : PlaceholderCharacterModel
{
    public const string CharacterId = "John Helldiver";

    public static readonly Color Color = new("ffffff");

    public override Color NameColor => Color;
    public override CharacterGender Gender => CharacterGender.Neutral;
    public override int StartingHp => 100;
    public override int StartingGold => 200;

    /*
    public override IEnumerable<CardModel> StartingDeck =>
    [
        ModelDb.Card<Liberator>(),
        ModelDb.Card<Liberator>(),
        ModelDb.Card<Liberator>(),
        ModelDb.Card<Liberator>(),
        ModelDb.Card<Liberator>(),
        ModelDb.Card<Liberator>(),
        ModelDb.Card<HE_Grenade>(),
        ModelDb.Card<HE_Grenade>(),
        ModelDb.Card<Resupply>(),
        ModelDb.Card<Stim>(),
        ModelDb.Card<Stim>(),
        ModelDb.Card<Eagle_Strafing_Run>(),
        ModelDb.Card<Eagle_Strafing_Run>(),
        ModelDb.Card<Eagle_Re_Arm>()
    ];
    */
    
    public override IEnumerable<CardModel> StartingDeck =>
    [
        ModelDb.Card<Liberator>(),
        ModelDb.Card<Liberator>(),
        ModelDb.Card<Incendiary_Impact_Grenade>(),
        ModelDb.Card<Incendiary_Impact_Grenade>(),
        ModelDb.Card<Jump_Pack>(),
        ModelDb.Card<Jump_Pack>(),
        
        ModelDb.Card<Grenade_Launcher>(),
        ModelDb.Card<Grenade_Launcher>(),
        
        ModelDb.Card<Dive>(),
        ModelDb.Card<Dive>(),
        ModelDb.Card<Resupply>(),
        ModelDb.Card<Stim>(),
        ModelDb.Card<Stim>(),

    ];

    public override IReadOnlyList<RelicModel> StartingRelics =>
    [
        ModelDb.Relic<Helldivers_Mobilize>()
    ];
    



public static class HelldiverKeywords
    {
        [CustomEnum, KeywordProperties(AutoKeywordPosition.After)]
        public static CardKeyword Resupply;

        [CustomEnum, KeywordProperties(AutoKeywordPosition.After)]
        public static CardKeyword ReArm;
        
        [CustomEnum, KeywordProperties(AutoKeywordPosition.After)]
        public static CardKeyword Grenade;
        
        [CustomEnum, KeywordProperties(AutoKeywordPosition.After)]
        public static CardKeyword Recharge;
        
        [CustomEnum, KeywordProperties(AutoKeywordPosition.After)]
        public static CardKeyword Artillery;
    }

    
    public override CardPoolModel CardPool => ModelDb.CardPool<Liberate_the_SpireCardPool>();
    public override RelicPoolModel RelicPool => ModelDb.RelicPool<Liberate_the_SpireRelicPool>();
    public override PotionPoolModel PotionPool => ModelDb.PotionPool<Liberate_the_SpirePotionPool>();

    /*  PlaceholderCharacterModel will utilize placeholder basegame assets for most of your character assets until you
        override all the other methods that define those assets.
        These are just some of the simplest assets, given some placeholders to differentiate your character with.
        You don't have to, but you're suggested to rename these images. */
    public override Control CustomIcon
    {
        get
        {
            var icon = NodeFactory<Control>.CreateFromResource(CustomIconTexturePath);
            icon.SetAnchorsAndOffsetsPreset(Control.LayoutPreset.FullRect);
            return icon;
        }
    }

    public override string CustomIconTexturePath => "character_icon_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectIconPath => "char_select_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectLockedIconPath => "char_select_char_name_locked.png".CharacterUiPath();
    public override string CustomMapMarkerPath => "map_marker_char_name.png".CharacterUiPath();
}