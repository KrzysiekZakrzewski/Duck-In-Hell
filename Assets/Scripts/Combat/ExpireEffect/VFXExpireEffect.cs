using BlueRacconGames.Pool;
using Damageable;
using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    public class VFXExpireEffect : IExpireEffect
    {
        private readonly ParticlePoolItem vfxEffect;

        public VFXExpireEffect(ParticlePoolItem vfxEffect)
        {
            this.vfxEffect = vfxEffect;
        }

        public void Execute(IDamageable damageable, DefaultPooledEmitter pooledEmitter)
        {
            pooledEmitter.EmitItem<ParticlePoolItem>(vfxEffect, damageable.GameObject.transform.position, Vector3.zero);
        }
    }
}
