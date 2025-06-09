using UnityEngine;

namespace Interactable
{
    public class EnemyInteractable : InteractableBase
    {
        private bool isHitted;

        protected override bool InteractInternal(InteractorControllerBase interactor)
        {
            if (isHitted) return false;

            return isHitted = true;
        }
        protected override void LeaveInteractInternal(InteractorControllerBase interactor)
        {
            base.LeaveInteractInternal(interactor);

            isHitted = false;
        }
    }
}