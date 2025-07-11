using Units;
using UnityEngine;

namespace BlueRacconGames.Cards.Effects
{
    public abstract class CardEffectBase
    {
        [SerializeField] protected int baseLevel = 1;

        public int Level { get; protected set; }

        public abstract void DiscardEffect();
        public virtual void ApplyEffect(CardsController cardsController, IUnit source)
        {
            Level = baseLevel;
        }
        public virtual void LevelUp()
        {
            Level++;
        }
    }
}