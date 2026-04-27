// Decompiled with JetBrains decompiler
// Type: MegaCrit.Sts2.Core.Models.Cards.Buffer
// Assembly: sts2, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 623673A3-2F6A-4E15-A560-4F44F2297867
// Assembly location: c:\program files (x86)\steam\steamapps\common\Slay the Spire 2\data_sts2_windows_x86_64\sts2.dll

using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using System.Collections.Generic;
using System.Threading.Tasks;
using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Models;

#nullable enable
namespace Liberate_the_Spire.Liberate_the_SpireCode.Cards.Uncommons.Defense;


public sealed class Ballistic_Shield() : Liberate_the_SpireCard(3, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => (IEnumerable<DynamicVar>) [((DynamicVar) new PowerVar<BufferPower>(2M))];


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        Ballistic_Shield pack = this;
        if (pack.DynamicVars.Power<BufferPower>().BaseValue <= 0)
        {
            return;
        }else if (pack.DynamicVars.Power<BufferPower>().BaseValue == 1)
        {
            pack.AddKeyword(CardKeyword.Exhaust);
        }
        
        await CreatureCmd.TriggerAnim(pack.Owner.Creature, "Cast", pack.Owner.Character.CastAnimDelay);
        BufferPower? bufferPower = await PowerCmd.Apply<BufferPower>(choiceContext, pack.Owner.Creature, pack.DynamicVars["BufferPower"].BaseValue, pack.Owner.Creature, (CardModel) pack);
        
        pack.DynamicVars.Power<BufferPower>().UpgradeValueBy(-1);
    }

    protected override void OnUpgrade() => this.DynamicVars["BufferPower"].UpgradeValueBy(1M);
}