using Projectiles.Implementation;
using UnityEngine;

namespace BlueRacconGames.AI
{
    public class SimpleSingleShot : IShootType
    {
        public virtual void Shoot(DefaultProjectileEmitterController projectileEmitterControllerBase, Transform target)
        {
            projectileEmitterControllerBase.EmitProjectile(TargetAngle(target.position, projectileEmitterControllerBase));
        }

        private Vector2 TargetAngle(Vector3 targetPoint, DefaultProjectileEmitterController projectileEmitterControllerBase)
        {
            return (targetPoint - projectileEmitterControllerBase.ProjectileSpawnPoint.position).normalized;
        }
    }
}
