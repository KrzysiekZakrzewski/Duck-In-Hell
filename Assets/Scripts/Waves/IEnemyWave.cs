using BlueRacconGames.Pool;
using System;
using Units;
using Units.Implementation;
using UnityEngine;

namespace EnemyWaves
{
    public interface IEnemyWave
    {
        WaveEnemyUnitData[] WaveEnemyUnitDatas {  get; }
        int TotalEnemy {  get; }
        int EnemyRemain {  get; }
        bool IsStarted { get; }
        bool IsCompleted {  get; }

        event Action<IEnemyWave> OnSetupedE;
        event Action<IEnemyWave> OnStartedE;
        event Action<IEnemyWave> OnUpdatedE;
        event Action<IEnemyWave> OnCompletedE;

        void SetupWave(DefaultPooledEmitter defaultPooledEmitter, PooledEnemyUnit enemyUnitPrefab);
        void StartWave();
        void UpdateWave(IUnit unit);
        void CompleteWave();
    }

    [System.Serializable]
    public class WaveEnemyUnitData
    {
        [field: SerializeField, Range(0f, 1f)] public float PercentChance { get; private set; }
        [field: SerializeField] public PooledEnemyUnitDataSO EnemyUnitDataSO { get; private set; }
    }
}