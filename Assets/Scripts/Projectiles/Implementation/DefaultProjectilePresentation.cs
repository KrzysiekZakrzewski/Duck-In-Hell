using BlueRacconGames.MeleeCombat;
using BlueRacconGames.Pool;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Events;

namespace Projectiles.Implementation
{
    [Serializable]
    public class DefaultProjectilePresentation : PoolItemBase, IProjectilePresentation
    {
        [SerializeField] private UnityEvent onLaunch;
        [SerializeField] private UnityEvent onHit;
        [SerializeField] private UnityEvent onExpire;
        [SerializeField] private float timeToEndPresentationAfterExpire = 5f;
        
        private ParentConstraint parentConstraint;
        private IProjectile projectile;

        public event Action<IProjectilePresentation> OnPresentationEnd;

        private void Awake()
        {
            parentConstraint = GetComponent<ParentConstraint>();
        }

        public void Initialize(IProjectile projectile)
        {
            this.projectile = projectile;
            projectile.OnExpireE += (IProjectile projectile) => Expire();
            projectile.OnHitE += OnHit;
            projectile.OnLaunchE += (IProjectile projectile) => OnLaunch();
        }
        public void OnLaunch()
        {
            gameObject.SetActive(true);
            transform.SetPositionAndRotation(projectile.GameObject.transform.position, projectile.GameObject.transform.rotation);
            parentConstraint.AddSource(new ConstraintSource()
            {
                sourceTransform = projectile.GameObject.transform,
                weight = 1f
            });
            onLaunch?.Invoke();
            parentConstraint.constraintActive = true;
            projectile.OnLaunchE -= (IProjectile projectile) => OnLaunch();
        }
        public void OnHit(IDamagableTarget target)
        {
            onHit?.Invoke();
        }

        protected override void ExpireInternal()
        {
            base.ExpireInternal();

            onExpire?.Invoke();
            parentConstraint.RemoveSource(0);
            parentConstraint.constraintActive = false;
            StartCoroutine(EndPresentationAfterExpire());
            projectile.OnExpireE -= (IProjectile projectile) => Expire();
            projectile.OnHitE -= OnHit;
            projectile.OnLaunchE -= (IProjectile projectile) => OnLaunch();
        }

        private IEnumerator EndPresentationAfterExpire()
        {
            yield return new WaitForSeconds(timeToEndPresentationAfterExpire);
            OnPresentationEnd?.Invoke(this);
            gameObject.SetActive(false);
        }        
    }
}
