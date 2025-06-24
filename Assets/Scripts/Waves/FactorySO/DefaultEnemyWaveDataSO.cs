using Game.Difficulty;
using UnityEngine;

namespace EnemyWaves.Implementation
{
    [CreateAssetMenu(fileName = nameof(DefaultEnemyWaveDataSO), menuName = nameof(EnemyWaves) + "/" + nameof(EnemyWaves.Implementation) + "/" + nameof(DefaultEnemyWaveDataSO))]
    public class DefaultEnemyWaveDataSO : EnemyWaveFactorySO
    {
        [field: SerializeField] public WaveEnemyUnitData[] WaveEnemyUnitDatas {  get; private set; }
        public override IEnemyWave CreateEnemyWave()
        {
            return new DefaultEnemyWave(WaveEnemyUnitDatas);
        }
    }
}