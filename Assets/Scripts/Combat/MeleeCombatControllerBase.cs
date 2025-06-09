using BlueRacconGames.Animation;
using System.Collections.Generic;
using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    public abstract class MeleeCombatControllerBase : MonoBehaviour
    {
        [SerializeField] protected Transform attackPoint;
        [SerializeField] private LayerMask hitLayer;
        [SerializeField] private float attackRange = 50f;
        [SerializeField] private Color debugGizmosColor = Color.yellow;

        protected UnitAnimationControllerBase animationController;
        protected List<GameObject> targetsGM = new();
        protected IMeleeWeapon weapon;
        protected bool canAttack = true;

        private Vector2 lastHitPoint;

        protected virtual void Awake()
        {
            animationController = GetComponent<UnitAnimationControllerBase>();
        }

        public virtual void Attack()
        {
            if (!CanAttack())
                return;

            DamageDetected();
        }
        public virtual void DamageDetected()
        {
            Collider2D[] hitedObjects = Physics2D.OverlapCircleAll(attackPoint.position, attackRange * attackPoint.localScale.x, hitLayer);

            if (hitedObjects.Length == 0)
                return;

            foreach (Collider2D hit in hitedObjects)
            {
                if (targetsGM.Contains(hit.gameObject))
                    continue;

                targetsGM.Add(hit.gameObject);

                if (!hit.TryGetComponent<IDamagableTarget>(out var target))
                    continue;

                weapon.OnHit(this, target);
                lastHitPoint = hit.transform.position;
            }
        }
        public virtual void Dizzy(float dizzyTime)
        {

        }
        public void SpawnHitEffect(ParticleSystem vfx)
        {
            Instantiate(vfx, lastHitPoint, Quaternion.identity, null);
        }

        protected virtual bool CanAttack() => weapon != null && canAttack;
        protected void ResetTargets()
        {
            targetsGM.Clear();
            weapon.ResetWeapon();
        }

        private void OnDrawGizmos()
        {
            if (attackPoint == null)
                return;

            Gizmos.color = debugGizmosColor;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange * attackPoint.localScale.x);
        }
    }
}