using BlueRacconGames.MeleeCombat;
using BlueRacconGames.Pool;

namespace Projectiles
{
    public interface IProjectileTargetEffect
    {
        void Execute(IPoolItemEmitter sourceEmitter, IDamagableTarget target);
    }
}
