using BlueRacconGames.Pool;
using EnemyWaves.Implementation;
using Game.Difficulty;
using Game.Managers;
using Game.Map;
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
        private MapManager mapManager;
        private GameplayManager gameplayManager;
        private SelectCardManager selectCardManager;
        private IDifficulty difficulty;

        public event Action<IEnemyWaveFactory> OnWaveAddedE;
        public event Action<IEnemyWave> OnWaveSetupedE;
        public event Action OnWaveCompletedE;

        public int TotalWaves { get; private set; }
        public int CurrentWavesId { get; private set; }
        public int EndedWavesCount { get; private set; }

        [Inject]
        private void Inject(UnitPoolEmitter unitSpawner, MapManager mapManager, GameplayManager gameplayManager,
            SelectCardManager selectCardManager)
        {
            this.unitSpawner = unitSpawner;
            this.mapManager = mapManager;
            this.gameplayManager = gameplayManager;
            this.selectCardManager = selectCardManager;

            gameplayManager.OnGameplaySetuped += GameplayManager_OnGameplaySetuped;
        }

        #region Events_Callbacks
        private void GameplayManager_OnGameplayRestart()
        {
            currentWave?.TerminateWave();
            currentWave = null;

            CurrentWavesId = 0;

            wavesQueue.Clear();

            gameplayManager.OnGameplayRestart -= GameplayManager_OnGameplayRestart;
            gameplayManager.OnGameplayEnded -= GameplayManager_OnGameplayEnded;
        }
        private void GameplayManager_OnGameOver()
        {
            gameplayManager.OnGameOver -= GameplayManager_OnGameOver;
            selectCardManager.OnCardSelectedE -= SelectCardManager_OnCardSelected;

            currentWave?.PauseWave();

            gameplayManager.OnGameplayRestart += GameplayManager_OnGameplayRestart;
        }
        private void GameplayManager_OnGameplayEnded()
        {
            GameplayManager_OnGameplayRestart();
            gameplayManager.OnGameplaySetuped -= GameplayManager_OnGameplaySetuped;
        }
        private void GameplayManager_OnGameplaySetuped()
        {
            InitializeGame();
        }
        private void SelectCardManager_OnCardSelected()
        {
            PrepeareNextWave();
        }
        #endregion

        public void InitializeGame()
        {
            difficulty = gameplayManager.GetDifficulty();

            gameplayManager.OnGameOver += GameplayManager_OnGameOver;

            selectCardManager.OnCardSelectedE += SelectCardManager_OnCardSelected;

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
            currentWave.OnSetupedE += EnemyWave_OnSetupedE;
            currentWave.OnStartedE += EnemyWave_OnStartedE;
            currentWave.OnCompletedE += EnemyWave_OnCompletedE;
            currentWave.SetupWave(CurrentWavesId, unitSpawner, enemyUnitPrefab, mapManager.GetMapData(), difficulty, timerPresentation);
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
            Debug.Log(CurrentWavesId);

            AddWaveToQueue(wavesContainer.GetNextWave(CurrentWavesId));

            SetupWave();

            yield return new WaitUntil(NextWaveIsReady);

            StartWave();
        }
        private void EnemyWave_OnSetupedE(IEnemyWave enemyWave)
        {
            OnWaveSetupedE?.Invoke(enemyWave);
        }
        private void EnemyWave_OnStartedE(IEnemyWave enemyWave)
        {
            gameplayManager.GameplayRun();
        }
        private void EnemyWave_OnCompletedE(IEnemyWave enemyWave)
        {
            gameplayManager.GameplayStop();

            OnWaveCompletedE?.Invoke();
        }

        private bool NextWaveIsReady() => currentWave != null && currentWave.IsReady;

        #region DebugMethods
        [ContextMenu("Kill them all!")]
        public void DebugKillAllEnemies()
        {
            if(currentWave == null) return;

            currentWave.CompleteWave();
        }
        #endregion
    }
}