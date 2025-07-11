using BlueRacconGames.UI;
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
        [SerializeField] private UIButtonBase retryButton;
        [SerializeField] private UIButtonBase mainMenuButton;

        private GameManager gameManager;

        public override bool Absolute => false;

        [Inject]
        private void Inject(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        protected override void Awake()
        {
            base.Awake();

            gameManager.OnGameStartSetup += GameManager_OnGameStartSetup;
            gameManager.OnGameEnded += GameManager_OnGameEnded;
        }

        public override void NavigateTo(IAmViewStackItem previousViewStackItem)
        {
            base.NavigateTo(previousViewStackItem);

            UpdateUI();
        }

        private void GameManager_OnGameStartSetup()
        {
            SetupUI();
        }
        private void GameManager_OnGameEnded()
        {

        }
        private void RetryButton_OnClick()
        {
            gameManager.RestartGame();
        }
        private void MainMenuButton_OnClick()
        {

        }
        private void SetupUI()
        {
            retryButton.OnClickE += RetryButton_OnClick;
            mainMenuButton.OnClickE += MainMenuButton_OnClick;

        }
        private void UpdateUI()
        {
            yourScoreTxt.text = $"{PointsController.Score}";
            highScoreTxt.text = $"{PointsController.HighScore}";
        }
    }
}