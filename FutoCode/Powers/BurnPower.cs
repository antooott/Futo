// Decompiled with JetBrains decompiler
// Type: MegaCrit.Sts2.Core.Models.Powers.PoisonPower
// Assembly: sts2, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1B6C867B-1925-4D98-A300-0B940AE284E4
// Assembly location: /home/toto/.steam/steam/steamapps/common/Slay the Spire 2/data_sts2_linuxbsd_x86_64/sts2.dll

using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MegaCrit.Sts2.Core.Models;

#nullable enable
namespace Futo.FutoCode.Powers;

public sealed class BurnPower : FutoPower
{
  public override PowerType Type => PowerType.Debuff;

  public override PowerStackType StackType => PowerStackType.Counter;

  public override async Task AfterSideTurnStart(CombatSide side, CombatState combatState)
  {
    BurnPower burnPower = this;
    if (side != burnPower.Owner.Side)
      return;
    burnPower.Flash();
    IEnumerable<DamageResult> damageResults = await CreatureCmd.Damage((PlayerChoiceContext) new ThrowingPlayerChoiceContext(), burnPower.Owner, (Decimal) burnPower.Amount, ValueProp.Unblockable | ValueProp.Unpowered, (Creature) null, (CardModel) null);
  }
}