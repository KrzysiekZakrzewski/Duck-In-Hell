using BlueRacconGames.MeleeCombat;
using UnityEngine;

namespace Projectiles
{
    public interface IProjectilePhysics
    {
        void Initialize(IProjectile projectile);
        void UpdatePhysics();
        void OnCollide(IDamagableTarget target);
    }
}
