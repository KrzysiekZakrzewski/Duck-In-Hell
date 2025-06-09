using UnityEngine;
using Zenject;

namespace Damageable.Implementation
{
    public class PlayerDamagable : DamageableBase
    {
        private PlayerDamagableDataSO damagableData;

        public override bool ExpireOnDead => damagableData.ExpireOnDead;

        public override void Launch(IDamagableDataSO damagableDataSO)
        {
            damagableData = damagableDataSO as PlayerDamagableDataSO;

            base.Launch(damagableDataSO);

            //healthBar.Launch(CurrentHealth, MaxHealth);
        }

        protected override void HealInternal(int healValue)
        {
            base.HealInternal(healValue);

            //healthBar.UpdateBar(CurrentHealth, MaxHealth);
        }

        protected override void TakeDamageInternal(int damageValue)
        {
            base.TakeDamageInternal(damageValue);

            //healthBar.UpdateBar(CurrentHealth, MaxHealth);

            StartCoroutine(GetHitSequence());
        }

        protected override void IncreaseHealtInternal(int increaseValue)
        {
            base.IncreaseHealtInternal(increaseValue);

            //healthBar.UpdateBar(CurrentHealth, MaxHealth);
        }

        protected override void OnExpireInternal()
        {
            if (expired) return;

            expired = true;
        }
    }
}