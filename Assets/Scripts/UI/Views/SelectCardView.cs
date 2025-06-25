using BlueRacconGames.Cards;
using BlueRacconGames.UI;
using Game.Managers;
using System;
using System.Collections.Generic;
using UnityEngine;
using ViewSystem;
using ViewSystem.Implementation;
using Zenject;

namespace Game.View
{
    public class SelectCardView : BasicView
    {
        [SerializeField] private CardPanel[] rewardsPanels;
        public override bool Absolute => false;

        private SelectCardManager selectCardManager;

        [Inject]
        private void Inject(SelectCardManager selectCardManager)
        {
            this.selectCardManager = selectCardManager;

            selectCardManager.OnCardsDrawnE += SetupCardPanels;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            selectCardManager.OnCardsDrawnE -= SetupCardPanels;
        }

        private void SetupCardPanels(CardFactorySO[] drawnCards)
        {
            for (int i = 0; i < rewardsPanels.Length; i++)
            {
                rewardsPanels[i].SetupPanel(selectCardManager, drawnCards[i]);
            }
        }
    }
}