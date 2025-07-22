using Game.Managers;
using TMPro;
using UnityEngine;
using Zenject;

namespace EnemyWaves.UI
{
    public class WaveUIManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI waveNumberTxt;
        [SerializeField] private TextMeshProUGUI enemyRemainTxt;

        private EnemyWavesManager waveManager;
        private GameplayManager gameplayManager;

        private const string ENEMY_REMAIN_TEXT = "EnemyRemain";
        private const string WAVES_TEXT = "Waves:";

        [Inject]
        private void Inject(EnemyWavesManager waveManager, GameplayManager gameplayManager)
        {
            this.waveManager = waveManager;
            this.gameplayManager = gameplayManager;

            gameplayManager.OnGameplaySetup += GameManager_OnGameplaySetup;
        }

        private void GameManager_OnGameplaySetup()
        {
            enemyRemainTxt.text = $"{ENEMY_REMAIN_TEXT} \n -";
            waveNumberTxt.text = $"{WAVES_TEXT} -";

            waveManager.OnWaveSetupedE += WaveManager_OnWaveSetupedE;
            gameplayManager.OnGameOver += GameplayManager_OnGameOver;
        }
        private void GameplayManager_OnGameOver()
        {
            waveManager.OnWaveSetupedE -= WaveManager_OnWaveSetupedE;
            gameplayManager.OnGameOver -= GameplayManager_OnGameOver;

            gameplayManager.OnGameplayEnded += GameplayManager_OnGameplayEnded;
            gameplayManager.OnGameplayRestart += GameplayManager_OnGameplayRestart;
        }
        private void GameplayManager_OnGameplayRestart()
        {
            gameplayManager.OnGameplayEnded -= GameplayManager_OnGameplayEnded;
            gameplayManager.OnGameplayRestart -= GameplayManager_OnGameplayRestart;
        }
        private void GameplayManager_OnGameplayEnded()
        {
            gameplayManager.OnGameplaySetup -= GameManager_OnGameplaySetup;
            gameplayManager.OnGameplayEnded -= GameplayManager_OnGameplayEnded;
        }
        private void WaveManager_OnWaveSetupedE(IEnemyWave enemyWave)
        {
            SubscribeWave(enemyWave);
        }
        private void EnemyWave_OnUpdatedE(IEnemyWave enemyWave)
        {
            EnemyRemainUpdate(enemyWave);
        }
        private void EnemyWave_OnCompletedE(IEnemyWave enemyWave)
        {
            UnSubscribeWave(enemyWave);
        }

        private void SubscribeWave(IEnemyWave enemyWave)
        {
            enemyWave.OnUpdatedE += EnemyWave_OnUpdatedE;
            enemyWave.OnCompletedE += EnemyWave_OnCompletedE;
            EnemyRemainUpdate(enemyWave);
            waveNumberTxt.text = $"{WAVES_TEXT} {waveManager.CurrentWavesId}";
        }
        private void UnSubscribeWave(IEnemyWave enemyWave)
        {
            enemyWave.OnUpdatedE -= EnemyWave_OnUpdatedE;
            enemyWave.OnCompletedE -= EnemyWave_OnCompletedE;
            enemyRemainTxt.text = $"{ENEMY_REMAIN_TEXT} \n -";
        }
        private void EnemyRemainUpdate(IEnemyWave enemyWave)
        {
            enemyRemainTxt.text = $"{ENEMY_REMAIN_TEXT} \n {enemyWave.EnemyRemain}";
        }
    }
}