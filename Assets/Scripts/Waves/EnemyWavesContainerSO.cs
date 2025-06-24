using System.Collections.Generic;
using UnityEngine;

namespace EnemyWaves.Implementation
{
    [CreateAssetMenu(fileName = nameof(EnemyWavesContainerSO), menuName = nameof(EnemyWaves) + "/" + nameof(Implementation) + "/" + nameof(EnemyWavesContainerSO))]
    public class EnemyWavesContainerSO : ScriptableObject
    {
        [field: SerializeField] private List<EnemyWavesContainerData> enemyWavesContainerDatas;
        [field: SerializeField] private DefaultEnemyWaveDataSO DefaultEnemyWave;

        public EnemyWaveFactorySO GetNextWave(int waveId)
        {
            foreach (EnemyWavesContainerData containerData in enemyWavesContainerDatas)
            {
                if (waveId >= containerData.StartRangeWaveId && waveId <= containerData.EndRangeWaveId)
                    return containerData.DefaultEnemyWaveDataSO;
            }

            return DefaultEnemyWave;
        }
    }

    [System.Serializable]
    public class EnemyWavesContainerData
    {
        public int StartRangeWaveId;
        public int EndRangeWaveId;
        public DefaultEnemyWaveDataSO DefaultEnemyWaveDataSO;
    }
}
