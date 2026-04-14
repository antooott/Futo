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

namespace Futo.FutoCode.Cards.Basic;

[Pool(typeof(FutoCardPool))]
public class StarDragonBow : FutoCard
{
    public StarDragonBow() : base(2, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
    {
        WithDamage(16);
    }
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        StarDragonBow starDragonBow = this;
        ArgumentNullException.ThrowIfNull((object) play.Target, "cardPlay.Target");
        await DamageCmd.Attack(starDragonBow.DynamicVars.Damage.BaseValue).FromCard((FutoCard) starDragonBow).Targeting(play.Target)
            .WithHitFx("vfx/vfx_attack_blunt", tmpSfx: "blunt_attack.mp3").Execute(choiceContext);
        
        CardModel card = CardFactory.GetDistinctForCombat(starDragonBow.Owner, starDragonBow.Owner.Character.CardPool.GetUnlockedCards(starDragonBow.Owner.UnlockState, starDragonBow.Owner.RunState.CardMultiplayerConstraint).Where<CardModel>((Func<CardModel, bool>) (c => c.Rarity == CardRarity.Token)), 1, starDragonBow.Owner.RunState.Rng.CombatCardGeneration).FirstOrDefault<CardModel>();
        if (card == null)
            return;
        CardPileAddResult combat = await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, true);
        await Cmd.Wait(0.1f);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Damage"].UpgradeValueBy(4m);
    }
}