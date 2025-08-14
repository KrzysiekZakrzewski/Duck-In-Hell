using BlueRacconGames.Cards.Effects;
using BlueRacconGames.Cards.Effects.Data;
using EnemyWaves;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Managers
{
    public class BuffsManager : MonoBehaviour
    {
        public Action<BuffCardEffect> OnBuffAddedE;
        public Action<BuffCardEffect> OnBuffRemovedE;
        public Action<BuffsManager> OnWaveCompletedE;

        private Dictionary<BuffIdSO, BuffEffectData> buffsLUT = new();

        private GameplayManager gameplayManager;

        [Inject]
        private void Inject(GameplayManager gameplayManager, EnemyWavesManager enemyWavesManager)
        {
            this.gameplayManager = gameplayManager;
            enemyWavesManager.OnWaveCompletedE += EnemyWavesManager_OnWaveCompletedE;

            gameplayManager.OnGameplaySetup += GameplayManager_OnGameplaySetup;
        }

        public void AddBuffs(BuffCardEffect buffEffect)
        {
            if(!buffsLUT.TryGetValue(buffEffect.Id, out var effectData))
            {
                BuffEffectData newBuffEffectData = new(buffEffect);

                buffsLUT.Add(buffEffect.Id, newBuffEffectData);

                if(buffEffect.IsPermament) return;

                OnWaveCompletedE += newBuffEffectData.BuffsManager_OnWaveCompletedE;
            }
            else
                effectData.IncreaseAmount();

            OnBuffAddedE?.Invoke(buffEffect);
        }
        public void RemoveBuffs(BuffCardEffect buffEffect)
        {
            var id = buffEffect.Id;

            if (!buffsLUT.ContainsKey(id)) return;

            buffsLUT[id].DecreaseAmount();

            if(buffsLUT[id].Amount <= 0)
            {
                OnWaveCompletedE -= buffsLUT[id].BuffsManager_OnWaveCompletedE;

                buffsLUT.Remove(id);
            }

            OnBuffRemovedE?.Invoke(buffEffect);
        }
        public object GetBuffValue(BuffIdSO buffId, bool changeToOpposite = false)
        {
            if (!buffsLUT.TryGetValue(buffId, out var effectData))
                return 0;

            return effectData.GetValue(changeToOpposite);
        }
        public void EnemyWavesManager_OnWaveCompletedE()
        {
            OnWaveCompletedE?.Invoke(this);
        }

        private void GameplayManager_OnGameplaySetup()
        {
            gameplayManager.OnGameOver += GameplayManager_OnGameOver;
        }
        private void GameplayManager_OnGameOver()
        {
            gameplayManager.OnGameOver -= GameplayManager_OnGameOver;

            gameplayManager.OnGameplayEnded += GameplayManager_OnGameplayEnded;
            gameplayManager.OnGameplayRestart += GameplayManager_OnGameRestart;

            buffsLUT.Clear();
        }
        private void GameplayManager_OnGameplayEnded()
        {
            gameplayManager.OnGameplaySetup -= GameplayManager_OnGameplaySetup;

            GameplayManager_OnGameRestart();
        }
        private void GameplayManager_OnGameRestart()
        {
            gameplayManager.OnGameplayRestart -= GameplayManager_OnGameRestart;
            gameplayManager.OnGameplayEnded -= GameplayManager_OnGameplayEnded;
        }
    }

    public class BuffEffectData
    {
        public BuffCardEffect Effect { get; private set; }
        public int Amount { get; private set; }

        public int WavesToEnd { get; private set; }

        public BuffEffectData(BuffCardEffect effect, int amount = 1)
        {
            Effect = effect;
            Amount = amount;
            WavesToEnd = effect.IsPermament ? -1 : effect.WavesDuration;
        }

        public void IncreaseAmount()
        {
            Amount++;

            if(Effect.IsPermament) return;

            WavesToEnd = Effect.WavesDuration;
        }
        public void DecreaseAmount() => Amount--;
        public object GetValue(bool changeToOpposite) => Effect.GetValue(changeToOpposite);
        public void BuffsManager_OnWaveCompletedE(BuffsManager buffsManager)
        {
            if(Effect.IsPermament) return;

            WavesToEnd--;

            if(WavesToEnd > 0) return;

            buffsManager.RemoveBuffs(Effect);
        }
    }
}