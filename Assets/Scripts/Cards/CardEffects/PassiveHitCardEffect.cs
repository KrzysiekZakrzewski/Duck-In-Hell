using BlueRacconGames.MeleeCombat;
using Damageable;
using System.Collections.Generic;
using UnityEngine;

namespace BlueRacconGames.Cards.Effects
{
    public abstract class PassiveHitCardEffect : CardEffectBase
    {
        [SerializeField] protected int tickDuration;
        [SerializeField] protected int tickLoopDuration;
        [SerializeField] protected int damageAmount;

        protected Dictionary<IDamagableTarget, TargetData> targets;
        protected int tick;
        public override void ApplyEffect(CardsController cardsController)
        {
            cardsController.AddPassiveHitEffect(this);
        }

        public abstract void Execute(IDamagableTarget target);
    }

    public struct TargetData
    {
        public IDamagableTarget DamagableTarget;
        public IDamageable Damageable;
        public int StartTick;
    }
}