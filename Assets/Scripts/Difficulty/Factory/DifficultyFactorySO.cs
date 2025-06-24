using UnityEngine;

namespace Game.Difficulty
{
    public abstract class DifficultyFactorySO : ScriptableObject, IDifficultyFactory
    {
        public abstract IDifficulty Create();
    }
}
