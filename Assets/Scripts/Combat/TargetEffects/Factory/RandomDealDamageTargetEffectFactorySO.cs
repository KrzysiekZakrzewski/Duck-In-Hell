using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    [CreateAssetMenu(fileName = nameof(RandomDealDamageTargetEffectFactorySO), menuName = nameof(BlueRacconGames) + "/" + nameof(MeleeCombat) + "/" + nameof(RandomDealDamageTargetEffectFactorySO))]
    public class RandomDealDamageTargetEffectFactorySO : MeleeTargetEffectFactorySO
    {
        [field: SerializeField] public int MinDamageValue;
        [field: SerializeField] public int MaxDamageValue;

        public override IMeleeTargetEffect CreateEffect()
        {
            return new RandomDealDamageTargetEffect(this);
        }
    }
}
