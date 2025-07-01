using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    [CreateAssetMenu(fileName = nameof(ExecuteCardsTargetEffectFactorySO), menuName = nameof(BlueRacconGames) + "/" + nameof(MeleeCombat) + "/" + nameof(ExecuteCardsTargetEffectFactorySO))]
    public class ExecuteCardsTargetEffectFactorySO : MeleeTargetEffectFactorySO
    {
        public override IMeleeTargetEffect CreateEffect()
        {
            return new ExecuteCardsTargetEffect();
        }
    }
}