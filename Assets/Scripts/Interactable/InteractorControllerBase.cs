using Damageable;
using UnityEngine;

namespace Interactable
{
    public abstract class InteractorControllerBase : MonoBehaviour
    {
        [SerializeField] protected Transform interactionPoint;
        [SerializeField] protected float radius = 0.08f;
        [SerializeField] protected LayerMask interactableLayerMask;

        private Collider2D cacheCollider;

        protected Collider2D[] interactableCollider = new Collider2D[0];

        protected IInteractable interactable;
        protected IInteractable lastInteractable;

        protected virtual void Awake()
        {
            var damageable = GetComponent<IDamageable>();

            damageable.OnExpireE += Damageable_OnExpire;
        }
        protected virtual void FixedUpdate()
        {
            interactableCollider = Physics2D.OverlapCircleAll(interactionPoint.position, radius, interactableLayerMask);
        }
        protected virtual void LateUpdate()
        {
            CheckInteractable();

            RemoveInteractable();
        }
        protected virtual void CheckInteractable()
        {
            if (interactableCollider.Length == 0) return;

            if (cacheCollider == interactableCollider[0]) return;

            cacheCollider = interactableCollider[0];

            interactable = interactableCollider[0].GetComponent<IInteractable>();

            if(interactable == null || !interactable.AutoInteractable || interactable.IsExpired) return;

            Interact();
        }
        protected virtual void Interact()
        {
            if (interactable == null || !interactable.IsInteractable || interactable.IsExpired)
                return;

            interactable.Interact(this);
        }
        protected virtual void RemoveInteractable()
        {
            if (interactable == null && interactableCollider.Length == 0) return;

            if (interactable != null && interactableCollider.Length == 0)
            {
                interactable.LeaveInteract(this);
                interactable = null;
                lastInteractable = null;
                cacheCollider = null;
            }

            if (interactable != null && interactableCollider.Length > 0)
            {
                if (interactable == lastInteractable)
                    return;

                lastInteractable?.LeaveInteract(this);

                lastInteractable = interactable;
            }
        }

        protected void ResetInteractor()
        {
            interactable = lastInteractable = null;
            interactableCollider = null;
            cacheCollider = null;
        }
        private void Damageable_OnExpire(IDamageable damageable)
        {
            damageable.OnExpireE -= Damageable_OnExpire;

            ResetInteractor();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(interactionPoint.position, radius);
        }
    }
}