using BlueRacconGames.MeleeCombat;
using BlueRacconGames.Pool;
using Damageable;
using System.Collections.Generic;
using TimeTickSystems;
using UnityEngine;

namespace BlueRacconGames.Cards.Effects
{
    public class PassiveHitLoopCardEffect : PassiveHitCardEffect
    {
        [SerializeField] protected int tickDuration;
        [SerializeField] protected int tickLoopDuration;
        [SerializeField] protected ParticlePoolItem passiveVFX;

        protected int tick;
        protected Dictionary<IDamagableTarget, TargetData> targets;

        public override void ApplyEffect(CardsController cardsController)
        {
            targets = new();

            cardsController.AddPassiveHitEffect(this);
        }
        public override void Execute(IDamagableTarget target, DefaultPooledEmitter pooledEmitter)
        {
            if (targets.ContainsKey(target)) return;

            if (targets.Count == 0)
                TimeTickSystem.OnTick += OnTick;

            TargetData targetData = new()
            {
                DamagableTarget = target,
                Damageable = target.GameObject.GetComponent<IDamageable>(),
                StartTick = tick,
                VFX = pooledEmitter.EmitItem<ParticlePoolItem>(passiveVFX, target.GameObject.transform.position, Vector3.zero)
            };

            targets.Add(target, targetData);
        }

        protected void OnEffectEnded(TargetData target)
        {
            target.VFX.ForceExpire();
            targets.Remove(target.DamagableTarget);

            if (targets.Count > 0) return;

            TimeTickSystem.OnTick -= OnTick;
        }

        private void OnTick(object sender, OnTickEventArgs e)
        {
            if (targets.Count <= 0) return;

            foreach (var target in targets.Values)
            {
                int tickDifference = tick - target.StartTick;

                if (tickDifference >= tickDuration)
                {
                    OnEffectEnded(target);
                    return;
                }

                if (tickDifference % tickLoopDuration != 0) continue;

                target.Damageable.TakeDamage(damageAmount);
            }

            tick++;
        }


        protected struct TargetData
        {
            public IDamagableTarget DamagableTarget;
            public IDamageable Damageable;
            public int StartTick;
            public ParticlePoolItem VFX;
        }
    }
}