using Damageable;
using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    public class DealDamageTargetEffect : IMeleeTargetEffect
    {
        private int damageValue;

        public DealDamageTargetEffect(int damageValue)
        {
            this.damageValue = damageValue;
        }

        public void Execute(MeleeCombatControllerBase source, IDamagableTarget target)
        {
            IDamageable damageable = target.GameObject.GetComponent<IDamageable>();

            damageable?.TakeDamage(damageValue, out var isFatalDamage);
        }
    }
}