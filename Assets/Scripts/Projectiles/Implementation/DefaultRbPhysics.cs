using BlueRacconGames.MeleeCombat;
using Extensions;
using System;
using UnityEngine;

namespace Projectiles.Implementation
{
    [Serializable]
    public class DefaultRbPhysics : IProjectilePhysics
    {
        private Rigidbody2D rb;
        private IProjectile projectile;
        
        public DefaultRbPhysics(IProjectile projectile, Rigidbody2D rb)
        {
            this.rb = rb;
            Initialize(projectile);
        }

        public void Initialize(IProjectile projectile)
        {
            this.projectile = projectile;
        }

        public void UpdatePhysics()
        {
            rb.MovePosition(rb.transform.position + projectile.Speed * Time.fixedDeltaTime * rb.transform.up);
        }

        public void OnCollide(IDamagableTarget target)
        {
            if (target == null || !CheckTargetLayer(target)) return;
            
            projectile.OnHit(target);
        }

        private bool CheckTargetLayer(IDamagableTarget target) => projectile.TargetLayer.Includes(target.GameObject.layer);
    }
}
