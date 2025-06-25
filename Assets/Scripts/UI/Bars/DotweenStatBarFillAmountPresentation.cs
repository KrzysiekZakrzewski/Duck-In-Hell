using DG.Tweening;
using System;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using ViewSystem.Utils;

namespace BlueRacconGames.UI.Bars.Presentation
{
    [Serializable]
    public class DotweenStatBarFillAmountPresentation : BaseStatBarPresentation
    {
        [SerializeField]
        private float tweenDuration = 0.33f;
        [SerializeField]
        private Ease ease = Ease.OutCubic;
        [SerializeField]
        private CanvasGroup canvasGroup;
        [SerializeField]
        private Image fillAmountImage;

        private Sequence sequence;

        public override event Action<IStatBarPresentation> OnShowPresentationComplete;
        public override event Action<IStatBarPresentation, IStatBar> OnUpdatePresentationComplete;
        public override event Action<IStatBarPresentation> OnHidePresentationComplete;

        public override void PlayShowPresentation(IStatBar statBar)
        {
            PrepareSequence();
            sequence = GetShowSequence(statBar);
            sequence.onComplete += () =>
            {
                OnShowPresentationComplete?.Invoke(this);
            };
            sequence.SetUpdate(true);
        }
        public override void PlayUpdatePresentation(IStatBar statBar)
        {
            float value = CalculateValue(statBar.CurrentValue, statBar.MaxValue);

            if (value <= 0)
            {
                ForceHidePresentationComplete();
                return;
            }

            fillAmountImage.DOFillAmount(value, tweenDuration).OnComplete(() =>
            {
                OnUpdatePresentationComplete?.Invoke(this, statBar);
            });
        }
        public override void PlayHidePresentation(IStatBar statBar)
        {
            PrepareSequence();
            sequence = GetHideSequence(statBar);
            sequence.onComplete += () =>
            {
                OnHidePresentationComplete?.Invoke(this);
            };
            sequence.SetUpdate(true);
        }
        public override void ForceHidePresentationComplete()
        {
            sequence?.Kill();
            OnHidePresentationComplete?.Invoke(this);
        }
        public override void ForceUpdate(IStatBar statBar)
        {
            fillAmountImage.fillAmount = CalculateValue(statBar.CurrentValue, statBar.MaxValue);
            OnUpdatePresentationComplete?.Invoke(this, statBar);
        }
        public override void ResetPresentation(IStatBar statBar)
        {
            fillAmountImage.fillAmount = 0f;
        }

        protected virtual Sequence GetShowSequence(IStatBar statBar)
        {
            return DotweenViewAnimationUtil.FadeIn(canvasGroup, ease, tweenDuration);
        }
        protected virtual Sequence GetHideSequence(IStatBar statBar)
        {
            return DotweenViewAnimationUtil.FadeOut(canvasGroup, ease, tweenDuration);
        }

        private void PrepareSequence()
        {
            sequence?.Kill();
        }
        private float CalculateValue(int currentValue, int maxValue) => (float)currentValue / (float)maxValue;
    }
}
