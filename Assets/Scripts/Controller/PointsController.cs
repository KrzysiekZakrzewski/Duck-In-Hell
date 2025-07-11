using Game.Item;
using System;
using System.Collections.Generic;
using TimeTickSystems;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

namespace Game.Points
{
    public class PointsController : MonoBehaviour
    {
        #region Events
        public static event Action<int> OnScoreChangedE;
        public static event Action<int> OnHighScoreChangedE;
        #endregion

        #region Veriables

        private const int maxScoreValue = 999999999;
        private static float multiplier = 1f;
        public static int Score { get; private set; }
        public static int HighScore {  get; private set; }
        #endregion

        #region Public methods
        public static void AddPoints(int value)
        {
            int pointsToAdd = Mathf.FloorToInt(value * multiplier);

            SetScore(Score += pointsToAdd);
        }
        public static void ReducePoints(int value)
        {
            if(Score <= 0) return;

            SetScore(Score -= value);
        }
        public static bool IsNewHighScore()
        {
            var result = Score > HighScore;

            if(!result) return false;

            OnHighScoreChangedE?.Invoke(HighScore = Score);

            return result;
        }
        public static void ResetScore()
        {
            SetScore(0);
        }
        public static void AddMultiplier(PointsMultiplierItem item)
        {
            multiplier += item.Multiplier;
        }
        public static void RemoveMultiplier(PointsMultiplierItem item)
        {
            multiplier -= item.Multiplier;
        }
        #endregion

        #region Private methods
        private static void SetScore(int value)
        {
            value = Math.Clamp(value, 0, maxScoreValue);

            Score = value;

            OnScoreChangedE?.Invoke(Score);
        }
        #endregion
    }
}