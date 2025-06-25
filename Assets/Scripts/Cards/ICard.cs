using System;

namespace BlueRacconGames.Cards
{
    public interface ICard
    {
        int CardLevel { get; }
        event Action<int> OnLevelUpE;

        void Execute();
        int LevelUp();
    }
}
