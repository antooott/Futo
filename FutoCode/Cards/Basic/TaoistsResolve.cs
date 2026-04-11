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
        Random _rnd = new Random();
        int plateIndex = _rnd.Next(4);
        switch (plateIndex)
        {
            case 0: await FengPlateFire.CreateInHand(taoistsResolve.Owner, taoistsResolve.CombatState);
                break;
            case 1: await FengPlateWater.CreateInHand(taoistsResolve.Owner, taoistsResolve.CombatState);
                break;
            case 2: await FengPlateWood.CreateInHand(taoistsResolve.Owner, taoistsResolve.CombatState);
                break;
            case 3: await FengPlateMetal.CreateInHand(taoistsResolve.Owner, taoistsResolve.CombatState);
                break;
            case 4: await FengPlateEarth.CreateInHand(taoistsResolve.Owner, taoistsResolve.CombatState);
                break;
        }
        await Cmd.Wait(0.1f);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["Block"].UpgradeValueBy(4m);
    }
}