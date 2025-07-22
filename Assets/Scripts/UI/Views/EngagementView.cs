using Inputs;
using Loading.Data;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using ViewSystem;
using ViewSystem.Implementation;

namespace Engagement.UI
{
    public class EngagementView : BasicView
    {
        [SerializeField] private Image backgroundImage;
        [SerializeField] private EngagementController engagementController;
        [SerializeField] private TextMeshProUGUI continueText;
        [SerializeField] private LoadingScreenDatabase loadingScreenDatabase;
        [SerializeField] private bool ignoreContiniuePressed = false;
        [NonSerialized] private Inputs.PlayerInput playerInput;

        public override bool Absolute => false;

        protected override void Awake()
        {
            base.Awake();

            backgroundImage.sprite = loadingScreenDatabase.GetRandomLoadingBackground();

            if (ignoreContiniuePressed)
            {
                continueText.text = "Loading...";
                return;
            }
            
            playerInput = InputManager.GetPlayer(0);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (!ignoreContiniuePressed)
                playerInput.RemoveInputEventDelegate(Continue_OnPerformed);
        }

        protected override void Presentation_OnShowPresentationComplete(IAmViewPresentation presentation)
        {
            base.Presentation_OnShowPresentationComplete(presentation);

            if (ignoreContiniuePressed)
            {
                StartCoroutine(LoadingWait());
                return;
            }

            playerInput.AddInputEventDelegate(Continue_OnPerformed, Inputs.InputActionEventType.ButtonUp, InputUtilities.Submit);
        }

        private void Continue_OnPerformed(InputAction.CallbackContext callback)
        {
            engagementController.FinishEngagement();
        }

        private IEnumerator LoadingWait()
        {
            yield return new WaitForSeconds(1f);

            engagementController.FinishEngagement();
        }

        public void ShowContinueText()
        {

        }
    }
}