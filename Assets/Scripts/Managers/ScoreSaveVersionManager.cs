using Game.Points;
using Saves;
using Saves.Object;
using System;
using UnityEngine;
using Zenject;

namespace Game.Managers
{
    public class ScoreSaveVersionManager : MonoBehaviour
    {
        public event Action<HighScoreDataLoaded> OnLoaded;
        public event Action OnValidate;

        private HighScoreSaveObject highScoreSaveObject;
        private GameManager gameManager;
        private GameplayManager gameplayManager;

        [Inject] 
        private void Inject(GameManager gameManager)
        {
            this.gameManager = gameManager;

            gameManager.OnGameStart += GameManager_OnGameStart;
        }

        public void LoadHighScore()
        {
            GetSaveObject();

            var highScoreDataLoaded = new HighScoreDataLoaded
            {
                Score = GetValue<int>(SaveKeyUtilities.HighScore)
            };

            GameplayPointsManager.Load(highScoreDataLoaded);

            OnLoaded?.Invoke(highScoreDataLoaded);
        }
        public void SetHighScore(int score)
        {
            SetHighScoreValue(SaveKeyUtilities.HighScore, score);
        }
        public void SetGameplayManager(GameplayManager gameplayManager)
        {
            this.gameplayManager = gameplayManager;
        }

        private void GameManager_OnGameStart()
        {
            gameManager.OnGameStart -= GameManager_OnGameStart;

            LoadHighScore();
        }
        private void GameplayManager_OnGameplayEnded()
        {
            gameplayManager.OnGameplaySetup -= GameplayManager_OnGameplaySetup;
            GameplayManager_OnGameRestart();
        }
        private void GameplayManager_OnGameplaySetup()
        {
            gameplayManager.OnGameplayRestart += GameplayManager_OnGameRestart;
            gameplayManager.OnGameplayEnded += GameplayManager_OnGameplayEnded;
            GameplayPointsManager.OnHighScoreChangedE += PointsController_OnHighScoreChangedE;
        }
        private void GameplayManager_OnGameRestart()
        {
            GameplayPointsManager.OnHighScoreChangedE -= PointsController_OnHighScoreChangedE;
            gameplayManager.OnGameplayRestart -= GameplayManager_OnGameRestart;
            gameplayManager.OnGameplayEnded -= GameplayManager_OnGameplayEnded;
        }
        private void PointsController_OnHighScoreChangedE(int score)
        {
            SetHighScore(score);
        }

        private T GetValue<T>(string key)
        {
            return highScoreSaveObject.GetValue<T>(key).Value;
        }
        private void SetHighScoreValue(string key, object value)
        {
            highScoreSaveObject.SetValue(key, value);
        }
        private void GetSaveObject()
        {
            SaveManager.TryGetSaveObject(out highScoreSaveObject);
        }
    }
    
    public struct HighScoreDataLoaded
    {
        public int Score;
    }
}