using UnityEngine;

namespace Game.Difficulty
{
    public abstract class DifficultyBase : IDifficulty
    {
        protected DifficultyDataSO initializeData;

        public string Name => initializeData.Name;

        public DifficultyBase(DifficultyDataSO initializeData)
        {
            this.initializeData = initializeData;
        }

        public int CalculateTotalEnemyAmount(int waveInRangeId)
        {
            var multiplier = initializeData.EnemyAmountMultiplier + waveInRangeId * initializeData.EnemyAmountMultiplierByWaveId;

            int minEnemyAmount = Mathf.RoundToInt(initializeData.BaseDifficultyData.MinEnemyAmount * multiplier);
            int maxEnemyAmount = Mathf.RoundToInt(initializeData.BaseDifficultyData.MinEnemyAmount * multiplier);

            return Random.Range(minEnemyAmount, maxEnemyAmount);
        }
    }
}
