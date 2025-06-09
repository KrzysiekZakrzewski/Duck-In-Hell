using Inputs;
using System;
using UnityEngine;
using static UnityEngine.UIElements.UxmlAttributeDescription;

namespace Game.CharacterController
{
    public class PlayerController : MonoBehaviour
    {
        [NonSerialized] private PlayerInput playerInput;

        private CharacterController2D characterController;
        private float horizontalMove = 0f;
        private float verticalMove = 0f;
        private bool isMoving = false;

        private void Awake()
        {
            characterController = GetComponent<CharacterController2D>();
            SetupInputs();
        }
        private void Update()
        {
            if (!characterController.CanMove || !isMoving)
            {
                horizontalMove = verticalMove = 0f;
                return;
            }

            horizontalMove = playerInput.GetAxis<float>(InputUtilities.HorizontalAxis);
            verticalMove = playerInput.GetAxis<float>(InputUtilities.VerticalAxis);
        }
        private void FixedUpdate()
        {
            characterController.Move(horizontalMove, verticalMove, false);
        }

        private void SetupInputs()
        {
            playerInput = InputManager.GetPlayer(0);

            playerInput.AddInputEventDelegate(StartMove, InputActionEventType.ButtonPressed, InputUtilities.Move);
            playerInput.AddInputEventDelegate(StopMove, InputActionEventType.ButtonUp, InputUtilities.Move);
            playerInput.AddInputEventDelegate(Dodge, InputActionEventType.ButtonUp, InputUtilities.Dodge);
        }
        private void RemoveInputs()
        {
            playerInput.RemoveInputEventDelegate(StartMove);
            playerInput.RemoveInputEventDelegate(Dodge);
        }
        private void StartMove(UnityEngine.InputSystem.InputAction.CallbackContext callbackContext)
        {
            isMoving = true;
        }
        private void StopMove(UnityEngine.InputSystem.InputAction.CallbackContext callbackContext)
        {
            isMoving = false;
        }
        private void Dodge(UnityEngine.InputSystem.InputAction.CallbackContext callbackContext)
        {

        }
    }
}