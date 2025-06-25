using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace BlueRacconGames.UI
{
    public class UIButtonPresentationController : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Image buttonImage;
        [SerializeField] private Color normalColor = Color.white;
        [SerializeField] private Color highlightedColor = new(0.9f, 0.9f, 0.9f);
        [SerializeField] private Color disabledColor = new(0.7843137f, 0.7843137f, 0.7843137f, 0.5019608f);

        protected float duration = 0.15f;
        protected Vector3 baseScale;
        protected bool isHighlighted;
        protected float scaleFactor = 0.8f;

        private void Awake()
        {
            baseScale = transform.localScale;
        }

        public void OnEnterPresentation(bool isInteractable)
        {
            isHighlighted = true;

            Color targetColor = isInteractable ? highlightedColor : disabledColor;
            buttonImage.DOColor(targetColor, duration);
        }
        public void OnExitPresentation(bool isInteractable) 
        {
            isHighlighted = false;

            Color targetColor = isInteractable ? normalColor : disabledColor;
            buttonImage.DOColor(targetColor, duration);
        }
        public void OnDownPresentation()
        {
            transform.DOScale(baseScale * scaleFactor, duration).SetEase(Ease.OutBack);
        }
        public void OnUpPresentation()
        {
            transform.DOScale(baseScale, duration).SetEase(Ease.OutBack);
        }
        public void OnClickPresentation()
        {

        }
        public void InteractableVisualize(bool isInteractable)
        {
            Color targetColor = isInteractable ? normalColor : disabledColor;
            buttonImage.DOColor(targetColor, duration);
        }
    }
}