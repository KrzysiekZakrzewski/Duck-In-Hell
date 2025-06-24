using EnemyWaves;
using System;
using UnityEngine;
using Zenject;

namespace Game.Managers
{
    public class SelectCardManager : MonoBehaviour
    {
        private EnemyWavesManager enemyWavesManager;

        public event Action OnCardSetupedE;
        public event Action OnCardSelectedE;

        [Inject]
        private void Inject(EnemyWavesManager enemyWavesManager)
        {
            this.enemyWavesManager = enemyWavesManager;
        }

        private void Awake()
        {
            enemyWavesManager.OnWaveSetupedE += OnWaveSetuped;
        }
        private void OnDestroy()
        {
            enemyWavesManager.OnWaveSetupedE -= OnWaveSetuped;
        }

        private void OnWaveSetuped(IEnemyWave wave)
        {
            wave.OnCompletedE += SetupCards;
            Debug.Log("Select Setuped");
        }
        private void SetupCards(IEnemyWave wave)
        {
            OnCardSetupedE?.Invoke();
            Debug.Log("Select SetupCards");
            //TO DO wylosowaæ karty 
            //TO DO pokazaæ UI
        }
    }
}