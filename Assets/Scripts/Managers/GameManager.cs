using EnemyWaves;
using Game.Difficulty;
using UnityEngine;
using Zenject;

namespace Game.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private DefaultDifficultyFactorySO difficultyFactorySO;

        private EnemyWavesManager wavesManager;
        private IDifficulty difficulty;

        [Inject]
        private void Inject(EnemyWavesManager wavesManager)
        {
            this.wavesManager = wavesManager;
        }

        private void Awake()
        {
            difficulty = difficultyFactorySO.Create();

            wavesManager.Initialize(difficulty);
        }
    }
}