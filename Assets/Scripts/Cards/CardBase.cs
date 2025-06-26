using System;

namespace BlueRacconGames.Cards
{
    public abstract class CardBase : ICard
    {
        private int cardLevel = 1;

        public int CardLevel => cardLevel;
        public event Action<int> OnLevelUpE;

        public CardBase()
        {
            cardLevel = 1;
        }

        public abstract void Execute(CardsController cardsController);

        public int LevelUp()
        {
            cardLevel++;

            OnLevelUpE?.Invoke(cardLevel);

            return cardLevel;
        }
    }
}