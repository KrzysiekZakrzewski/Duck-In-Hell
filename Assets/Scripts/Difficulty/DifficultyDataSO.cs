using UnityEngine;

namespace Game.Difficulty
{
    [CreateAssetMenu(fileName = nameof(DifficultyDataSO), menuName = nameof(Game) + "/" + nameof(Difficulty) + "/" + nameof(DifficultyDataSO))]
    public class DifficultyDataSO : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public BaseDifficultyDataSO BaseDifficultyData { get; private set; }
        [field: SerializeField] public float EnemyAmountMultiplier { get; private set; }
        [field: SerializeField] public float EnemyAmountMultiplierByWaveId { get; private set; }
        [field: SerializeField] public float ScoreMultiplier { get; private set; }
        [field: SerializeField] public float RewardsMultiplier { get; private set; }
        [field: SerializeField] public float DamageMultiplier { get; private set; }
    }
}