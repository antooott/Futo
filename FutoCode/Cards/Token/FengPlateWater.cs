using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using Futo.FutoCode.Cards;
using Futo.FutoCode.Character;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace Futo.FutoCode.Cards.Token;

public sealed class FengPlateWater : FutoCard
{
    public FengPlateWater() : base(0, CardType.Skill, CardRarity.Token, TargetType.Self)
    {
        WithBlock(5);
        WithKeyword(CardKeyword.Exhaust);
        WithKeyword(CardKeyword.Ethereal);
    }
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CommonActions.CardBlock(this, play);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Block"].UpgradeValueBy(3m);
    }
    public static async Task<CardModel?> CreateInHand(Player owner, CombatState combatState)
    {
        return (await FengPlateFire.CreateInHand(owner, 1, combatState)).FirstOrDefault<CardModel>();
    }
    public static async Task<IEnumerable<CardModel>> CreateInHand(
        Player owner,
        int count,
        CombatState combatState)
    {
        if (count == 0)
            return (IEnumerable<CardModel>) Array.Empty<CardModel>();
        if (CombatManager.Instance.IsOverOrEnding)
            return (IEnumerable<CardModel>) Array.Empty<CardModel>();
        List<CardModel> plates = new List<CardModel>();
        for (int index = 0; index < count; ++index)
            plates.Add((CardModel) combatState.CreateCard<FengPlateWater>(owner));
        IReadOnlyList<CardPileAddResult> combat = await CardPileCmd.AddGeneratedCardsToCombat((IEnumerable<CardModel>) plates, PileType.Hand, true);
        return (IEnumerable<CardModel>) plates;
    }
}