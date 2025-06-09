using System;
using UnityEngine;

namespace TimeTickSystems
{
    public class TimeTickSystem : MonoBehaviour
    {
        private const float TICK_TIMER_MAX = 0.2f;
        private const int BIG_DICK_DURATION = 2;

        private static int tick;
        private static GameObject timeTickSystemGameObject;

        public static event EventHandler<OnTickEventArgs> OnTick;
        public static event EventHandler<OnTickEventArgs> OnBigTick;

        public static void Create()
        {
            if (timeTickSystemGameObject != null)
                return;

            timeTickSystemGameObject = new GameObject("TimeTickSystemObject");
            timeTickSystemGameObject.AddComponent<TimeTickSystemObject>();
        }

        public static int GetTick()
        {
            return tick;
        }

       private class TimeTickSystemObject : MonoBehaviour
        {
            private float tickTimer;

            private void Awake()
            {
                tick = 0;
            }

            void Update()
            {
                tickTimer += Time.deltaTime;

                if (tickTimer < TICK_TIMER_MAX) return;

                tickTimer -= TICK_TIMER_MAX;
                tick++;
                OnTick?.Invoke(this, new OnTickEventArgs { tick = tick });

                if (tick % BIG_DICK_DURATION != 0) return;
                OnBigTick?.Invoke(this, new OnTickEventArgs { tick = tick });
            }
        }
    }
}
