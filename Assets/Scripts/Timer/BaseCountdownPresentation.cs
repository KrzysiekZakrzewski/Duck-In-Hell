using System;
using UnityEngine;

namespace Timers
{
    [System.Serializable]
    public abstract class BaseCountdownPresentation : MonoBehaviour, ICountdownPresentation
    {
        public abstract event Action<ICountdownPresentation> OnShowPresentationComplete;
        public abstract event Action<ICountdownPresentation> OnUpdatePresentationComplete;
        public abstract event Action<ICountdownPresentation> OnHidePresentationComplete;

        public abstract void ForceHidePresentationComplete();
        public abstract void ForceUpdatePresentaionComplete();
        public abstract void PlayHidePresentation(Countdown timer);
        public abstract void PlayShowPresentation(Countdown timer);
        public abstract void PlayUpdatePresentaion(float remaningTime);
    }
}
