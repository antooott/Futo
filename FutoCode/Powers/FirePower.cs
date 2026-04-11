using BaseLib.Abstracts;
using BaseLib.Utils;
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

public class FirePower: CustomPowerModel
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
        FirePower firePower = this;
        if (dealer == null || dealer != firePower.Owner && dealer.PetOwner?.Creature != firePower.Owner || !props.IsPoweredAttack())
            return;
        PoisonPower poisonPower = await PowerCmd.Apply<PoisonPower>(target, (Decimal) (firePower.Amount), firePower.Owner, (CardModel) null);
        await PowerCmd.Remove((PowerModel)this);
    }
    
    
}