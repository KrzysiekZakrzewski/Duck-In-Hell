using TimeTickSystems;
using Units;
using UnityEngine;

namespace BlueRacconGames.Cards.Effects
{
    public abstract class PassiveLoopCardEffect : CardEffectBase
    {
        [SerializeField] protected int durationTick;
        [SerializeField] protected int loopDurationTick;

        protected int startTick;
        protected CardsController cardController;
        protected IUnit source;

        public override void ApplyEffect(CardsController cardsController, IUnit source)
        {
            base.ApplyEffect(cardsController, source);

            this.cardController = cardsController;
            this.source = source;

            cardsController.AddPassiveLoopEffect(this);

            startTick = TimeTickSystem.GetTick();

            TimeTickSystem.OnTick += OnTick;
        }
        public override void DiscardEffect()
        {
            cardController.RemovePassiveLoopEffect(this);

            TimeTickSystem.OnTick -= OnTick;
        }
        public abstract void Execute();

        private void OnTick(object sender, OnTickEventArgs e)
        {
            var tick = TimeTickSystem.GetTick();
            var duration = tick - startTick;

            if (loopDurationTick != -1 && duration >= loopDurationTick)
            {
                DiscardEffect();
                return;
            }

            if (duration % durationTick != 0) return;

            Execute();
        }
    }
}