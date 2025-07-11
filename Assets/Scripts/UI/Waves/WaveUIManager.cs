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
        private GameManager gameManager;

        private const string ENEMY_REMAIN_TEXT = "EnemyRemain";
        private const string WAVES_TEXT = "Waves:";

        [Inject]
        private void Inject(EnemyWavesManager waveManager, GameManager gameManager)
        {
            this.waveManager = waveManager;
            this.gameManager = gameManager;

            gameManager.OnGameStartSetup += GameManager_OnGameStartSetup;
            gameManager.OnGameEnded += GameManager_OnGameEnded;
        }

        private void GameManager_OnGameStartSetup()
        {
            Debug.Log("StartSetupWaveManager");

            enemyRemainTxt.text = $"{ENEMY_REMAIN_TEXT} \n -";
            waveNumberTxt.text = $"{WAVES_TEXT} -";

            waveManager.OnWaveSetupedE += WaveManager_OnWaveSetupedE;
        }
        private void GameManager_OnGameEnded()
        {
            waveManager.OnWaveSetupedE -= WaveManager_OnWaveSetupedE;
            gameManager.OnGameStartSetup -= GameManager_OnGameStartSetup;
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