using BlueRacconGames.MeleeCombat;
using UnityEngine;

namespace Projectiles.Effects
{
    public class KnockbackEffect : IProjectileTargetEffect
    {
        private float forceMagnitude = 0.2f;
        public void Execute(IProjectile source, IDamagableTarget target)
        {
            var direction = (target.GameObject.transform.position - source.GameObject.transform.position).normalized;

            var rb = target.GameObject.GetComponent<Rigidbody2D>();

            rb.AddForce(direction * forceMagnitude, ForceMode2D.Impulse);
        }
    }
}
