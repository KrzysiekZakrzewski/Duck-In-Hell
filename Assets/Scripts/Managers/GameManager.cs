using EnemyWaves;
using Game.Difficulty;
using Game.SceneLoader;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Game.Managers
{
    public class GameManager : MonoBehaviour
    {
        #region Events
        public event Action OnGameStartSetup;
        public event Action OnGameSetuped;
        public event Action OnGameStartRun;
        public event Action OnGameRun;
        public event Action OnGameStoped;
        public event Action OnGameEnded;
        public event Action OnGameStartRestart;
        public event Action OnGameRestarted;
        public event Action OnGamePaused;
        public event Action OnGameUnPaused;
        #endregion

        [SerializeField] private DefaultDifficultyFactorySO difficultyFactorySO;
        [SerializeField] private SceneDataSo mainMenuScene;

        private EnemyWavesManager wavesManager;
        private SelectCardManager selectCardManager;
        private SceneLoadManagers sceneLoadManagers;
        private IDifficulty difficulty;

        [Inject]
        private void Inject(EnemyWavesManager wavesManager, SelectCardManager selectCardManager
            )//SceneLoadManagers sceneLoadManagers
        {
            this.wavesManager = wavesManager;
            this.selectCardManager = selectCardManager;
            //this.sceneLoadManagers = sceneLoadManagers;
        }

        private void Start()
        {
            SetupGame();
        }

        public void GameOver()
        {
            OnGameEnded?.Invoke();
        }
        public void RunGame()
        {
            OnGameRun?.Invoke();
        }
        public void StopGame()
        {
            OnGameStoped?.Invoke();
        }
        public void RestartGame()
        {
            OnGameStartRestart?.Invoke();
        }
        public void EndGame()
        {
            OnGameEnded?.Invoke();

            sceneLoadManagers.LoadLocation(mainMenuScene);
        }

        private void SetupGame()
        {
            OnGameStartSetup?.Invoke();

            difficulty = difficultyFactorySO.Create();

            StartCoroutine(SetupEndlesGameMode());

            OnGameSetuped?.Invoke();
        }
        private IEnumerator SetupEndlesGameMode()
        {
            yield return new WaitForSeconds(2f);

            selectCardManager.OnCardSelectedE += wavesManager.PrepeareNextWave;

            wavesManager.InitializeGameMode(difficulty);
        }
    }
}