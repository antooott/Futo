using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using Futo.FutoCode.Cards;
using Futo.FutoCode.Character;
using Futo.FutoCode.Powers;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;

namespace Futo.FutoCode.Cards.Token;

public sealed class TalismanOfEmbers : FutoCard
{
    public TalismanOfEmbers() : base(2, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
    {
        WithPower<BurnPower>(6);
        WithKeyword(CardKeyword.Exhaust);
    }
    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        TalismanOfEmbers cardSource = this;
        ArgumentNullException.ThrowIfNull((object) cardPlay.Target, "cardPlay.Target");
        await CreatureCmd.TriggerAnim(cardSource.Owner.Creature, "Cast", cardSource.Owner.Character.CastAnimDelay);
        BurnPower burnPower = await PowerCmd.Apply<BurnPower>(cardPlay.Target, cardSource.DynamicVars["BurnPower"].BaseValue, cardSource.Owner.Creature, (CardModel) cardSource);
    }
    protected override void OnUpgrade()
    {
        this.RemoveKeyword(CardKeyword.Exhaust);
    }
}