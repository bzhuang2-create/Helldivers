using BaseLib.Abstracts;
using BaseLib.Utils;
using Liberate_the_Spire.Liberate_the_SpireCode.Character;

namespace Liberate_the_Spire.Liberate_the_SpireCode.Relics;

// Decompiled with JetBrains decompiler
// Type: MegaCrit.Sts2.Core.Models.Relics.RingOfTheDrake
// Assembly: sts2, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 623673A3-2F6A-4E15-A560-4F44F2297867
// Assembly location: c:\program files (x86)\steam\steamapps\common\Slay the Spire 2\data_sts2_windows_x86_64\sts2.dll

using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using System;
using System.Collections.Generic;

#nullable enable


[Pool(typeof(Liberate_the_SpireRelicPool))]
public sealed class Helldivers_Mobilize : CustomRelicModel
{
    public override RelicRarity Rarity => RelicRarity.Starter;

    public override string PackedIconPath => 
        System.IO.Path.Join(MainFile.ResPath, "images", "relics", "helldivers_mobilize.png");
    
    protected override string PackedIconOutlinePath => 
        System.IO.Path.Join(MainFile.ResPath, "images", "relics", "helldivers_mobilize_outline.png");

    protected override IEnumerable<DynamicVar> CanonicalVars => [((DynamicVar) new CardsVar(1))];

    public override Decimal ModifyHandDraw(Player player, Decimal count)
    {
        ArgumentNullException.ThrowIfNull(player.Creature.CombatState);
        return player != this.Owner ? count : count + this.DynamicVars.Cards.IntValue;
    }
}
