using BaseLib.Abstracts;
using BaseLib.Patches.Content;
using BaseLib.Utils.NodeFactories;
using Liberate_the_Spire.Liberate_the_SpireCode.Extensions;
using Godot;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards.Commons;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards.Commons.Artillery;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards.Commons.Bullet_Hose;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards.Commons.Defense;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards.Commons.FireGas;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards.Commons.Grenades;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards.Commons.Stims;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards.Rares.Artillery;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards.Rares.Artillery.Shells;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards.Rares.Bullet_Hose;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards.Rares.Defense;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards.Rares.FireGas;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards.Rares.Grenades;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards.Rares.Stims;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards.Starter_Deck;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards.Uncommons.Artillery;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards.Uncommons.Bullet_Hose;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards.Uncommons.Defense;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards.Uncommons.FireGas;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards.Uncommons.Grenades;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards.UnCommons.Grenades;
using Liberate_the_Spire.Liberate_the_SpireCode.Cards.Uncommons.Stims;
using Liberate_the_Spire.liberate_the_SpireCode.Powers;
using Liberate_the_Spire.Liberate_the_SpireCode.Relics;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Relics;
using Gas_Grenade = Liberate_the_Spire.Liberate_the_SpireCode.Cards.Commons.Grenades.Gas_Grenade;

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
        ModelDb.Card<Orbital_Precision_Strike>(),
        ModelDb.Card<Orbital_Precision_Strike>(),
        ModelDb.Card<Ammunition_Redesign>(),
        
        ModelDb.Card<Orbital_120>(),
        ModelDb.Card<Orbital_120>(),
        
        ModelDb.Card<Atmospheric_Monitoring>(),
        ModelDb.Card<Atmospheric_Monitoring>(),
        ModelDb.Card<Atmospheric_Monitoring>(),
        ModelDb.Card<Stratagem_Hero>(),
        ModelDb.Card<Stratagem_Hero>(),
        
        ModelDb.Card<Stim_Pistol>(),
        ModelDb.Card<Stim_Pistol>(),
        ModelDb.Card<Stim>(),
        ModelDb.Card<Stim>(),
        
        ModelDb.Card<Experimental_Infusion>(),
        ModelDb.Card<Experimental_Infusion>()
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

        [CustomEnum, KeywordProperties(AutoKeywordPosition.After)]
        public static CardKeyword Continuous;
    }

    public static class HelldiverTags
    {
        [CustomEnum] public static CardTag Stim;
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