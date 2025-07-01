using BlueRacconGames.MeleeCombat;
using BlueRacconGames.Pool;
using Damageable;
using System.Collections.Generic;
using TimeTickSystems;
using Units;
using UnityEngine;

namespace BlueRacconGames.Cards.Effects
{
    public class PassiveHitLoopCardEffect : PassiveHitCardEffect
    {
        [SerializeField] protected int tickDuration;
        [SerializeField] protected int tickLoopDuration;
        [SerializeField] protected ParticlePoolItem passiveVFX;
        [SerializeField] protected PositionOnSprite vfxPosition;

        protected int tick;
        protected Dictionary<IDamagableTarget, TargetData> targets;
        protected List<IDamagableTarget> targetsToRemove = new();

        public override void ApplyEffect(CardsController cardsController)
        {
            targets = new();

            cardsController.AddPassiveHitEffect(this);
        }
        public override void Execute(IDamagableTarget target, DefaultPooledEmitter pooledEmitter)
        {
            if (targets.ContainsKey(target)) return;

            var damagable = target.GameObject.GetComponent<IDamageable>();

            if (damagable == null || !damagable.DamagableIsOn) return;

            var vfxPosition = target.GameObject.GetComponent<IUnit>().GetOnSpritePosition(this.vfxPosition);
            var particleEffect = pooledEmitter.EmitItem<ParticlePoolItem>(passiveVFX, vfxPosition, Vector3.zero);

            particleEffect.transform.SetParent(target.GameObject.transform);

            TargetData targetData = new()
            {
                DamagableTarget = target,
                Damageable = damagable,
                StartTick = tick,
                VFX = particleEffect
            };

            if (targets.Count == 0)
                TimeTickSystem.OnTick += OnTick;

            targets.Add(target, targetData);
        }

        protected void OnEffectEnded(TargetData target)
        {
            target.VFX.ForceExpire();
            targetsToRemove.Add(target.DamagableTarget);
        }

        private void OnTick(object sender, OnTickEventArgs e)
        {
            if (targets.Count <= 0) return;

            foreach(var targetToRemove in targetsToRemove)
            {
                targets.Remove(targetToRemove);

                if (targets.Count <= 0)
                {
                    TimeTickSystem.OnTick -= OnTick;
                    return;
                }
            }

            foreach (var target in targets.Values)
            {
                int tickDifference = tick - target.StartTick;

                if (tickDifference >= tickDuration)
                {
                    OnEffectEnded(target);
                    return;
                }

                if (tickDifference % tickLoopDuration != 0) continue;

                target.Damageable.TakeDamage(damageAmount, out bool isFatalDamage);
                
                if(isFatalDamage)
                    OnEffectEnded(target);           
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