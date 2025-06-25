using EnemyWaves;
using Game.Difficulty;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Game.Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private DefaultDifficultyFactorySO difficultyFactorySO;

        private EnemyWavesManager wavesManager;
        private SelectCardManager selectCardManager;
        private IDifficulty difficulty;

        [Inject]
        private void Inject(EnemyWavesManager wavesManager, SelectCardManager selectCardManager)
        {
            this.wavesManager = wavesManager;
            this.selectCardManager = selectCardManager;
        }

        private void Awake()
        {
            difficulty = difficultyFactorySO.Create();

            StartCoroutine(SetupEndlesGameMode());
        }

        private IEnumerator SetupEndlesGameMode()
        {
            yield return new WaitForSeconds(2f);

            selectCardManager.OnCardSelectedE += wavesManager.PrepeareNextWave;

            wavesManager.InitializeGameMode(difficulty);
        }
    }
}