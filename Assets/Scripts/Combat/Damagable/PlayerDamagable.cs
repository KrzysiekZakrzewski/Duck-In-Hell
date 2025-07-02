using UnityEngine;
using Zenject;

namespace Damageable.Implementation
{
    public class PlayerDamagable : DamageableBase
    {
        public override void Launch(DamagableDataSO initialData)
        {
            this.initialData = initialData as PlayerDamagableDataSO;

            base.Launch(initialData);

            //healthBar.Launch(CurrentHealth, MaxHealth);
        }

        protected override void HealInternal(int healValue)
        {
            base.HealInternal(healValue);

            //healthBar.UpdateBar(CurrentHealth, MaxHealth);
        }

        protected override void TakeDamageInternal(int damageValue, DamageMode damageMode)
        {
            base.TakeDamageInternal(damageValue, damageMode);

            //healthBar.UpdateBar(CurrentHealth, MaxHealth);

            StartCoroutine(ProcessDamage());
        }

        protected override void IncreaseHealtInternal(int increaseValue)
        {
            base.IncreaseHealtInternal(increaseValue);

            //healthBar.UpdateBar(CurrentHealth, MaxHealth);
        }
    }
}