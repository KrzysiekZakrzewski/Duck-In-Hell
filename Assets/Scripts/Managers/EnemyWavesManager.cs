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
        private UnitPoolEmitter unitSpawner;
        private IDifficulty difficulty;

        public event Action<IEnemyWaveFactory> OnWaveAddedE;
        public event Action<IEnemyWave> OnWaveSetupedE;

        public int TotalWaves { get; private set; }
        public int CurrentWavesId { get; private set; }
        public int EndedWavesCount { get; private set; }

        [Inject]
        private void Inject(UnitPoolEmitter unitSpawner)
        {
            this.unitSpawner = unitSpawner;
        }

        public void InitializeGameMode(IDifficulty difficulty)
        {
            this.difficulty = difficulty;

            PrepeareNextWave();
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
            currentWave.OnSetupedE += OnWaveSetupedE;
            currentWave.SetupWave(CurrentWavesId, unitSpawner, enemyUnitPrefab, difficulty, timerPresentation);
        }
        public void StartWave()
        {
            if(currentWave == null) return;

            currentWave.StartWave();
        }
        public void PrepeareNextWave()
        {
            StartCoroutine(PrepeareAutoNextWaveSequence());
        }

        private IEnumerator PrepeareAutoNextWaveSequence()
        {
            yield return new WaitForSeconds(2f);

            AddWaveToQueue(wavesContainer.GetNextWave(CurrentWavesId));

            SetupWave();

            yield return new WaitUntil(NextWaveIsReady);

            StartWave();
        }
        private bool NextWaveIsReady() => currentWave != null && currentWave.IsReady;
    }
}