using BlueRacconGames.Pool;
using UnityEngine;

namespace Projectiles.Implementation
{
    public abstract class ProjectileEmitterControllerBase : MonoBehaviour
    {
        [SerializeField] private Transform projectileSpawnPoint;
        
        protected abstract ProjectilePoolEmitter ProjectileEmitter { get; }
        protected abstract ProjectileBase ProjectilePrefab { get; }
        protected abstract DefaultProjectilePresentation ProjectilePresentationPrefab { get; }

        public Transform ProjectileSpawnPoint => projectileSpawnPoint;

        public void EmitProjectile(Vector3 direction)
        {
            ProjectileEmitter.EmitProjectile(ProjectilePrefab, ProjectilePresentationPrefab, projectileSpawnPoint.position, direction);
        }
    }
}