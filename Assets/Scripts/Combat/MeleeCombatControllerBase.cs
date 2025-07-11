using BlueRacconGames.Animation;
using BlueRacconGames.Cards;
using BlueRacconGames.Pool;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BlueRacconGames.MeleeCombat
{
    public abstract class MeleeCombatControllerBase : MonoBehaviour
    {
        public event Action<bool> OnAttackEnableChanged;

        [SerializeField] protected bool canUseCardsEffect;
        [SerializeField] protected Transform attackPoint;
        [SerializeField] private LayerMask hitLayer;
        [SerializeField] private float baseAttackRange = 50f;
        [SerializeField] private Color debugGizmosColor = Color.yellow;

        protected CardsController cardController;
        protected DefaultPooledEmitter pooledEmitter;
        protected UnitAnimationControllerBase animationController;
        protected List<GameObject> targetsGM = new();
        protected IMeleeWeapon weapon;
        protected bool attackEnable = true;
        protected float attackRange;
        protected float attackRangeMultiplier = 1f;

        public Vector3 AttackPosition => attackPoint.position;

        public DefaultPooledEmitter PooledEmitter => pooledEmitter;

        public virtual void Initialize(DefaultPooledEmitter pooledEmitter, CardsController cardController)
        {
            animationController = GetComponent<UnitAnimationControllerBase>();
            this.pooledEmitter = pooledEmitter;
            this.cardController = cardController;
            attackRange = baseAttackRange;
            attackRangeMultiplier = 1f;
        }
        public virtual void Attack()
        {
            if (!CanAttack())
                return;

            weapon.OnAttack(this);

            DamageDetected();
        }
        public virtual void DamageDetected()
        {
            Collider2D[] hitedObjects = Physics2D.OverlapCircleAll(attackPoint.position, GetDamageRadius(), hitLayer);

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
            }
        }
        public void ExecuteCards(IDamagableTarget target)
        {
            if(!CanExecuteCards()) return;

            cardController.ExecutePassiveHitEffects(target);
        }
        public bool CanExecuteCards() => cardController != null;
        public void UpdateAttackEnable(bool value)
        {
            if (attackEnable == value) return;

            attackEnable = value;

            InternalUpdateAttackEnable();
        }
        public float GetDamageRadius() => attackRange * attackRangeMultiplier * attackPoint.localScale.x;
        public void IncreaseAttackRange(float value) => attackRange += value;
        public void DecreaseAttackRange(float value) => attackRange -= value;
        public void IncreaseAttackRangeMultiplier(float value) => attackRangeMultiplier += value;
        public void DecreaseAttackRangeMultiplier(float value) => attackRangeMultiplier -= value;

        protected virtual void InternalUpdateAttackEnable()
        {
            OnAttackEnableChanged?.Invoke(attackEnable);
        }
        protected virtual bool CanAttack() => weapon != null && attackEnable;
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
            Gizmos.DrawWireSphere(attackPoint.position, GetDamageRadius());
        }
    }
}