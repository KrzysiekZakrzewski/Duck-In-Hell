using BlueRacconGames.Pool;
using Units;
using UnityEngine;
using Zenject;

namespace BlueRacconGames.MeleeCombat
{
    public class SpawnHitVFXTargetEffect : IMeleeTargetEffect
    {
        private readonly ParticlePoolItem vfxEffect;
        private readonly PositionOnSprite vfxPosition;

        public SpawnHitVFXTargetEffect(ParticlePoolItem vfxEffect, PositionOnSprite vfxPosition)
        {
            this.vfxEffect = vfxEffect;
            this.vfxPosition = vfxPosition;
        }

        public void Execute(MeleeCombatControllerBase source, IDamagableTarget target)
        {
            var unit = target.GameObject.GetComponent<IUnit>();

            var vfxPosition = unit.GetOnSpritePosition(this.vfxPosition);
            var particleEffect = source.PooledEmitter.EmitItem<ParticlePoolItem>(vfxEffect, vfxPosition, Vector3.zero);

            particleEffect.OnExpireE += unit.PopPoolItem;

            unit.PushPoolItem(particleEffect);
        }
    }
}