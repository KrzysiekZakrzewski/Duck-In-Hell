using BlueRacconGames.MeleeCombat;
using BlueRacconGames.Pool;
using Damageable;
using TimeTickSystems;
using Units;
using UnityEngine;

namespace BlueRacconGames.Cards.Effects
{
    public class PassiveHitLoopCardEffect : PassiveHitCardEffect
    {
        [SerializeField] protected int tickDuration;
        [SerializeField] protected int tickLoopDuration;

        public override void DiscardEffect()
        {

        }

        protected override void ExecuteInternal(TargetData targetData, ParticlePoolItem particleEffect)
        {
            if (!IsFirstTarget()) return;

            TimeTickSystem.OnTick += OnTick;
        }
        protected override void OnEffectEnded(TargetData target)
        {
            target.VFX.ForceExpire();
            target.Unit.PopPoolItem(target.VFX);
            targetsToRemove.Add(target.DamagableTarget);
        }

        private void OnTick(object sender, OnTickEventArgs e)
        {
            Debug.Log("TickE");

            if (targets.Count <= 0) return;
            Debug.Log("TickE2");
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
        private bool IsFirstTarget() => targets.Count == 1;
    }
}