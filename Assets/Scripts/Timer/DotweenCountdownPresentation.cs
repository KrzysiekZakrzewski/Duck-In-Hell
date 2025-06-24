using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using ViewSystem.Utils;

namespace Timers
{
    public class DotweenCountdownPresentation : BaseCountdownPresentation
    {
        [SerializeField] private float tweenDuration = 0.33f;
        [SerializeField] private Ease ease = Ease.OutCubic;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private TextMeshProUGUI countdownTxt;
        [SerializeField] private string finishMsg;

        private Sequence sequence;

        public override event Action<ICountdownPresentation> OnShowPresentationComplete;
        public override event Action<ICountdownPresentation> OnUpdatePresentationComplete;
        public override event Action<ICountdownPresentation> OnHidePresentationComplete;

        private void Awake()
        {
            canvasGroup.alpha = 0f;
        }

        public override void PlayShowPresentation(Countdown timer)
        {
            timer.OnCountdownUpdatedE += PlayUpdatePresentaion;

            PrepareSequence();
            sequence = GetShowSequence(timer);
            sequence.onComplete += () =>
            {
                OnShowPresentationComplete?.Invoke(this);
            };
            sequence.SetUpdate(true);
        }
        public override void PlayUpdatePresentaion(float remaningTime)
        {
            PrepareSequence();
            sequence = GetUpdateSequence(remaningTime.ToString());
            sequence.onComplete += () =>
            {
                OnUpdatePresentationComplete?.Invoke(this);
            };
            sequence.SetUpdate(true);
        }
        public override void PlayHidePresentation(Countdown timer)
        {
            timer.OnCountdownUpdatedE -= PlayUpdatePresentaion;
            ShowFinishMsg();
            PrepareSequence();
            sequence = GetHideSequence(timer);
            sequence.onComplete += () =>
            {
                OnHidePresentationComplete?.Invoke(this);
            };
            sequence.SetUpdate(true);
        }
        public override void ForceUpdatePresentaionComplete()
        {
            sequence?.Kill();
            OnUpdatePresentationComplete?.Invoke(this);
        }
        public override void ForceHidePresentationComplete()
        {
            sequence?.Kill();
            OnHidePresentationComplete?.Invoke(this);
        }

        protected virtual Sequence GetShowSequence(Countdown timer)
        {
            return DotweenViewAnimationUtil.FadeIn(canvasGroup, ease, tweenDuration);
        }
        protected virtual Sequence GetHideSequence(Countdown timer)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(GetUpdateSequence(finishMsg));
            sequence.Append(DotweenViewAnimationUtil.FadeOut(canvasGroup, ease, tweenDuration));
            return sequence;
        }
        protected virtual Sequence GetUpdateSequence(string msg)
        {
            countdownTxt.text = msg;
            return DotweenViewAnimationUtil.ScaleIn(canvasGroup, Vector3.one, ease, tweenDuration);
        }
        protected virtual void ShowFinishMsg()
        {
            countdownTxt.text = finishMsg;
        }

        private void PrepareSequence()
        {
            sequence?.Kill();
        }
    }
}
