using BlueRacconGames.Pool;
using EnemyWaves.Implementation;
using Game.Difficulty;
using System;
using System.Collections;
using System.Collections.Generic;
using Timers;
using Units.Implementation;
using UnityEngine;
using Zenject;

namespace EnemyWaves
{
    public class EnemyWavesManager : MonoBehaviour
    {
        [SerializeField] private EnemyWavesContainerSO wavesContainer;
        [SerializeField] private PooledUnitBase enemyUnitPrefab;
        [SerializeField] private BaseCountdownPresentation timerPresentation;

        private readonly Queue<EnemyWaveFactorySO> wavesQueue = new();
        private IEnemyWave currentWave;
        private DefaultPooledEmitter pooledEmitter;
        private IDifficulty difficulty;

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

        public void Initialize(IDifficulty difficulty)
        {
            this.difficulty = difficulty;

            StartCoroutine(PrepeareNextWaveSequnce());
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

            CurrentWavesId++;

            var nextWaveData = wavesQueue.Dequeue();

            currentWave = nextWaveData.CreateEnemyWave();
            currentWave.SetupWave(CurrentWavesId, pooledEmitter, enemyUnitPrefab, difficulty, timerPresentation);

            OnWaveSetupedE?.Invoke(currentWave);
        }
        public void StartWave()
        {
            if(currentWave == null) return;

            currentWave.StartWave();
        }

        private IEnumerator PrepeareNextWaveSequnce()
        {
            AddWaveToQueue(wavesContainer.GetNextWave(CurrentWavesId));

            yield return new WaitForSeconds(1f);

            SetupWave();

            yield return new WaitForSeconds(2f);

            StartWave();
        }
    }
}