using UnityEngine;

namespace Projectiles
{
    public interface IProjectileEmitter
    {
        public void EmitProjectile(IProjectile projectile, IProjectilePresentation presentation, Vector2 startPosition, Vector2 direction);
    }
}
