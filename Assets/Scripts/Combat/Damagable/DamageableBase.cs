using BlueRacconGames.Animation;
using BlueRacconGames.MeleeCombat;
using BlueRacconGames.Pool;
using Game.CharacterController;
using System;
using System.Collections;
using System.Collections.Generic;
using Units;
using Units.Implementation;
using UnityEngine;
using Zenject;

namespace Damageable.Implementation
{
    [RequireComponent(typeof(IDamagableTarget))]
    public abstract class DamageableBase : MonoBehaviour, IDamageable
    {
        protected DamagableDataSO initialData;
        protected int maxHealth;
        protected bool expired;
        protected bool isImmue;
        protected bool damagableIsOn;
        protected UnitAnimationControllerBase animationController;
        protected CharacterController2D characterController;
        protected Rigidbody2D rb;

        private int currentHealth;
        private bool isDead;
        private List<IExpireEffect> expireEffectsLUT;

        public event Action<int, int> OnTakeDamageE;
        public event Action OnHealE;
        public event Action<IUnit> OnDeadE;
        public event Action<IDamageable> OnExpireE;

        public bool ExpireOnDead => initialData.ExpireOnDead;
        public GameObject GameObject => gameObject;
        public int MaxHealth => maxHealth;
        public int CurrentHealth => currentHealth;
        public bool IsDead => isDead;
        public bool DamagableIsOn => damagableIsOn;

        protected virtual void Awake()
        {
            expireEffectsLUT = new();
            characterController = GetComponent<CharacterController2D>();
            animationController = GetComponent<UnitAnimationControllerBase>();
            rb = GetComponent<Rigidbody2D>();
        }

        public virtual void Launch(DamagableDataSO initialData)
        {
            this.initialData = initialData;
            maxHealth = initialData.MaxHealth;

            foreach (IExpireEffectFactory effectFactory in initialData.ExpireEffectFactorySO)
                expireEffectsLUT.Add(effectFactory.CreateExpireEffect());

            ResetDamagable();
        }
        public void OnDead()
        {
            if (isDead)
                return;

            OnDeadInternal();
        }
        public void TakeDamage(int damageValue, DamageMode damageMode = DamageMode.Normal)
        {
            if (!CanBeDamage(damageMode))
                return;

            TakeDamageInternal(damageValue, damageMode);
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
            isDead = false;
            currentHealth = MaxHealth;
        }
        public virtual void SetDamagableOn(bool value)
        {
            damagableIsOn = value;
        }

        protected virtual IEnumerator ProcessDamage()
        {
            characterController.SetCanMove(false, false);
            animationController.PlayAnimation(initialData.GetHitAnimation);

            yield return new WaitUntil(IsGetHitAnimationPlaying);
            yield return new WaitWhile(IsGetHitAnimationPlaying);

            characterController.SetCanMove(true, false);

            isImmue = false;
        }
        protected virtual void TakeDamageInternal(int damageValue, DamageMode damageMode = DamageMode.Normal)
        {
            currentHealth -= damageValue;

            OnTakeDamageE?.Invoke(currentHealth, maxHealth);

            if(currentHealth <= 0)
            {
                OnDead();
                return;
            }

            StartCoroutine(ProcessDamage());
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
            var pooledEmitter = GetComponent<IUnit>().DefaultPooledEmitter;

            foreach (IExpireEffect effect in expireEffectsLUT)
                effect.Execute(this, pooledEmitter);
        }
        protected bool IsGetHitAnimationPlaying()
        {
            return animationController.IsAnimationPlayingOnLayer(initialData.GetHitAnimation);
        }

        private void OnDeadInternal()
        {
            isDead = true;

            PooledUnitBase unit = gameObject.GetComponent<PooledUnitBase>();

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
        private bool CanBeDamage(DamageMode damageMode)
        {
            if(IsDead || !DamagableIsOn) return false;

            switch (damageMode)
            {
                case DamageMode.Normal:
                    return !isImmue;
                case DamageMode.Passive:
                    return true;
                case DamageMode.IgnoreImmue:
                    return true;
                default:
                    return !isImmue;
            }
        }

        [ContextMenu("OnDamageable")]
        private void OnDamageable()
        {
            damagableIsOn = true;
        }
    }
}
