using Game.View;
using ViewSystem.Implementation;
using UnityEngine;
using EnemyWaves.UI;
using EnemyWaves;
using Zenject;
using Game.Managers;
using ViewSystem;

namespace BlueRacconGames.View
{
    public class GameViewController : SingleViewTypeStackController
    {
        [SerializeField] private SelectCardView selectCardView;
        [SerializeField] private GameOverView gameOverView;

        private GameplayManager gameplayManager;
        private SelectCardManager selectCardManager;

        [Inject]
        private void Inject(SelectCardManager selectCardManager, GameplayManager gameplayManager)
        {
            this.selectCardManager = selectCardManager;
            this.gameplayManager = gameplayManager;

            gameplayManager.OnGameplaySetup += GameplayManager_OnGameplaySetup;
        }

        #region Public methods
        #endregion

        #region Private methods
        #region Evennt calbacks methods
        private void GameplayManager_OnGameplaySetup()
        {
            Initialize();
            gameplayManager.OnGameOver += GameplayManager_OnGameOver;
        }
        private void GameplayManager_OnGameOver()
        {
            gameplayManager.OnGameOver -= GameplayManager_OnGameOver;
            gameplayManager.OnGameplayEnded += GameplayManager_OnGameplayEnded;

            ShowView(gameOverView);

            UnInitialize();
        }
        private void GameplayManager_OnGameplayEnded()
        {
            gameplayManager.OnGameplaySetup -= GameplayManager_OnGameplaySetup;
            gameplayManager.OnGameplayEnded -= GameplayManager_OnGameplayEnded;
        }
        #endregion
        private void Initialize()
        {
            TryOpenSafe<GameHud>();

            var openedViewParentStack = Peek();

            selectCardManager.OnCardSetupedE += () => ShowView(selectCardView);
            selectCardManager.OnCardSelectedE += CloseView;
        }
        private void UnInitialize()
        {
            selectCardManager.OnCardSetupedE -= () => ShowView(selectCardView);
            selectCardManager.OnCardSelectedE -= CloseView;
        }
        private void ShowView(IAmViewStackItem view)
        {
            var parentStack = Peek().ParentStack;

            parentStack.TryPushSafe(view);
        }
        private void CloseView()
        {
            var parentStack = Peek().ParentStack;

            parentStack.TryPopSafe();
        }
        #endregion
    }
}