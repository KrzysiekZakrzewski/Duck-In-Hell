using BlueRacconGames.MeleeCombat;
using Damageable;
using Projectiles;
using UnityEngine;

namespace BasicImplementationSample.Scripts.Effects.ExampleDealDamage
{
    public class ExampleDealDamageEffect : IProjectileTargetEffect
    {
        public void Execute(IProjectileEmitter sourceEmitter, IDamagableTarget target)
        {
            IDamageable damageable = target.GameObject.GetComponent<IDamageable>();

            damageable.TakeDamage(1);//TO DO value set

            Debug.Log($"Damage dealt to {target.GameObject.name}");
        }
    }
}
