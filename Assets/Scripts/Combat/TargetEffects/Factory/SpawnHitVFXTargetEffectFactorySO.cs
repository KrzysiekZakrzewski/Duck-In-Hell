using BlueRacconGames.Pool;
using Units;
using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    [CreateAssetMenu(fileName = nameof(SpawnHitVFXTargetEffectFactorySO), menuName = nameof(BlueRacconGames) + "/" + nameof(MeleeCombat) + "/" + nameof(SpawnHitVFXTargetEffectFactorySO))]
    public class SpawnHitVFXTargetEffectFactorySO : MeleeTargetEffectFactorySO
    {
        [SerializeField] private ParticlePoolItem vfxEffect;
        [SerializeField] private PositionOnSprite vfxPosition;

        public override IMeleeTargetEffect CreateEffect()
        {
            return new SpawnHitVFXTargetEffect(vfxEffect, vfxPosition);
        }
    }
}