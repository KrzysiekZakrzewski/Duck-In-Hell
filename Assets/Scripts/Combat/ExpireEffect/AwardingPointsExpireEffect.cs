using BlueRacconGames.Pool;
using Damageable;
using Game.Points;

namespace BlueRacconGames.MeleeCombat
{
    public class AwardingPointsExpireEffect : IExpireEffect
    {
        private int points;

        public AwardingPointsExpireEffect(int points)
        {
            this.points = points;
        }

        public void Execute(IDamageable damageable, DefaultPooledEmitter defaultPooledEmitter)
        {
            GameplayPointsManager.AddPoints(points);
        }
    }
}