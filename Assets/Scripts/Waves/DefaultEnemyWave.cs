using UnityEngine;

namespace EnemyWaves.Implementation
{
    public class DefaultEnemyWave : EnemyWaveBase
    {
        private readonly DefaultEnemyWaveDataSO initialData;

        public override int TotalEnemy => initialData.TotalEnemy;

        public override WaveEnemyUnitData[] WaveEnemyUnitDatas => initialData.WaveEnemyUnitDatas;

        public DefaultEnemyWave(DefaultEnemyWaveDataSO initialData)
        {
            this.initialData = initialData;
        }
    }
}