using ViewSystem.Implementation;
using Game.View;
using Zenject;
using Game.Managers;
using UnityEngine;

public class MainMenuViewController : SingleViewTypeStackController
{
    private MainMenuManager mainMenuManager;

    [Inject]
    private void Inject(MainMenuManager mainMenuManager)
    {
        this.mainMenuManager = mainMenuManager;

        mainMenuManager.OnMainMenuLoaded += MainMenuManager_OnMainMenuLoaded;
    }

    private void MainMenuManager_OnMainMenuLoaded()
    {
        mainMenuManager.OnMainMenuLoaded -= MainMenuManager_OnMainMenuLoaded;
        TryOpenSafe<MainMenuView>();
    }
}
