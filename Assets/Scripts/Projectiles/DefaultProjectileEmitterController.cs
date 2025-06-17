using BlueRacconGames.Pool;
using UnityEngine;
using Zenject;

namespace Projectiles.Implementation
{
    public class DefaultProjectileEmitterController : ProjectileEmitterControllerBase
    {
        [SerializeField] private GameObject projectilePrefabGameObject;
        [SerializeField] private GameObject projectilePresentationPrefabGameObject;

        private ProjectilePoolEmitter projectilePoolEmitter;
        private ProjectileBase projectilePrefab;
        private DefaultProjectilePresentation projectilePresentationPrefab;
        protected override ProjectilePoolEmitter ProjectileEmitter => projectilePoolEmitter;
        protected override ProjectileBase ProjectilePrefab => projectilePrefab;
        protected override DefaultProjectilePresentation ProjectilePresentationPrefab => projectilePresentationPrefab;

        public void Launch(ProjectilePoolEmitter projectilePoolEmitter)
        {
            projectilePrefab = projectilePrefabGameObject.GetComponent<ProjectileBase>();
            projectilePresentationPrefab = projectilePresentationPrefabGameObject.GetComponent<DefaultProjectilePresentation>();
            this.projectilePoolEmitter = projectilePoolEmitter;
        }
    }
}
