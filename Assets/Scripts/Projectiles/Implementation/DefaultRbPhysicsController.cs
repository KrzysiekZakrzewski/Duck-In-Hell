using BlueRacconGames.MeleeCombat;
using UnityEngine;

namespace Projectiles.Implementation
{
    public class DefaultRbPhysicsController : PhysicsControllerBase
    {
        protected override IProjectilePhysicsFactory ProjectilePhysicsFactory => new DefaultRbPhysicsFactory(GetComponent<Rigidbody2D>());

        private void OnTriggerEnter2D(Collider2D other)
        {
            IDamagableTarget target = other.gameObject.GetComponent<IDamagableTarget>();
            physics.OnCollide(target);
        }
    }
}
