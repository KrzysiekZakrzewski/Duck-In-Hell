using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace BlueRacconGames.UI
{
    public class UIButtonBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private UIButtonPresentationController presentation;

        public event Action OnClickE;
        public event Action<bool> OnEnterE;
        public event Action<bool> OnExitE;

        public bool IsInteractable { get; private set; }

        public void SetInteractable(bool value)
        {
            IsInteractable = value;

            presentation.InteractableVisualize(value);
        }
        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            presentation.OnEnterPresentation(IsInteractable);

            OnEnterE?.Invoke(IsInteractable);
        }
        public virtual void OnPointerExit(PointerEventData eventData)
        {
            OnExitE?.Invoke(IsInteractable);

            presentation.OnExitPresentation(IsInteractable);
        }
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if(!IsInteractable) return;

            presentation.OnClickPresentation();
            OnClickE?.Invoke();
        }
        public virtual void OnPointerDown(PointerEventData eventData)
        {
            presentation.OnDownPresentation();
        }
        public virtual void OnPointerUp(PointerEventData eventData)
        {
            presentation.OnUpPresentation();
        }
        public virtual void ResetButton()
        {
            OnClickE = null;
            OnEnterE = null;
            OnExitE = null;
        }
        public void ForceUpdatePresentationModule(bool state)
        {
            if(presentation == null) return;

            presentation.ForceUpdatePresentationModule(state);
        }
    }
}