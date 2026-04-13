using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;

#nullable enable
namespace Futo.FutoCode.Powers;

public class FengMetalPower: CustomPowerModel
{

    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    
    
    public override async Task AfterDamageGiven(
        PlayerChoiceContext choiceContext,
        Creature? dealer,
        DamageResult result,
        ValueProp props,
        Creature target,
        CardModel? cardSource)
    {
        FengMetalPower fengMetalPower = this;
        if (dealer == null || dealer != fengMetalPower.Owner && dealer.PetOwner?.Creature != fengMetalPower.Owner || !props.IsPoweredAttack_())
            return;
        PlatingPower platingPower = await PowerCmd.Apply<PlatingPower>(fengMetalPower.Owner, (Decimal) (fengMetalPower.Amount), fengMetalPower.Owner, (CardModel) null);
        await PowerCmd.Remove((PowerModel)this);
    }

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        FengMetalPower power = this;
        if (side != power.Owner.Side)
            return;
        await PowerCmd.Remove((PowerModel)power);
    }
}