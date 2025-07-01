using BlueRacconGames.MeleeCombat;
using BlueRacconGames.Pool;
using Damageable;
using Projectiles;
using UnityEngine;

namespace BasicImplementationSample.Scripts.Effects.ExampleDealDamage
{
    public class DealDamageEffect : IProjectileTargetEffect
    {
        public void Execute(IProjectile source, IDamagableTarget target)
        {
            IDamageable damageable = target.GameObject.GetComponent<IDamageable>();

            damageable.TakeDamage(1, out var isFatalDamage);//TO DO value set

            Debug.Log($"Damage dealt to {target.GameObject.name}");
        }
    }
}
