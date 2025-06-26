using BlueRacconGames.Cards.Effects;
using BlueRacconGames.MeleeCombat;
using System.Collections.Generic;
using UnityEngine;

namespace BlueRacconGames.Cards
{
    public class CardsController : MonoBehaviour
    {
        private List<PassiveHitCardEffect> passiveHitCardEffects = new ();

        public void AddPassiveHitEffect(PassiveHitCardEffect passiveHitEffect)
        {
            if(passiveHitCardEffects.Contains(passiveHitEffect))
                passiveHitCardEffects.Remove(passiveHitEffect);
                
            passiveHitCardEffects.Add(passiveHitEffect);

            Debug.Log(passiveHitCardEffects.Count);
        }
        public void ExecutePassiveHitEffects(IDamagableTarget target)
        {
            if(passiveHitCardEffects.Count <= 0) return;
            Debug.Log("ExecutePassiveHitEffects");
            foreach (var effect in passiveHitCardEffects)
                effect.Execute(target);
        }
    }
}