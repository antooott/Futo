using BaseLib.Abstracts;
using BaseLib.Utils;
using Futo.FutoCode.Character;

namespace Futo.FutoCode.Potions;

[Pool(typeof(FutoPotionPool))]
public abstract class FutoPotion : CustomPotionModel;