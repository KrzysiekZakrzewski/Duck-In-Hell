using BlueRacconGames.Pool;
using Damageable;

namespace BlueRacconGames.MeleeCombat
{
    public interface IExpireEffect
    {
        void Execute(IDamageable damageable, DefaultPooledEmitter defaultPooledEmitter);
    }
}
