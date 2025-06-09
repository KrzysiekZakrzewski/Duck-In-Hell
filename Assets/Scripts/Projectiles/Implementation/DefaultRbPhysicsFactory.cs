using UnityEngine;

namespace Projectiles.Implementation
{
    public class DefaultRbPhysicsFactory : IProjectilePhysicsFactory
    {
        private readonly Rigidbody2D rb;
        
        public DefaultRbPhysicsFactory(Rigidbody2D rb)
        {
            this.rb = rb;
        }

        public IProjectilePhysics Create(IProjectile projectile)
        {
            return new DefaultRbPhysics(projectile, rb);
        }
    }
}
