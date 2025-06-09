using BlueRacconGames.MeleeCombat;
using UnityEngine;

namespace Projectiles.Implementation
{
    public class DefaultRbPhysicsController : PhysicsControllerBase
    {
        protected override IProjectilePhysicsFactory ProjectilePhysicsFactory => new DefaultRbPhysicsFactory(GetComponent<Rigidbody2D>());

        private void OnTriggerEnter(Collider other)
        {
            IDamagableTarget target = other.GetComponent<IDamagableTarget>();
            physics.OnCollide(target);
        }
    }
}
