using BlueRacconGames.MeleeCombat;
using BlueRacconGames.Pool;
using Damageable;
using System.Collections.Generic;
using Units;
using UnityEngine;

namespace BlueRacconGames.Cards.Effects
{
    public abstract class PassiveHitCardEffect : CardEffectBase
    {
        [SerializeField] protected int damageAmount;
        [SerializeField] protected ParticlePoolItem vfxEffect;
        [SerializeField] protected PositionOnSprite vfxPosition;

        protected int tick;
        protected Dictionary<IDamagableTarget, TargetData> targets = new();
        protected List<IDamagableTarget> targetsToRemove = new();

        public override void ApplyEffect(CardsController cardsController, IUnit source)
        {
            base.ApplyEffect(cardsController, source);

            cardsController.AddPassiveHitEffect(this);
        }
        public virtual void Execute(IDamagableTarget target, DefaultPooledEmitter pooledEmitter)
        {
            if (targets.ContainsKey(target) || !target.GameObject.TryGetComponent<IUnit>(out var unit)) return;

            var damageable = unit.Damageable;

            if (damageable == null || !damageable.DamagableIsOn || damageable.IsDead) return;

            var vfxPosition = unit.GetOnSpritePosition(this.vfxPosition);
            var particleEffect = pooledEmitter.EmitItem<ParticlePoolItem>(vfxEffect, vfxPosition, Vector3.zero);

            unit.PushPoolItem(particleEffect);

            TargetData targetData = new()
            {
                DamagableTarget = target,
                Unit = unit,
                StartTick = tick,
                VFX = particleEffect
            };

            damageable.OnExpireE += (IDamageable damagable) => OnEffectEnded(targetData);

            targets.Add(target, targetData);

            ExecuteInternal(targetData, particleEffect);
        }

        protected abstract void ExecuteInternal(TargetData targetData, ParticlePoolItem particleEffect);
        protected abstract void OnEffectEnded(TargetData targetData);
        protected struct TargetData
        {
            public IDamagableTarget DamagableTarget;
            public IUnit Unit;
            public int StartTick;
            public ParticlePoolItem VFX;
        }
    }
}