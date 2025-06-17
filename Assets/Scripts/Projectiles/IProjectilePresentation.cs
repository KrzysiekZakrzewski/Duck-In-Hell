using BlueRacconGames;
using BlueRacconGames.MeleeCombat;
using System;

namespace Projectiles
{
    public interface IProjectilePresentation : IGameObject
    {
        event Action<IProjectilePresentation> OnPresentationEnd;
        void Initialize(IProjectile projectile);
        void OnHit(IDamagableTarget target);
    }
}
