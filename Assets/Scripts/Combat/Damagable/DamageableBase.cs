using BlueRacconGames.Animation;
using BlueRacconGames.MeleeCombat;
using Game.CharacterController;
using System;
using System.Collections;
using Units;
using Units.Implementation;
using UnityEngine;

namespace Damageable.Implementation
{
    [RequireComponent(typeof(IDamagableTarget))]
    public abstract class DamageableBase : MonoBehaviour, IDamageable
    {
        protected int maxHealth;
        protected bool expired;
        protected bool isImmune;
        protected UnitAnimationControllerBase animationController;
        protected CharacterController2D characterController;
        protected Rigidbody2D rb;
        private int currentHealth;
        private bool dead;

        public abstract bool ExpireOnDead { get; }

        public GameObject GameObject => gameObject;
        public int MaxHealth => maxHealth;
        public int CurrentHealth => currentHealth;
        public bool Dead => dead;
        public bool IsImmune => isImmune;

        public event Action<int, int> OnTakeDamageE;
        public event Action OnHealE;
        public event Action<IUnit> OnDeadE;
        public event Action<IDamageable> OnExpireE;

        protected virtual void Awake()
        {
            characterController = GetComponent<CharacterController2D>();
            animationController = GetComponent<UnitAnimationControllerBase>();
            rb = GetComponent<Rigidbody2D>();
        }

        public virtual void Launch(IDamagableDataSO damagableDataSO)
        {
            maxHealth = damagableDataSO.MaxHealth;
            ResetDamagable();
        }
        public void OnDead()
        {
            if (dead)
                return;

            OnDeadInternal();
        }
        public void TakeDamage(int damageValue)
        {
            if (dead || isImmune)
                return;

            TakeDamageInternal(damageValue);
        }
        public void Heal(int healValue)
        {
            if (currentHealth >= MaxHealth)
                return;

            HealInternal(healValue);
        }
        public void IncreaseHealt(int increaseValue)
        {
            IncreaseHealtInternal(increaseValue);
        }
        public void DecreaseHealt(int decreaseValue)
        {
            DecreaseHealtInternal(decreaseValue);
        }
        public void ResetDamagable()
        {
            expired = false;
            dead = false;
            currentHealth = MaxHealth;
        }

        protected virtual IEnumerator GetHitSequence()
        {
            characterController.SetCanMove(false);
            isImmune = true;
            animationController.GetHitAnimation();
            Knockback(0.1f);
            //yield return new WaitUntil(() => animationController.GetBoolParameter("DamageDetect"));
            yield return new WaitForSeconds(0.5f);
            //yield return new WaitWhile(() => animationController.GetBoolParameter("DamageDetect"));

            characterController.SetCanMove(true);
            isImmune = false;
        }
        protected virtual void TakeDamageInternal(int damageValue)
        {
            currentHealth -= damageValue;

            OnTakeDamageE?.Invoke(currentHealth, maxHealth);

            if (currentHealth > 0)
            {
                StartCoroutine(GetHitSequence());
                return;
            }

            OnDead();
        }
        protected virtual void Knockback(float force)
        {
            characterController.SetCanMove(false);

            if (rb == null) return;

            rb.linearVelocity = Vector2.zero;
            rb.AddForceX((transform.localScale.x * force) * -1, ForceMode2D.Impulse);
        }
        protected virtual void HealInternal(int healValue)
        {
            currentHealth += healValue;

            OnHealE?.Invoke();

            currentHealth = currentHealth > MaxHealth ? MaxHealth : currentHealth;
        }
        protected virtual void IncreaseHealtInternal(int increaseValue)
        {
            maxHealth += increaseValue;

            currentHealth = maxHealth;
        }
        protected virtual void DecreaseHealtInternal(int decreaseValue)
        {
            maxHealth -= decreaseValue;

            currentHealth = maxHealth < currentHealth ? maxHealth : currentHealth;
        }
        protected virtual void OnExpireInternal()
        {

        }

        private void OnDeadInternal()
        {
            dead = true;

            PooledEnemyUnit unit = gameObject.GetComponent<PooledEnemyUnit>();

            OnDeadE?.Invoke(unit);

            OnExpire();
        }
        private void OnExpire()
        {
            if (!ExpireOnDead || expired) return;

            expired = true;

            OnExpireInternal();

            OnExpireE?.Invoke(this);
        }

        public virtual void SetImmune(bool value)
        {
            isImmune = value;
        }
    }
}
