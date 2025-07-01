using BlueRacconGames.Pool;
using System;
using UnityEngine;

namespace Interactable
{
    public abstract class InteractableBase : PoolItemBase, IInteractable
    {
        [SerializeField] protected SpriteRenderer spriteRenderer;
        [SerializeField] protected Collider2D collider2D;
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
        protected void MatchColliderToSprite()
        {
            if (collider2D == null || spriteRenderer == null) return;

            float spriteWidth = spriteRenderer.sprite.bounds.size.x;
            float spriteHeight = spriteRenderer.sprite.bounds.size.y;

            switch (collider2D)
            {
                case CircleCollider2D:
                    float diameter = Mathf.Min(spriteWidth, spriteHeight);
                    float radius = diameter / 2f;

                    CircleCollider2D circleCollider2D = collider2D as CircleCollider2D;
                    circleCollider2D.radius = radius;
                    break;
                case BoxCollider2D:
                    BoxCollider2D boxCollider2D = collider2D as BoxCollider2D;
                    boxCollider2D.size = new Vector2(spriteWidth, spriteHeight);
                    break;
            }

            collider2D.isTrigger = true;
        }
    }
}