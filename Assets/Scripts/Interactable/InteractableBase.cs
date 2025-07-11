using BlueRacconGames.Pool;
using System;
using UnityEngine;

namespace Interactable
{
    public abstract class InteractableBase : PoolItemBase, IInteractable
    {
        [SerializeField] protected SpriteRenderer spriteRenderer;
        [SerializeField] protected Collider2D itemCollider;
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

        public void Interact(InteractorControllerBase interactor)
        {
            if(!isInteractable || isExpired) return;

            SwitchInteractable(false);

            InteractInternal(interactor);
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
        public override void ResetItem()
        {
            base.ResetItem();

            SwitchInteractable(true);
        }

        protected abstract void InteractInternal(InteractorControllerBase interactor);
        protected virtual void LeaveInteractInternal(InteractorControllerBase interactor)
        {
            OnLeaveInteractableE?.Invoke(this);
        }
        protected void MatchColliderToSprite()
        {
            if (itemCollider == null || spriteRenderer == null) return;

            float spriteWidth = spriteRenderer.sprite.bounds.size.x;
            float spriteHeight = spriteRenderer.sprite.bounds.size.y;

            switch (itemCollider)
            {
                case CircleCollider2D:
                    float diameter = Mathf.Min(spriteWidth, spriteHeight);
                    float radius = diameter / 2f;

                    CircleCollider2D circleCollider2D = itemCollider as CircleCollider2D;
                    circleCollider2D.radius = radius;
                    break;
                case BoxCollider2D:
                    BoxCollider2D boxCollider2D = itemCollider as BoxCollider2D;
                    boxCollider2D.size = new Vector2(spriteWidth, spriteHeight);
                    break;
            }

            itemCollider.isTrigger = true;
        }
    }
}