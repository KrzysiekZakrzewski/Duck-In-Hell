using UnityEngine;

namespace Interactable
{
    public interface IInteractable
    {
        bool IsInteractable { get; }
        bool AutoInteractable { get; }
        bool IsExpired { get; }
        string InteractionPrompt { get; }
        Vector2 PromptPosition { get; }
        void SwitchInteractable(bool state);
        void Interact(InteractorControllerBase interactor);
        void LeaveInteract(InteractorControllerBase interactor);
    }
}