using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using Futo.FutoCode.Cards;
using Futo.FutoCode.Character;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace Futo.FutoCode.Cards.Rare;

[Pool(typeof(FutoCardPool))]

public class CeremonialArray : FutoCard
{
    public CeremonialArray() : base(1, CardType.Skill, CardRarity.Rare, TargetType.Self)
    {
        WithCards(3);
        WithKeyword(CardKeyword.Exhaust);
    }

    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        CeremonialArray ceremonialArray = this;
        for (int i = 0; i < ceremonialArray.DynamicVars.Cards.IntValue; i++)
        {
            CardModel card = CardFactory.GetDistinctForCombat(ceremonialArray.Owner,
                ceremonialArray.Owner.Character.CardPool
                    .GetUnlockedCards(ceremonialArray.Owner.UnlockState,
                        ceremonialArray.Owner.RunState.CardMultiplayerConstraint)
                    .Where<CardModel>((Func<CardModel, bool>)(c => c.Rarity == CardRarity.Token)), 1,
                ceremonialArray.Owner.RunState.Rng.CombatCardGeneration).FirstOrDefault<CardModel>();
            CardPileAddResult combat = await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, true);
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Cards"].UpgradeValueBy(1m);
    }
}