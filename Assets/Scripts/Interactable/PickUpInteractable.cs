using UnityEngine;

namespace Interactable
{
    public class PickUpInteractable : InteractableBase
    {
        private readonly float destroyDelay = 0.1f;

        protected override bool InteractInternal(InteractorControllerBase interactor)
        {
            isExpired = true;

            Destroy(gameObject, destroyDelay);

            return true;
        }
    }
}