using System.Threading.Tasks.Dataflow;
using BaseLib.Abstracts;
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

public class FengWoodPower: CustomPowerModel
{

    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    

    protected override object InitInternalData() => (object) new FengWoodPower.Data();

    public override Task BeforeCardPlayed(CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner.Creature != this.Owner)
            return Task.CompletedTask;
        this.GetInternalData<FengWoodPower.Data>().amountsForPlayedCards.Add(cardPlay.Card, this.Amount);
        return Task.CompletedTask;
    }

    public override async Task AfterCardPlayed(PlayerChoiceContext context, CardPlay cardPlay)
    {
        FengWoodPower fengWoodPower = this;
        int amount;
        if (cardPlay.Card.Owner.Creature != fengWoodPower.Owner || !fengWoodPower.GetInternalData<FengWoodPower.Data>().amountsForPlayedCards.Remove(cardPlay.Card, out amount) || amount <= 0 || cardPlay.Card.Type == CardType.Status)
            return;
        for (int i = 0; i < fengWoodPower.Amount; i++)
        {
            CardModel cardModel = await CardPileCmd.Draw(context, fengWoodPower.Owner.Player);
        }
        await PowerCmd.Remove((PowerModel)this);
    }

    private class Data
    {
        public readonly Dictionary<CardModel, int> amountsForPlayedCards = new Dictionary<CardModel, int>();
    }

    public override async Task AfterTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        FengWoodPower power = this;
        if (side != power.Owner.Side)
            return;
        await PowerCmd.Remove((PowerModel)power);
    }
}