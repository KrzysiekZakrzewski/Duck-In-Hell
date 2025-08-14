using Projectiles.Implementation;
using UnityEngine;

namespace BlueRacconGames.AI
{
    public class RandomDirectionShoot : IShootType
    {
        [SerializeField] private int projectileCount;
        [SerializeField] private float maxAngle = 360f;

        public void Shoot(DefaultProjectileEmitterController projectileEmitterControllerBase, Transform target)
        {
            for(int i = 0 ; i < projectileCount; i++)
            {
                float angle = Random.Range(0f, maxAngle);
                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                float y = Mathf.Sin(angle * Mathf.Deg2Rad);
                Vector2 direction = new Vector2(x, y).normalized;

                projectileEmitterControllerBase.EmitProjectile(direction);
            }
        }
    }
}