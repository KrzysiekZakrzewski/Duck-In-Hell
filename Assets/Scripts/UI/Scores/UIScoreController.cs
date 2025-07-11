using Game.Managers;
using Game.Points;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.HUD
{
    public class UIScoreController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreTxt;

        private const string scoreNameData = "Score:";

        private GameManager gameManager;

        [Inject]
        private void Inject(GameManager gameManager)
        {
            this.gameManager = gameManager;

            gameManager.OnGameStartSetup += GameManager_OnGameStartSetup;
            gameManager.OnGameEnded += GameManager_OnGameEnded;
        }

        private void GameManager_OnGameStartSetup()
        {
            UpdateScoreTxt(0);
            PointsController.OnScoreChangedE += PointsController_OnScoreChanged;
        }
        private void GameManager_OnGameEnded()
        {
            PointsController.OnScoreChangedE -= PointsController_OnScoreChanged;
            gameManager.OnGameStartSetup -= GameManager_OnGameStartSetup;
            gameManager.OnGameEnded -= GameManager_OnGameEnded;
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
