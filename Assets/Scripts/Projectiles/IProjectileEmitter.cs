using Projectiles.Implementation;
using UnityEngine;

namespace Projectiles
{
    public interface IProjectileEmitter
    {
        public void EmitProjectile(ProjectileBase projectile, IProjectilePresentation presentation, Vector3 startPosition, Vector3 direction);
    }
}
