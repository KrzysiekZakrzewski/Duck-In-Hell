using BlueRacconGames.Pool;
using System;
using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    public class LaunchAttackVFXEffect : ILaunchAttackEffect
    {
        private ParticlePoolItem vfxEffect;

        public LaunchAttackVFXEffect(ParticlePoolItem vfxEffect)
        {
            this.vfxEffect = vfxEffect;
        }

        public void Execute(MeleeCombatControllerBase source)
        {
            var emitter = source.PooledEmitter;

            Vector3 spawnPosition = source.AttackPosition;
            Vector3 direction = Vector3.zero;

            emitter.EmitItem<ParticlePoolItem>(vfxEffect, spawnPosition, direction);
        }
    }
}
