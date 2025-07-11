using System;
using Units;

namespace BlueRacconGames.Cards
{
    public abstract class CardBase : ICard
    {
        private int cardLevel = 1;
        private int maxCardLevel = 5;

        public int CardLevel => cardLevel;
        public event Action<int> OnLevelUpE;

        public CardBase()
        {
            cardLevel = 1;
        }

        public abstract void Execute(CardsController cardsController, IUnit source);
        public void LevelUp()
        {
            if(cardLevel >= maxCardLevel) return;

            cardLevel++;

            OnLevelUpE?.Invoke(cardLevel);

            LevelUpInternal();
        }

        protected virtual void LevelUpInternal()
        {
           
        }
    }
}