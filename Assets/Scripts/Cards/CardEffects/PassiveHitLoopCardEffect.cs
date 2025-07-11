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

        public override void ApplyEffect(CardsController cardsController, IUnit source)
        {
            base.ApplyEffect(cardsController, source);

            targets = new();

            cardsController.AddPassiveHitEffect(this);
        }

        public override void DiscardEffect()
        {

        }

        public override void Execute(IDamagableTarget target, DefaultPooledEmitter pooledEmitter)
        {
            if (targets.ContainsKey(target)) return;
    
            if(!target.GameObject.TryGetComponent<IUnit>(out var unit)) return;

            var damageable = unit.Damageable;

            if (damageable == null || !damageable.DamagableIsOn || damageable.IsDead) return;

            var vfxPosition = unit.GetOnSpritePosition(this.vfxPosition);
            var particleEffect = pooledEmitter.EmitItem<ParticlePoolItem>(passiveVFX, vfxPosition, Vector3.zero);

            unit.PushPoolItem(particleEffect);

            TargetData targetData = new()
            {
                DamagableTarget = target,
                Unit = unit,
                StartTick = tick,
                VFX = particleEffect
            };

            damageable.OnExpireE += (IDamageable damagable) => OnEffectEnded(targetData);

            if (targets.Count == 0)
                TimeTickSystem.OnTick += OnTick;

            targets.Add(target, targetData);
        }

        protected void OnEffectEnded(TargetData target)
        {
            target.VFX.ForceExpire();
            target.Unit.PopPoolItem(target.VFX);
            targetsToRemove.Add(target.DamagableTarget);
        }

        private void OnTick(object sender, OnTickEventArgs e)
        {
            if (targets.Count <= 0) return;

            foreach (var targetToRemove in targetsToRemove)
            {
                if(!targets.TryGetValue(targetToRemove, out TargetData targetData)) continue;

                var damagable = targetData.Unit.Damageable;

                damagable.OnExpireE -= (IDamageable damagable) => OnEffectEnded(targetData);

                targets.Remove(targetToRemove);
            }

            targetsToRemove.Clear();

            if (targets.Count <= 0)
            {
                TimeTickSystem.OnTick -= OnTick;
                return;
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

                var damagable = target.Unit.Damageable;

                damagable.TakeDamage(damageAmount);     
            }

            tick++;
        }

        protected struct TargetData
        {
            public IDamagableTarget DamagableTarget;
            public IUnit Unit;
            public int StartTick;
            public ParticlePoolItem VFX;
        }
    }
}