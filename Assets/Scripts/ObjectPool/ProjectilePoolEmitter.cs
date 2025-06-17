using Projectiles.Implementation;
using UnityEngine;

namespace BlueRacconGames.Pool
{
    public class ProjectilePoolEmitter : PooledEmitterBase
    {
        public void EmitProjectile(ProjectileBase projectilePrefab, DefaultProjectilePresentation projectilePresentationPrefab, Vector3 spawnPoint, Vector3 direction)
        {
            EmitItem<ProjectileBase>(projectilePrefab, spawnPoint, direction);
            //EmitItem<DefaultProjectilePresentation>(projectilePresentationPrefab, spawnPoint, direction);
        }
    }
}