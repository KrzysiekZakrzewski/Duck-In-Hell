using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Difficulty
{
    [CreateAssetMenu(fileName = nameof(BaseDifficultyDataSO), menuName = nameof(Game) + "/" + nameof(Difficulty) + "/" + nameof(BaseDifficultyDataSO))]

    public class BaseDifficultyDataSO : ScriptableObject
    {
        [field: SerializeField] public int MinEnemyAmount { get; private set; }
        [field: SerializeField] public int MaxEnemyAmount { get; private set; }
        //Add rewards base

        public int GetRandomEnemyAmount() => Random.Range(MinEnemyAmount, MaxEnemyAmount);
    }
}