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

        private GameManager gameManager;
        private SelectCardManager selectCardManager;

        [Inject]
        private void Inject(SelectCardManager selectCardManager, GameManager gameManager)
        {
            this.selectCardManager = selectCardManager;
            this.gameManager = gameManager;

            gameManager.OnGameSetuped += GameManager_OnGameSetuped;
            gameManager.OnGameEnded += GameManager_OnGameEnded;
        }

        #region Unity methods
        protected override void OnDestroy()
        {
            base.OnDestroy();

            gameManager.OnGameSetuped -= GameManager_OnGameSetuped;
            gameManager.OnGameEnded -= GameManager_OnGameEnded;
        }
        #endregion

        #region Private methods
        #region Evennt calbacks methods
        private void GameManager_OnGameSetuped()
        {
            Initialize();
        }
        private void GameManager_OnGameEnded()
        {
            ShowView(gameOverView);

            UnInitialize();
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

        #region Public methods
        #endregion
    }
}