using System;
using Units;

namespace BlueRacconGames.Cards
{
    public interface ICard
    {
        int CardLevel { get; }
        event Action<int> OnLevelUpE;

        void Execute(CardsController cardsController, IUnit source);
        void LevelUp();
    }
}
