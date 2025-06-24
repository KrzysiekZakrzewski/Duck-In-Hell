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

        private const string ENEMY_REMAIN_TEXT = "EnemyRemain";
        private const string WAVES_TEXT = "Waves:";

        [Inject]
        private void Inject(EnemyWavesManager waveManager)
        {
            this.waveManager = waveManager;
        }

        private void Start()
        {
            enemyRemainTxt.text = $"{ENEMY_REMAIN_TEXT} \n -";
            waveNumberTxt.text = $"{WAVES_TEXT} -";
            waveManager.OnWaveSetupedE += SubscribeWave;
        }
        private void OnDisable()
        {
            waveManager.OnWaveSetupedE -= SubscribeWave;
        }

        private void SubscribeWave(IEnemyWave enemyWave)
        {
            enemyWave.OnUpdatedE += EnemyRemainUpdate;
            enemyWave.OnCompletedE += UnSubscribeWave;
            EnemyRemainUpdate(enemyWave);
            waveNumberTxt.text = $"{WAVES_TEXT} {waveManager.CurrentWavesId}";
        }
        private void UnSubscribeWave(IEnemyWave enemyWave)
        {
            enemyWave.OnUpdatedE -= EnemyRemainUpdate;
            enemyWave.OnCompletedE -= UnSubscribeWave;
            enemyRemainTxt.text = $"{ENEMY_REMAIN_TEXT} \n -";
        }
        private void EnemyRemainUpdate(IEnemyWave enemyWave)
        {
            enemyRemainTxt.text = $"{ENEMY_REMAIN_TEXT} \n {enemyWave.EnemyRemain}";
        }
    }
}