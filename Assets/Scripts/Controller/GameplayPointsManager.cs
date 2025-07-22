using Game.Item;
using Game.Managers;
using System;
using UnityEngine;

namespace Game.Points
{
    public class GameplayPointsManager : MonoBehaviour
    {
        #region Events
        public static event Action<int> OnScoreChangedE;
        public static event Action<int> OnHighScoreChangedE;
        #endregion

        #region Veriables
        private GameplayManager gameplayManager;

        private static bool isNewHighScore;
        private const int maxScoreValue = 999999999;
        private static float multiplier = 1f;
        public static int Score { get; private set; }
        public static int HighScore {  get; private set; }

        #endregion

        #region Event_Callbacks
        private void GameplayManager_OnGameplaySetup()
        {
            gameplayManager.OnGameOver += GameplayManager_OnGameOver;

            isNewHighScore = false;
        }
        private void GameplayManager_OnGameOver()
        {
            gameplayManager.OnGameOver -= GameplayManager_OnGameOver;

            gameplayManager.OnGameplayEnded += GameplayManager_OnGameplayEnded;

            IsNewHighScore();
        }
        private void GameplayManager_OnGameplayEnded()
        {
            gameplayManager.OnGameplayEnded -= GameplayManager_OnGameplayEnded;
            gameplayManager.OnGameplayEnded -= GameplayManager_OnGameplaySetup;
        }
        #endregion

        #region Public methods
        public static void AddPoints(int value)
        {
            int pointsToAdd = Mathf.FloorToInt(value * multiplier);

            SetScore(Score += pointsToAdd);

            CheckHighScore();
        }
        public static void ReducePoints(int value)
        {
            if(Score <= 0) return;

            SetScore(Score -= value);
        }
        public static void CheckHighScore()
        {
            if(Score <= HighScore) return;

            HighScore = Score;

            isNewHighScore = true;
        }
        public static void AddMultiplier(PointsMultiplierItem item)
        {
            multiplier += item.Multiplier;
        }
        public static void RemoveMultiplier(PointsMultiplierItem item)
        {
            multiplier -= item.Multiplier;
        }
        public static void ResetScore()
        {
            SetScore(0);
        }
        public static void Load(HighScoreDataLoaded data)
        {
            HighScore = data.Score;
        }
        #endregion

        #region Private methods
        private static void SetScore(int value)
        {
            value = Math.Clamp(value, 0, maxScoreValue);

            Score = value;

            OnScoreChangedE?.Invoke(Score);
        }
        private static void IsNewHighScore()
        {
            if(!isNewHighScore) return;

            OnHighScoreChangedE?.Invoke(HighScore);
        }
        #endregion
    }
}