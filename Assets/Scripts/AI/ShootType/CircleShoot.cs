using Projectiles.Implementation;
using UnityEngine;

namespace BlueRacconGames.AI
{
    public class CircleShoot : IShootType
    {
        [SerializeField] private int projectileCount;

        public void Shoot(DefaultProjectileEmitterController projectileEmitterControllerBase, UnityEngine.Transform target)
        {
            float angleStep = 360f / projectileCount;
            float angle = 0f;

            for (int i = 0; i < projectileCount; i++)
            {
                float dirX = Mathf.Cos(angle * Mathf.Deg2Rad);
                float dirY = Mathf.Sin(angle * Mathf.Deg2Rad);
                Vector2 direction = new Vector2(dirX, dirY).normalized;

                projectileEmitterControllerBase.EmitProjectile(direction);

                angle += angleStep;
            }
        }
    }
}
