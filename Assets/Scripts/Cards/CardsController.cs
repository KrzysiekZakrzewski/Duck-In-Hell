using BlueRacconGames.Cards.Effects;
using BlueRacconGames.MeleeCombat;
using BlueRacconGames.Pool;
using Game.Managers;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BlueRacconGames.Cards
{
    public class CardsController : MonoBehaviour
    {
        private readonly List<PassiveHitCardEffect> passiveHitCardEffects = new ();
        private readonly List<PassiveLoopCardEffect> passiveLoopCardEffect = new ();
        private GameplayManager gameplayManager;

        public DefaultPooledEmitter PoolEmiter { get; private set; }

        [Inject]
        private void Inject(DefaultPooledEmitter poolEmiter, GameplayManager gameplayManager)
        {
            this.PoolEmiter = poolEmiter;
            this.gameplayManager = gameplayManager;

            gameplayManager.OnGameplaySetup += GameplayManager_OnGameplaySetup;
        }
        private void GameplayManager_OnGameOver()
        {
            gameplayManager.OnGameOver -= GameplayManager_OnGameOver;

            gameplayManager.OnGameplayEnded += GameplayManager_OnGameplayEnded;
            gameplayManager.OnGameplayRestart += GameplayManager_OnGameRestart;

            for (int i = passiveHitCardEffects.Count - 1; i >= 0; i--)
                passiveHitCardEffects[i].DiscardEffect();
            for (int i = passiveLoopCardEffect.Count - 1; i >= 0; i--)
                passiveLoopCardEffect[i].DiscardEffect();
        }
        private void GameplayManager_OnGameplaySetup()
        {
            gameplayManager.OnGameOver += GameplayManager_OnGameOver;
        }
        private void GameplayManager_OnGameRestart()
        {
            gameplayManager.OnGameplayRestart -= GameplayManager_OnGameRestart;
            gameplayManager.OnGameplayEnded -= GameplayManager_OnGameplayEnded;

            passiveHitCardEffects.Clear();
            passiveLoopCardEffect.Clear();
        }
        private void GameplayManager_OnGameplayEnded()
        {
            GameplayManager_OnGameRestart();
        }

        public void AddPassiveHitEffect(PassiveHitCardEffect passiveHitEffect)
        {
            if(passiveHitCardEffects.Contains(passiveHitEffect))
                passiveHitCardEffects.Remove(passiveHitEffect);
                
            passiveHitCardEffects.Add(passiveHitEffect);
        }
        public void ExecutePassiveHitEffects(IDamagableTarget target)
        {
            if (passiveHitCardEffects.Count <= 0) return;

            foreach (var effect in passiveHitCardEffects)
                effect.Execute(target, PoolEmiter);
        }
        public void AddPassiveLoopEffect(PassiveLoopCardEffect passiveHitEffect)
        {
            if (passiveLoopCardEffect.Contains(passiveHitEffect))
                passiveLoopCardEffect.Remove(passiveHitEffect);

            passiveLoopCardEffect.Add(passiveHitEffect);
        }
        public void RemovePassiveLoopEffect(PassiveLoopCardEffect passiveHitEffect)
        {
            if (!passiveLoopCardEffect.Contains(passiveHitEffect)) return;

            passiveLoopCardEffect.Remove(passiveHitEffect);
        }

        [ContextMenu("Show Effects Level")]
        private void ShowEffectsLevel()
        {
            foreach(PassiveHitCardEffect passiveHit in passiveHitCardEffects)
            {
                Debug.Log("Lvl: " + passiveHit.GetType() + " " + passiveHit.Level);
            }
        }
    }
}