using BlueRacconGames.MeleeCombat;
using Damageable;
using Projectiles;
using UnityEngine;

namespace BasicImplementationSample.Scripts.Effects
{
    public class DealDamageEffect : IProjectileTargetEffect
    {
        private ProjectileDealDamageDataSO initialData;

        public DealDamageEffect(ProjectileDealDamageDataSO initialData)
        {
            this.initialData = initialData;
        }

        public void Execute(IProjectile source, IDamagableTarget target)
        {
            IDamageable damageable = target.GameObject.GetComponent<IDamageable>();

            damageable.TakeDamage(initialData.GetDamageValue(), initialData.DamageType);

            Debug.Log($"Damage dealt to {target.GameObject.name}");
        }
    }
}