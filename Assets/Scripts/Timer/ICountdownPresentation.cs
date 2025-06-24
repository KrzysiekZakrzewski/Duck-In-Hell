using System;

namespace Timers
{
    public interface ICountdownPresentation
    {
        event Action<ICountdownPresentation> OnShowPresentationComplete;
        event Action<ICountdownPresentation> OnUpdatePresentationComplete;
        event Action<ICountdownPresentation> OnHidePresentationComplete;
        void PlayShowPresentation(Countdown timer);
        void PlayUpdatePresentaion(float remaningTime);
        void PlayHidePresentation(Countdown timer);
        void ForceHidePresentationComplete();
        void ForceUpdatePresentaionComplete();
    }
}
