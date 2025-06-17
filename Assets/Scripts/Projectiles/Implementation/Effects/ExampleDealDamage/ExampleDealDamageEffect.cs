using BlueRacconGames.MeleeCombat;
using BlueRacconGames.Pool;
using Damageable;
using Projectiles;
using UnityEngine;

namespace BasicImplementationSample.Scripts.Effects.ExampleDealDamage
{
    public class ExampleDealDamageEffect : IProjectileTargetEffect
    {
        public void Execute(IPoolItemEmitter sourceEmitter, IDamagableTarget target)
        {
            IDamageable damageable = target.GameObject.GetComponent<IDamageable>();

            damageable.TakeDamage(1);//TO DO value set

            Debug.Log($"Damage dealt to {target.GameObject.name}");
        }
    }
}
