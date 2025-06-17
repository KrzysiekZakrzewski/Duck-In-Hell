using BlueRacconGames.MeleeCombat;
using BlueRacconGames.Pool;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Projectiles.Implementation
{
    [RequireComponent(typeof(Rigidbody2D))]
    public abstract class ProjectileBase : PoolItemBase, IProjectile
    {
        private readonly HashSet<IDamagableTarget> hitTargets = new HashSet<IDamagableTarget>();
        private float lastLaunchTime;

        public abstract float Speed { get; }
        public abstract float ExpireTime { get; }
        public abstract bool ExpireOnHit { get; }
        public List<IProjectileTargetEffect> ProjectileTargetHitEffects { get; } = new List<IProjectileTargetEffect>();
        private float TimeSinceLaunch => Time.time - lastLaunchTime;
        private float angleOffSet = -90f;

        public new event Action<IProjectile> OnLaunchE;
        public event Action<IDamagableTarget> OnHitE;
        public new event Action<IProjectile> OnExpireE;

        private void Update()
        {
            if(expired)
            {
                return;
            }
            
            if (TimeSinceLaunch > ExpireTime)
            {
                Expire();
            }
        }

        public override void Launch(IPoolItemEmitter sourceEmitter, Vector3 startPosition, Vector3 direction)
        {
            lastLaunchTime = Time.time;

            base.Launch(sourceEmitter, startPosition, direction);
        }
        public void OnHit(IDamagableTarget target)
        {
            if (hitTargets.Contains(target) || expired)
            {
                return;
            }

            hitTargets.Add(target);
            OnHitInternal(target);
        }

        protected override Quaternion CalculateRotation(float angle)
        {
            return base.CalculateRotation(angle + angleOffSet);
        }

        public override void ResetItem()
        {
            hitTargets.Clear();
            base.ResetItem();
        }
        private void OnHitInternal(IDamagableTarget target)
        {
            OnHitE?.Invoke(target);
            
            if (ExpireOnHit)
            {
                Expire();
            }
            
            foreach (IProjectileTargetEffect projectileTargetHitEffect in ProjectileTargetHitEffects)
            {
                projectileTargetHitEffect.Execute(sourceEmitter, target);
            }
        }
    }
}
