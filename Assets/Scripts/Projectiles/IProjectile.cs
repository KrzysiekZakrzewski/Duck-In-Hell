using BlueRacconGames;
using BlueRacconGames.MeleeCombat;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Projectiles
{
    public interface IProjectile : IGameObject
    {
        LayerMask TargetLayer { get; }
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
