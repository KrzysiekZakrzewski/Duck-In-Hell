using BlueRacconGames.Pool;
using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    [CreateAssetMenu(fileName = nameof(LaunchAttackVFXFactorySO), menuName = nameof(BlueRacconGames) + "/" + nameof(MeleeCombat) + "/" + nameof(LaunchAttackVFXFactorySO))]
    public class LaunchAttackVFXFactorySO : LaunchAttackEffectFactorySO
    {
        [SerializeField] private ParticlePoolItem vfxEffect;
        public override ILaunchAttackEffect CreateEffect()
        {
            return new LaunchAttackVFXEffect(vfxEffect);
        }
    }
}