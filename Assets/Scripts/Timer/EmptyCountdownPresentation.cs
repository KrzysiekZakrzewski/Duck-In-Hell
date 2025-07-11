using System;

namespace Timers
{
    public class EmptyCountdownPresentation : BaseCountdownPresentation
    {
        public override event Action<ICountdownPresentation> OnShowPresentationComplete;
        public override event Action<ICountdownPresentation> OnUpdatePresentationComplete;
        public override event Action<ICountdownPresentation> OnHidePresentationComplete;

        public override void ForceHidePresentationComplete()
        {
            OnHidePresentationComplete?.Invoke(this);
        }

        public override void ForceUpdatePresentaionComplete()
        {
            OnUpdatePresentationComplete?.Invoke(this);
        }

        public override void PlayHidePresentation(Countdown timer)
        {
            OnHidePresentationComplete?.Invoke(this);
        }

        public override void PlayShowPresentation(Countdown timer, float remaningTime)
        {
            OnShowPresentationComplete?.Invoke(this);
        }

        public override void PlayUpdatePresentaion(float remaningTime)
        {
            OnUpdatePresentationComplete?.Invoke(this);
        }
    }
}
