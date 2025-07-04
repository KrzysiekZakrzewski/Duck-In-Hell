using BlueRacconGames.Cards.Effects;
using BlueRacconGames.MeleeCombat;
using BlueRacconGames.Pool;
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

        private DefaultPooledEmitter poolEmiter;

        [Inject]
        private void Inject(DefaultPooledEmitter poolEmiter)
        {
            this.poolEmiter = poolEmiter;
        }

        public void AddPassiveHitEffect(PassiveHitCardEffect passiveHitEffect)
        {
            if(passiveHitCardEffects.Contains(passiveHitEffect))
                passiveHitCardEffects.Remove(passiveHitEffect);
                
            passiveHitCardEffects.Add(passiveHitEffect);
        }
        public void AddPassiveLoopEffect(PassiveLoopCardEffect passiveHitEffect)
        {
            if (passiveLoopCardEffect.Count == 0)
                TimeTickSystem.OnTick += OnTick;

            if (passiveLoopCardEffect.Contains(passiveHitEffect))
                passiveLoopCardEffect.Remove(passiveHitEffect);

            passiveLoopCardEffect.Add(passiveHitEffect);
        }
        public void RemovePassiveLoopEffect(PassiveLoopCardEffect passiveHitEffect)
        {
            if (!passiveLoopCardEffect.Contains(passiveHitEffect)) return;

            passiveLoopCardEffect.Remove(passiveHitEffect);

            if(passiveLoopCardEffect.Count > 0) return;

            TimeTickSystem.OnTick -= OnTick;
        }
        public void ExecutePassiveHitEffects(IDamagableTarget target)
        {
            if(passiveHitCardEffects.Count <= 0) return;

            foreach (var effect in passiveHitCardEffects)
                effect.Execute(target, poolEmiter);
        }

        private void OnTick(object sender, OnTickEventArgs e)
        {
            int tick = TimeTickSystem.GetTick();
        }
    }
}