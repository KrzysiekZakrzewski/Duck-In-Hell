using BlueRacconGames;
using BlueRacconGames.MeleeCombat;
using System;
using System.Collections.Generic;

namespace Projectiles
{
    public interface IProjectile : IGameObject
    {
        float Speed { get; }
        float ExpireTime { get; }
        bool ExpireOnHit { get; }
        List<IProjectileTargetEffect> ProjectileTargetHitEffects { get; }
        event Action<IProjectile> OnLaunchE;
        event Action<IDamagableTarget> OnHitE;
        event Action<IProjectile> OnExpireE;
        void OnHit(IDamagableTarget target);
    }
}
