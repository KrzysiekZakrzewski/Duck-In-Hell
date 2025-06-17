using System;
using TimeTickSystems;

namespace BlueRacconGames.AI.Implementation
{
    public class WonderAIMode
    {
        private const int BASE_WONDER_TICK_COUNTDOWN = 10;
        private bool countdownEnable = false;
        private int tickRemaning;

        public event Action OnStartWonderE;
        public event Action OnEndWonderE;

        public bool IsWondering { get; private set; } = false;

        public void InitializeWonder(int wonderTickCountdown = BASE_WONDER_TICK_COUNTDOWN)
        {
            tickRemaning = wonderTickCountdown;

            TimeTickSystem.OnTick += OnTick;

            StartWonder();
        }

        private void OnTick(object sender, OnTickEventArgs e)
        {
            if(!countdownEnable || !IsWondering) return;

            tickRemaning--;

            if (tickRemaning > 0) return;

            EndWonder();
        }
        private void StartWonder()
        {
            IsWondering = countdownEnable = true;

            OnStartWonderE?.Invoke();
        }
        private void EndWonder()
        {
            countdownEnable = false;

            TimeTickSystem.OnTick -= OnTick;

            OnEndWonderE?.Invoke();

            IsWondering = false;
        }
    }
}
