using ViewSystem;
using ViewSystem.Implementation;
using UnityEngine;
using Game.Managers;
using BlueRacconGames.UI;

namespace Game.View
{
    public class MainMenuView : BasicView
    {
        [SerializeField] private MainMenuManager mainMenuManager;
        [SerializeField] private UIButtonBase playButton;
        [SerializeField] private UIButtonBase settingsButton;
        [SerializeField] private UIButtonBase creditsButton;
        [SerializeField] private UIButtonBase quitButton;
        [SerializeField] private SettingsView settingsView;
        [SerializeField] private CreditsView creditsView;

        public override bool Absolute => false;

        protected override void Awake()
        {
            base.Awake();

            SetupButtons();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            playButton.OnClickE -= PlayButton_OnPerformed;
            settingsButton.OnClickE -= SettingsButton_OnPerformed;
            creditsButton.OnClickE -= CreditsButton_OnPerformed;
        }

        private void SetupButtons()
        {
            playButton.OnClickE += PlayButton_OnPerformed;
            settingsButton.OnClickE += SettingsButton_OnPerformed;
            creditsButton.OnClickE += CreditsButton_OnPerformed;
        }

        private void PlayButton_OnPerformed()
        {
            mainMenuManager.Play();
        }

        private void SettingsButton_OnPerformed()
        {
            ParentStack.TryPushSafe(settingsView);
        }

        private void CreditsButton_OnPerformed()
        {
            ParentStack.TryPushSafe(creditsView);
        }

        protected override void Presentation_OnShowPresentationComplete(IAmViewPresentation presentation)
        {
            base.Presentation_OnShowPresentationComplete(presentation);
        }

        public override void NavigateTo(IAmViewStackItem previousViewStackItem)
        {
            base.NavigateTo(previousViewStackItem);
        }

        public override void NavigateFrom(IAmViewStackItem nextViewStackItem)
        {
            base.NavigateFrom(nextViewStackItem);
        }
    }
}