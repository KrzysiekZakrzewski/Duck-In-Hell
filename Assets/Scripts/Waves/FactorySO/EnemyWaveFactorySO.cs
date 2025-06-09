using UnityEngine;

namespace EnemyWaves.Implementation
{
    public abstract class EnemyWaveFactorySO : ScriptableObject, IEnemyWaveFactory
    {
        public abstract IEnemyWave CreateEnemyWave();
    }
}