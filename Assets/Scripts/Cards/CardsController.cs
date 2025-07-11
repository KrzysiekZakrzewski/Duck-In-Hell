using BlueRacconGames.Cards.Effects;
using BlueRacconGames.MeleeCombat;
using BlueRacconGames.Pool;
using System;
using System.Collections.Generic;
using TimeTickSystems;
using UnityEngine;
using Zenject;

namespace BlueRacconGames.Cards
{
    public class CardsController : MonoBehaviour
    {
        private readonly List<PassiveHitCardEffect> passiveHitCardEffects = new ();
        private readonly List<PassiveLoopCardEffect> passiveLoopCardEffect = new ();

        public DefaultPooledEmitter PoolEmiter { get; private set; }

        [Inject]
        private void Inject(DefaultPooledEmitter poolEmiter)
        {
            this.PoolEmiter = poolEmiter;
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