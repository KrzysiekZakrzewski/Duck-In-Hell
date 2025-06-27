using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    [CreateAssetMenu(fileName = nameof(MeleeExecuteCardsEffectFactorySO), menuName = nameof(BlueRacconGames) + "/" + nameof(MeleeCombat) + "/" + nameof(MeleeExecuteCardsEffectFactorySO))]
    public class MeleeExecuteCardsEffectFactorySO : MeleeWeaponTargetEffectFactorySO
    {
        public override IMeleeWeaponTargetEffect CreateEffect()
        {
            return new MeleeExecuteCardsEffect();
        }
    }
}