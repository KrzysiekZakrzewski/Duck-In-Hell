using BlueRacconGames.MeleeCombat;

namespace Projectiles
{
    public interface IProjectileTargetEffect
    {
        void Execute(IProjectileEmitter sourceEmitter, IDamagableTarget target);
    }
}
