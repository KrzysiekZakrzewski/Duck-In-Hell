using BlueRacconGames.Pool;
using EnemyWaves.Implementation;
using System;
using System.Collections.Generic;
using Units.Implementation;
using UnityEngine;
using Zenject;

namespace EnemyWaves
{
    public class EnemyWavesManager : MonoBehaviour
    {
        [SerializeField] private EnemyWaveFactorySO testWave;
        [SerializeField] private PooledUnitBase enemyUnitPrefab;

        private readonly Queue<EnemyWaveFactorySO> wavesQueue = new();
        private IEnemyWave currentWave;
        private DefaultPooledEmitter pooledEmitter;

        public event Action<IEnemyWaveFactory> OnWaveAddedE;
        public event Action<IEnemyWave> OnWaveSetupedE;

        public int TotalWaves { get; private set; }
        public int CurrentWavesId { get; private set; }
        public int EndedWavesCount { get; private set; }

        [Inject]
        private void Inject(DefaultPooledEmitter pooledEmitter)
        {
            this.pooledEmitter = pooledEmitter;
        }

        public void AddWaveToQueue(EnemyWaveFactorySO wave)
        {
            if (wavesQueue == null) return;

            wavesQueue.Enqueue(wave);

            OnWaveAddedE?.Invoke(wave);
        }
        public void SetupWave()
        {
            if(wavesQueue.Count == 0) return;

            var nextWaveData = wavesQueue.Dequeue();

            currentWave = nextWaveData.CreateEnemyWave();
            currentWave.SetupWave(pooledEmitter, enemyUnitPrefab);

            OnWaveSetupedE?.Invoke(currentWave);
        }
        [ContextMenu("Start wave")]
        public void StartWave()
        {
            if(currentWave == null) return;

            currentWave.StartWave();
        }

        #region Test Methods
        [ContextMenu("Setup test Wave")]
        private void SetupTestWave()
        {
            wavesQueue.Clear();

            AddWaveToQueue(testWave);

            SetupWave();
        }
        #endregion
    }
}