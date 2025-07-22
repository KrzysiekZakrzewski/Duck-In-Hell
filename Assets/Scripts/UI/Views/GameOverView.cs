using BlueRacconGames.UI;
using DG.Tweening;
using Game.Managers;
using Game.Points;
using TMPro;
using UnityEngine;
using Zenject;

namespace ViewSystem.Implementation
{
    public class GameOverView : BasicView
    {
        [SerializeField] private TextMeshProUGUI highScoreTxt;
        [SerializeField] private TextMeshProUGUI yourScoreTxt;
        [SerializeField] private TextMeshProUGUI newHighScorePrefixTxt;
        [SerializeField] private UIButtonBase retryButton;
        [SerializeField] private UIButtonBase mainMenuButton;

        private GameplayManager gameplayManager;
        private readonly float fadeInDuration = 0.33f;

        public override bool Absolute => false;

        [Inject]
        private void Inject(GameplayManager gameplayManager)
        {
            this.gameplayManager = gameplayManager;
            gameplayManager.OnGameplaySetup += GameplayManager_OnGameplaySetup;
        }

        public override void NavigateTo(IAmViewStackItem previousViewStackItem)
        {
            base.NavigateTo(previousViewStackItem);

            newHighScorePrefixTxt.color = Color.clear;

            UpdateButtonsInteractable(true);
        }

        private void GameplayManager_OnGameplaySetup()
        {
            gameplayManager.OnGameplayEnded += GameplayManager_OnGameplayEnded;
            gameplayManager.OnGameplayRestart += GameplayManager_OnGameStartRestart;
            gameplayManager.OnGameOver += GameplayManager_OnGameOver;
            GameplayPointsManager.OnHighScoreChangedE += PointsController_OnHighScoreChanged;

            SetupUI();
        }
        private void GameplayManager_OnGameOver()
        {
            UpdateUI();
        }
        private void GameplayManager_OnGameplayEnded()
        {
            gameplayManager.OnGameplayEnded -= GameplayManager_OnGameplayEnded;
            gameplayManager.OnGameplayRestart -= GameplayManager_OnGameStartRestart;
            gameplayManager.OnGameOver -= GameplayManager_OnGameOver;
            GameplayPointsManager.OnHighScoreChangedE -= PointsController_OnHighScoreChanged;
            RemoveButtonsEvent();
        }
        private void GameplayManager_OnGameStartRestart()
        {
            gameplayManager.OnGameplayEnded -= GameplayManager_OnGameplayEnded;
            gameplayManager.OnGameplayRestart -= GameplayManager_OnGameStartRestart;
            gameplayManager.OnGameOver -= GameplayManager_OnGameOver;
            GameplayPointsManager.OnHighScoreChangedE -= PointsController_OnHighScoreChanged;
            RemoveButtonsEvent();

            ParentStack.TryPopSafe();
        }
        private void PointsController_OnHighScoreChanged(int score)
        {
            newHighScorePrefixTxt.DOFade(1f, fadeInDuration);
        }
        private void RetryButton_OnClick()
        {
            UpdateButtonsInteractable(false);

            gameplayManager.RestartGame();
        }
        private void MainMenuButton_OnClick()
        {
            UpdateButtonsInteractable(false);

            gameplayManager.GameplayEnd();
        }
        private void SetupUI()
        {
            retryButton.OnClickE += RetryButton_OnClick;
            mainMenuButton.OnClickE += MainMenuButton_OnClick;

        }
        private void RemoveButtonsEvent()
        {
            retryButton.OnClickE -= RetryButton_OnClick;
            mainMenuButton.OnClickE -= MainMenuButton_OnClick;
        }
        private void UpdateUI()
        {
            yourScoreTxt.text = $"{GameplayPointsManager.Score}";
            highScoreTxt.text = $"{GameplayPointsManager.HighScore}";
        }
        private void UpdateButtonsInteractable(bool value)
        {
            retryButton.SetInteractable(value);
            mainMenuButton.SetInteractable(value);
        }
    }
}