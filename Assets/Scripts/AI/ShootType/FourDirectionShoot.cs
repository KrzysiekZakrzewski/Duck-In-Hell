using Projectiles.Implementation;
using UnityEngine;

namespace BlueRacconGames.AI
{
    public class FourDirectionShoot : IShootType
    {
        private readonly Vector2[] directions =
            {
                Vector2.up,
                Vector2.down,
                Vector2.left,
                Vector2.right
            };

        public void Shoot(DefaultProjectileEmitterController projectileEmitterControllerBase, Transform target)
        {
            foreach (var direction in directions)
            {
                projectileEmitterControllerBase.EmitProjectile(direction);
            }
        }
    }
}