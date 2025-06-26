using RDG.Platforms;
using System;
using System.Collections;
using UnityEngine;

namespace Timers
{
    public class Countdown
    {
        private float countdownSpeed = 1f;
        private float remainingTime;
        private const float baseInterval = 1f;
        private ICountdownPresentation presentation;

        public event Action OnCountdownE;
        public event Action<float> OnCountdownUpdatedE;

        public Countdown(ICountdownPresentation presentation)
        {
            this.presentation = presentation;

            presentation.OnShowPresentationComplete += (presentation) => CorutineSystem.StartSequnce(CountdownCorutine());
        }

        public void StartCountdown(float remainingTime, float countdownSpeed)
        {
            this.remainingTime = remainingTime;
            this.countdownSpeed = countdownSpeed;

            presentation.PlayShowPresentation(this, remainingTime);
        }

        private IEnumerator CountdownCorutine()
        {
            float waitTime = baseInterval / countdownSpeed;

            while (remainingTime > 0)
            {
                yield return new WaitForSeconds(waitTime);
                remainingTime -= baseInterval;

                OnCountdownUpdatedE?.Invoke(remainingTime);
            }
            CountdownFinished();
        }
        private string FormatTime(float time)
        {
            int minutes = GetMinutes(time);
            int seconds = GetSeconds(time);

            return string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        private int GetMinutes(float time)
        {
            return Mathf.FloorToInt(time / 60);
        }
        private int GetSeconds(float time)
        {
            return Mathf.FloorToInt(time % 60);
        }
        private void CountdownFinished()
        {
            OnCountdownE?.Invoke();
            OnCountdownUpdatedE = null;
            OnCountdownE = null;
            presentation.PlayHidePresentation(this);
        }
    }
}