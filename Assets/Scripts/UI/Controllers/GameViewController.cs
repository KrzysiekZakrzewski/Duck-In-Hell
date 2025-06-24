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
        [SerializeField] private WaveUIManager waveUIManager;
        [SerializeField] private SelectCardView selectCardView;

        private SelectCardManager selectCardManager;

        [Inject]
        private void Inject(SelectCardManager selectCardManager)
        {
            this.selectCardManager = selectCardManager;
        }

        protected override void Awake()
        {
            base.Awake();

            TryOpenSafe<GameHud>();

            Initialize();
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();

            UnInitialize();
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
        private void Initialize()
        {
            var openedViewParentStack = Peek();

            selectCardManager.OnCardSetupedE += () => ShowView(selectCardView);
            selectCardManager.OnCardSelectedE += CloseView;
        }
        private void UnInitialize()
        {
            selectCardManager.OnCardSetupedE -= () => ShowView(selectCardView);
            selectCardManager.OnCardSelectedE -= CloseView;
        }
    }
}