using EnemyWaves;
using Game.Difficulty;
using Game.GameCursor;
using Game.Points;
using Game.SceneLoader;
using RDG.Platforms;
using System;
using System.Collections;
using TimeTickSystems;
using UnityEngine;
using Zenject;

namespace Game.Managers
{
    public class GameplayManager : MonoBehaviour
    {
        #region Events
        public event Action OnGameplaySetup;
        public event Action OnGameplaySetuped;
        public event Action OnGameOver;
        public event Action OnGameplayRestart;
        public event Action OnGamePaused;
        public event Action OnGameUnPaused;
        public event Action OnGameplayRun;
        public event Action OnGameplayStop;
        public event Action OnGameplayEnded;
        #endregion

        #region Veriables
        [SerializeField] private DefaultDifficultyFactorySO difficultyFactorySO;

        private GameManager gameManager;
        private IDifficulty difficulty;
        #endregion

        [Inject]
        private void Inject(GameManager gameManager, ScoreSaveVersionManager scoreSaveVersionManager)
        {
            this.gameManager = gameManager;

            scoreSaveVersionManager.SetGameplayManager(this);
            gameManager.OnGameSceneChanged += GameManager_OnGameSceneChanged;
        }

        #region EventCallbacks
        private void GameManager_OnGameSceneChanged()
        {
            gameManager.OnGameSceneChanged -= GameManager_OnGameSceneChanged;
            SetupGameplay();
        }
        #endregion

        #region PublicMethods
        public void GameOver()
        {
            CursorManager.UpdateCursorVisable(true);

            OnGameOver?.Invoke();
        }
        public void GameplayRun()
        {
            OnGameplayRun?.Invoke();
        }
        public void GameplayStop()
        {
            OnGameplayStop?.Invoke();
        }
        public void RestartGame()
        {
            TimeTickSystem.ResetTickSystem();

            OnGameplayRestart?.Invoke();

            CursorManager.UpdateCursorVisable(false);

            SetupGameplay();
        }
        public void GameplayEnd()
        {
            CorutineSystem.ForceStopAllCorutines();

            OnGameplayEnded?.Invoke();

            gameManager.BackToMenu();
        }
        public IDifficulty GetDifficulty() => difficulty ??= difficultyFactorySO.Create();
        #endregion

        #region Private Methods
        private void SetupGameplay()
        {
            OnGameplaySetup?.Invoke();

            GameplayPointsManager.ResetScore();

            difficulty = difficultyFactorySO.Create();

            StartCoroutine(SetupEndlesGameMode());
        }
        private IEnumerator SetupEndlesGameMode()
        {
            yield return new WaitForSeconds(2f);

            OnGameplaySetuped?.Invoke();
        }
        #endregion
    }
}