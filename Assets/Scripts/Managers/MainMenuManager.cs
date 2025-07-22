using Settings;
using System;
using UnityEngine;
using Zenject;

namespace Game.Managers
{
    public class MainMenuManager : MonoBehaviour
    {
        public event Action OnMainMenuLoaded;
        public event Action OnNewGameStart;
        public event Action OnMainMenuLeaved;

        private SettingsManager settingsManager;
        private GameManager gameManager;

        [Inject]
        private void Inject(GameManager gameManager, SettingsManager settingsManager)
        {
            this.gameManager = gameManager;
            this.settingsManager = settingsManager;
        }

        private void Start()
        {
            OnMainMenuLoaded?.Invoke();
        }

        public void StartNewGame()
        {
            OnNewGameStart?.Invoke();

            gameManager.LoadGameplayScene();

            LeaveFromMainMenu();
        }

        private void LeaveFromMainMenu()
        {
            OnMainMenuLeaved?.Invoke();
        }

        public void UpdateSoundsEnable()
        {
            settingsManager.SetSfxValue();
        }

        public void UpdateMusicEnable()
        {
            settingsManager.SetMusicValue();
        }
        public void QuitGame()
        {
            gameManager.GameStop();
        }
        public T GetSettingsValue<T>(string key)
        {
            return settingsManager.GetSettingsValue<T>(key);
        }
    }
}