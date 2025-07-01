using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    [CreateAssetMenu(fileName = nameof(DealDamagetargetEffectFactorySO), menuName = nameof(BlueRacconGames) + "/" + nameof(MeleeCombat) + "/" + nameof(DealDamagetargetEffectFactorySO))]

    public class DealDamagetargetEffectFactorySO : MeleeTargetEffectFactorySO
    {
        [SerializeField] private int damageValue;
        public override IMeleeTargetEffect CreateEffect()
        {
            return new DealDamageTargetEffect(damageValue); 
        }
    }
}