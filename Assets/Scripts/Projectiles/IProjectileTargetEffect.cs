using BlueRacconGames.MeleeCombat;

namespace Projectiles
{
    public interface IProjectileTargetEffect
    {
        void Execute(IProjectile source, IDamagableTarget target);
    }
}
