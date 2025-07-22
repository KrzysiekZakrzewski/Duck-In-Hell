using Game.Managers;
using Game.Points;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.HUD
{
    public class UIGameplayScoreController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreTxt;

        private const string scoreNameData = "Score:";

        private GameplayManager gameplayManager;

        [Inject]
        private void Inject(GameplayManager gameplayManager)
        {
            this.gameplayManager = gameplayManager;

            gameplayManager.OnGameplaySetup += GameplayManager_OnGameplaySetup;
        }

        private void GameplayManager_OnGameplaySetup()
        {
            UpdateScoreTxt(0);
            GameplayPointsManager.OnScoreChangedE += PointsController_OnScoreChanged;
            gameplayManager.OnGameOver += GameplayManager_OnGameOver;
        }
        private void GameplayManager_OnGameOver()
        {
            GameplayPointsManager.OnScoreChangedE -= PointsController_OnScoreChanged;
            gameplayManager.OnGameOver -= GameplayManager_OnGameOver;

            gameplayManager.OnGameplayEnded += GameplayManager_OnGameplayEnded;
        }
        private void GameplayManager_OnGameplayEnded()
        {
            gameplayManager.OnGameplaySetup -= GameplayManager_OnGameplaySetup;
            gameplayManager.OnGameplayEnded -= GameplayManager_OnGameplayEnded;
        }

        private void PointsController_OnScoreChanged(int score)
        {
            UpdateScoreTxt(score);
        }

        private void UpdateScoreTxt(int score)
        {
            scoreTxt.text = $"{scoreNameData} {score}";
        }
    }
}
