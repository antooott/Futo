using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using Futo.FutoCode.Cards;
using Futo.FutoCode.Character;
using Futo.FutoCode.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Runs;

namespace Futo.FutoCode.Cards.Uncommon;

[Pool(typeof(FutoCardPool))]

public class FlowingGuard : FutoCard
{
    public FlowingGuard() : base(2, CardType.Power, CardRarity.Uncommon, TargetType.Self)
    {
        WithPower<FlowingGuardPower>(1);
    }

    
    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CommonActions.ApplySelf<FlowingGuardPower>(this, DynamicVars["FlowingGuardPower"].BaseValue);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["FlowingGuardPower"].UpgradeValueBy(1);
    }
}