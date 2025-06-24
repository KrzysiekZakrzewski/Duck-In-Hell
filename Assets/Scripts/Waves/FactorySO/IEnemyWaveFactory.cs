using Game.Difficulty;

namespace EnemyWaves
{
    public interface IEnemyWaveFactory
    {
        IEnemyWave CreateEnemyWave();
    }
}