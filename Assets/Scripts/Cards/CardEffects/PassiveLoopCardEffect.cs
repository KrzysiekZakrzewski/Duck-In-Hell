using BlueRacconGames.Pool;
using TimeTickSystems;
using UnityEngine;

namespace BlueRacconGames.Cards.Effects
{
    public abstract class PassiveLoopCardEffect : CardEffectBase
    {
        [SerializeField] protected int tickDuration;
        [SerializeField] protected int tickLoopDuration;

        protected int tick;
        public override void ApplyEffect(CardsController cardsController)
        {
            cardsController.AddPassiveLoopEffect(this);
        }

        public abstract void Execute(DefaultPooledEmitter pooledEmitter);
    }
}