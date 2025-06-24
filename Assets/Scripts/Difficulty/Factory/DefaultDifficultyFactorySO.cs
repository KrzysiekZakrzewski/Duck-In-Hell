using UnityEngine;

namespace Game.Difficulty
{
    [CreateAssetMenu(fileName = nameof(DefaultDifficultyFactorySO), menuName = nameof(Game) + "/" + nameof(Difficulty) + "/" + nameof(DefaultDifficultyFactorySO))]

    public class DefaultDifficultyFactorySO : DifficultyFactorySO
    {
        [SerializeField] private DifficultyDataSO initializeData;

        public override IDifficulty Create()
        {
            return new DefaultDifficulty(initializeData);
        }
    }
}
