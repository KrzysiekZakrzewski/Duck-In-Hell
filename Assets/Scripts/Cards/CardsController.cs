using BlueRacconGames.Cards.Effects;
using BlueRacconGames.MeleeCombat;
using BlueRacconGames.Pool;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BlueRacconGames.Cards
{
    public class CardsController : MonoBehaviour
    {
        private List<PassiveHitCardEffect> passiveHitCardEffects = new ();

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
        public void ExecutePassiveHitEffects(IDamagableTarget target)
        {
            if(passiveHitCardEffects.Count <= 0) return;

            foreach (var effect in passiveHitCardEffects)
                effect.Execute(target, poolEmiter);
        }
    }
}