using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using Futo.FutoCode.Cards;
using Futo.FutoCode.Cards.Token;
using Futo.FutoCode.Character;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Models.Cards;

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
        await FengPlateFire.CreateInHand(taoistsResolve.Owner, taoistsResolve.CombatState);
        await Cmd.Wait(0.1f);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Block"].UpgradeValueBy(4m);
    }
}