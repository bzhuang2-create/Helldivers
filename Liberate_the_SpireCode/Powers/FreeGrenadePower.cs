// Decompiled with JetBrains decompiler
// Type: MegaCrit.Sts2.Core.Models.Powers.FreeAttackPower
// Assembly: sts2, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 623673A3-2F6A-4E15-A560-4F44F2297867
// Assembly location: c:\program files (x86)\steam\steamapps\common\Slay the Spire 2\data_sts2_windows_x86_64\sts2.dll

using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using System;
using System.Threading.Tasks;
using BaseLib.Abstracts;
using Liberate_the_Spire.Liberate_the_SpireCode;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;

#nullable enable
namespace Liberate_the_Spire.liberate_the_SpireCode.Powers;


public sealed class FreeGrenadePower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public override string CustomPackedIconPath => 
        System.IO.Path.Join(MainFile.ResPath, "images", "powers", "free_grenade_power.png");
    
    public override string CustomBigIconPath => 
        System.IO.Path.Join(MainFile.ResPath, "images", "powers", "free_grenade_power.png");
    
    public override bool TryModifyEnergyCostInCombat(CardModel card, Decimal originalCost, out Decimal modifiedCost)
    {
        modifiedCost = originalCost;
        if (card.Owner.Creature != this.Owner || !card.Keywords.Contains(Liberate_the_SpireCode.Character.Liberate_the_Spire.HelldiverKeywords.Grenade))
            return false;
        PileType? type = card.Pile?.Type;
        bool flag;
        if (type.HasValue)
        {
            switch (type.GetValueOrDefault())
            {
                case PileType.Hand:
                case PileType.Play:
                    flag = true;
                    goto label_6;
            }
        }
        flag = false;
        label_6:
        if (!flag)
            return false;
        modifiedCost = 0M;
        return true;
    }

    public override async Task BeforeCardPlayed(CardPlay cardPlay)
    {
        FreeGrenadePower power = this;
        if (cardPlay.Card.Owner.Creature != power.Owner || !cardPlay.Card.Keywords.Contains(Liberate_the_SpireCode.Character.Liberate_the_Spire.HelldiverKeywords.Grenade))
            return;
        PileType? type = cardPlay.Card.Pile?.Type;
        bool flag;
        if (type.HasValue)
        {
            switch (type.GetValueOrDefault())
            {
                case PileType.Hand:
                case PileType.Play:
                    flag = true;
                    goto label_6;
            }
        }
        flag = false;
        label_6:
        if (!flag)
            return;
        await PowerCmd.Decrement((PowerModel) power);
    }
}