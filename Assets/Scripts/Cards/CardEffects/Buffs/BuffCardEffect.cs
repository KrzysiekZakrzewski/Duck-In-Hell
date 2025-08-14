using BlueRacconGames.Cards.Effects.Data;
using Units;
using Units.Implementation;
using UnityEngine;

namespace BlueRacconGames.Cards.Effects
{
    public abstract class BuffCardEffect : CardEffectBase
    {
        [field: SerializeField] public bool IsPermament { get; private set; }
        [field: SerializeField, HideIf(nameof(IsPermament), true)] public int WavesDuration { get; private set; }
        [field: SerializeField] public BuffIdSO Id { get; private set; }

        protected CardsController cardController;

        public override void ApplyEffect(CardsController cardsController, IUnit source)
        {
            base.ApplyEffect(cardsController, source);

            cardsController.AddBuffEffect(this);
        }
        public override void DiscardEffect()
        {
            cardController.RemoveBuffEffect(this);
        }
        public abstract void ExecuteEffect(IUnit unit);
        public abstract void CancelEffect(IUnit unit);
        public abstract object GetValue(bool changeToOpposite);
    }
}