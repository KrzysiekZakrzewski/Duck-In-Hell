using Damageable;
using System;
using UnityEngine;

namespace BlueRacconGames.AI
{
    public abstract class AttackAIModule : IAIModule
    {
        protected bool attackEnabled;
        protected AIControllerBase aIController;

        public event Action OnAttackEnabled;
        public event Action OnAttackDisabled;

        protected virtual void Damageable_OnExpireE(IDamageable damageable)
        {
            damageable.OnExpireE -= Damageable_OnExpireE;
        }

        public virtual void Initialize(AIControllerBase aIController)
        {
            this.aIController = aIController;

            var damageable = aIController.GetComponent<IDamageable>();

            damageable.OnExpireE += Damageable_OnExpireE;

            EnableAttack();
        }
        public virtual void DeInitialize(AIControllerBase aIController)
        {
            DisableAttack();

            var damageable = aIController.GetComponent<IDamageable>();

            damageable.OnExpireE -= Damageable_OnExpireE;
        }
        public void EnableAttack()
        {
            if (attackEnabled) return;

            attackEnabled = true;

            EnableAttackInternal();

            OnAttackEnabled?.Invoke();
        }
        public void DisableAttack()
        {
            if (!attackEnabled) return;

            attackEnabled = false;

            DisableAttackInternal();

            OnAttackDisabled?.Invoke();
        }

        protected virtual void EnableAttackInternal()
        {

        }
        protected virtual void DisableAttackInternal()
        {

        }
    }
}