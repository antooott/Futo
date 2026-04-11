using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using Futo.FutoCode.Cards;
using Futo.FutoCode.Cards.Token;
using Futo.FutoCode.Character;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Events;

#nullable enable
namespace Futo.FutoCode.Cards.Basic;

[Pool(typeof(FutoCardPool))]
public class TaoistsResolve : FutoCard
{
    public TaoistsResolve() : base(2, CardType.Skill, CardRarity.Basic, TargetType.Self)
    {
        WithBlock(8);
    }
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        TaoistsResolve taoistsResolve = this;
        await CommonActions.CardBlock(this, play);
        
        CardModel card = CardFactory.GetDistinctForCombat(taoistsResolve.Owner, taoistsResolve.Owner.Character.CardPool.GetUnlockedCards(taoistsResolve.Owner.UnlockState, taoistsResolve.Owner.RunState.CardMultiplayerConstraint).Where<CardModel>((Func<CardModel, bool>) (c => c.Rarity == CardRarity.Token)), 1, taoistsResolve.Owner.RunState.Rng.CombatCardGeneration).FirstOrDefault<CardModel>();
        if (card == null)
            return;
        CardPileAddResult combat = await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, true);
        await Cmd.Wait(0.1f);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Block"].UpgradeValueBy(4m);
    }
}