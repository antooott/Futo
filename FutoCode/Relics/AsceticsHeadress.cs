using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace Futo.FutoCode.Relics;

public class AsceticsHeadress : FutoRelic
{
    //private const string _extraDamageKey = "ExtraDamage";

    public override RelicRarity Rarity => RelicRarity.Starter;
    //protected override IEnumerable<DynamicVar> CanonicalVars => [new DynamicVar(_extraDamageKey, 2)];
    
    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        AsceticsHeadress asceticsHeadress = this;
        if (player != asceticsHeadress.Owner || asceticsHeadress.Owner.Creature.CombatState.RoundNumber != 1)
            return;
        asceticsHeadress.Flash();
        for (int i = 0; i < 3; i++)
        {
            CardModel card = CardFactory.GetDistinctForCombat(asceticsHeadress.Owner,
                asceticsHeadress.Owner.Character.CardPool
                    .GetUnlockedCards(asceticsHeadress.Owner.UnlockState,
                        asceticsHeadress.Owner.RunState.CardMultiplayerConstraint)
                    .Where<CardModel>((Func<CardModel, bool>)(c => c.Rarity == CardRarity.Token)), 1,
                asceticsHeadress.Owner.RunState.Rng.CombatCardGeneration).FirstOrDefault<CardModel>();
            CardPileAddResult combat = await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, true);
        }
    }
}