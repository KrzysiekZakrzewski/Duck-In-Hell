using ViewSystem;
using ViewSystem.Implementation;
using UnityEngine;
using Game.Managers;
using BlueRacconGames.UI;
using Zenject;
using Saves;

namespace Game.View
{
    public class MainMenuView : BasicView
    {
        [SerializeField] private UIButtonBase playButton;
        [SerializeField] private UIButtonBase quitButton;
        [SerializeField] private UIButtonBase soundsButton;
        [SerializeField] private UIButtonBase musicButton;

        private MainMenuManager mainMenuManager;

        public override bool Absolute => false;

        [Inject]
        private void Inject(MainMenuManager mainMenuManager)
        {
            this.mainMenuManager = mainMenuManager;

            mainMenuManager.OnMainMenuLoaded += MainMenuManager_OnMainMenuLoaded;
        }
        public override void NavigateTo(IAmViewStackItem previousViewStackItem)
        {
            base.NavigateTo(previousViewStackItem);

            UpdateButtonInteractableState(true);
        }
        public override void NavigateFrom(IAmViewStackItem previousViewStackItem)
        {
            base.NavigateFrom(previousViewStackItem);

            UpdateButtonInteractableState(false);
        }
        private void MainMenuManager_OnMainMenuLoaded()
        {
            mainMenuManager.OnMainMenuLoaded -= MainMenuManager_OnMainMenuLoaded;

            SetupButtons();

            mainMenuManager.OnNewGameStart += MainMenuManager_OnNewGameStart;
            mainMenuManager.OnMainMenuLeaved += MainMenuManager_OnMainMenuLeaved;
        }
        private void MainMenuManager_OnNewGameStart()
        {

        }
        private void MainMenuManager_OnMainMenuLeaved()
        {
            mainMenuManager.OnNewGameStart -= MainMenuManager_OnNewGameStart;
            mainMenuManager.OnMainMenuLeaved -= MainMenuManager_OnMainMenuLeaved;

            DeSetupButtons();
        }
        private void SetupButtons()
        {
            playButton.OnClickE += PlayButton_OnPerformed;
            quitButton.OnClickE += QuitButton_OnPerformed;
            soundsButton.OnClickE += SoundsButton_OnPerformed;
            musicButton.OnClickE += MusicButton_OnPerformed;

            soundsButton.ForceUpdatePresentationModule(mainMenuManager.GetSettingsValue<bool>(SaveKeyUtilities.SFXSettingsKey));
            musicButton.ForceUpdatePresentationModule(mainMenuManager.GetSettingsValue<bool>(SaveKeyUtilities.MusicSettingsKey));
        }
        private void DeSetupButtons()
        {
            playButton.OnClickE -= PlayButton_OnPerformed;
            quitButton.OnClickE -= QuitButton_OnPerformed;
            soundsButton.OnClickE -= SoundsButton_OnPerformed;
            musicButton.OnClickE -= MusicButton_OnPerformed;
        }

        private void PlayButton_OnPerformed()
        {
            mainMenuManager.StartNewGame();
        }
        private void QuitButton_OnPerformed()
        {
            mainMenuManager.QuitGame();
        }
        private void SoundsButton_OnPerformed()
        {
            mainMenuManager.UpdateSoundsEnable();
        }
        private void MusicButton_OnPerformed()
        {
            mainMenuManager.UpdateMusicEnable();
        }
        private void UpdateButtonInteractableState(bool state)
        {
            playButton.SetInteractable(state);
            quitButton.SetInteractable(state);
            soundsButton.SetInteractable(state);
            musicButton.SetInteractable(state);
        }
    }
}