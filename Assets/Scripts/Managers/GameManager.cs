using EnemyWaves;
using Game.GameCursor;
using Game.SceneLoader;
using Settings;
using System;
using UnityEngine;
using Zenject;

namespace Game.Managers
{
    public class GameManager : MonoBehaviour
    {
        public event Action OnGameStart;
        public event Action OnGameStoped;
        public event Action OnBackToMenu;
        public event Action OnGamePaused;
        public event Action OnGameResume;
        public event Action OnGameSceneChanged;
        public event Action OnGameSceneLoaded;

        [SerializeField] private SceneDataSo gameplayScene;
        [SerializeField] private SceneDataSo mainMenuScene;

        private SceneLoadManagers sceneLoadManagers;
        private SettingsManager settingsManager;

        public bool IsGamePaused { get; private set; }

        [Inject]
        private void Inject(SceneLoadManagers sceneLoadManagers)
        {
            this.sceneLoadManagers = sceneLoadManagers;
        }

        private void Awake()
        {
            GameStart();
        }

        private void SceneLoadManagers_OnSceneLoaded()
        {
            OnGameSceneLoaded?.Invoke();
        }
        private void SceneLoadManagers_OnSceneChanged()
        {
            OnGameSceneChanged?.Invoke();
        }

        public void BackToMenu()
        {
            OnBackToMenu?.Invoke();

            sceneLoadManagers.LoadLocation(mainMenuScene);
        }
        public void PauseGame()
        {
            CursorManager.UpdateCursorVisable(true);

            OnGamePaused?.Invoke();
        }
        public void GameResume()
        {
            CursorManager.UpdateCursorVisable(false);

            OnGameResume?.Invoke();
        }
        public void LoadGameplayScene()
        {
            sceneLoadManagers.LoadLocation(gameplayScene);
        }
        public void GameStop()
        {
            OnGameStoped?.Invoke();

            Application.Quit();
        }
        private void GameStart()
        {
            sceneLoadManagers.OnSceneChanged += SceneLoadManagers_OnSceneChanged;
            sceneLoadManagers.OnSceneLoaded += SceneLoadManagers_OnSceneLoaded;

            OnGameStart?.Invoke();
        }
    }
}