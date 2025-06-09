using System;
using UnityEngine;

namespace Interactable
{
    public abstract class InteractableBase : MonoBehaviour, IInteractable
    {
        [SerializeField] protected bool autoInteractable;
        [SerializeField, HideIf(nameof(autoInteractable), true)] protected string interactionPrompt;
        [SerializeField, HideIf(nameof(autoInteractable), true)] private Transform promptPosition;
        private bool isInteractable = true;
        protected bool isExpired = false;

        public bool IsInteractable => isInteractable;
        public bool IsExpired => isExpired;
        public string InteractionPrompt => interactionPrompt;
        public bool AutoInteractable => autoInteractable;
        public Vector2 PromptPosition => promptPosition.position;

        public event Action<InteractableBase> OnInteractableE;
        public event Action<InteractableBase> OnLeaveInteractableE;
        public event Action<bool> OnSwitchInteractableE;

        public bool Interact(InteractorControllerBase interactor)
        {
            if(!isInteractable || isExpired) return false;

            OnInteractableE?.Invoke(this);

            return InteractInternal(interactor);
        }
        public void LeaveInteract(InteractorControllerBase interactor)
        {
            LeaveInteractInternal(interactor);
        }
        public void SwitchInteractable(bool state)
        {
            isInteractable = state;

            OnSwitchInteractableE?.Invoke(isInteractable);
        }

        protected abstract bool InteractInternal(InteractorControllerBase interactor);
        protected virtual void LeaveInteractInternal(InteractorControllerBase interactor)
        {
            OnLeaveInteractableE?.Invoke(this);
        }
    }
}