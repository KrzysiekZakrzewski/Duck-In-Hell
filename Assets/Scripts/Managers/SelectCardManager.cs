using BlueRacconGames.Cards;
using EnemyWaves;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Managers
{
    public class SelectCardManager : MonoBehaviour
    {
        private int cardsToDrawAmount = 3;

        private EnemyWavesManager enemyWavesManager;
        private GrantCardManager grantCardManager;
        private CardsInventory cardsInventory;

        public event Action OnCardSetupedE;
        public event Action<CardFactorySO[]> OnCardsDrawnE;
        public event Action OnCardSelectedE;

        [Inject]
        private void Inject(EnemyWavesManager enemyWavesManager, GrantCardManager grantCardManager, CardsInventory cardsInventory)
        {
            this.enemyWavesManager = enemyWavesManager;
            this.grantCardManager = grantCardManager;
            this.cardsInventory = cardsInventory;
        }

        private void Awake()
        {
            enemyWavesManager.OnWaveSetupedE += OnWaveSetuped;
        }
        private void OnDestroy()
        {
            enemyWavesManager.OnWaveSetupedE -= OnWaveSetuped;
        }

        public void OnSelectCard(CardFactorySO cardData)
        {
            cardsInventory.AddCard(cardData);

            OnCardSelectedE?.Invoke();
        }

        private void OnWaveSetuped(IEnemyWave wave)
        {
            wave.OnCompletedE += SetupCards;
            Debug.Log("Select Setuped");
        }    
        private void SetupCards(IEnemyWave wave)
        {
            var cards = GetCards();

            OnCardsDrawnE?.Invoke(cards.ToArray());

            OnCardSetupedE?.Invoke();
            Debug.Log("Select SetupCards");
        }  
        private List<CardFactorySO> GetCards()
        {
            List<CardFactorySO> drawnCards = new();

            int cardPanelId = 0;

            while (cardPanelId < cardsToDrawAmount)
            {
                var card = grantCardManager.RandomizeCardData(drawnCards);
                drawnCards.Add(card);

                cardPanelId++;
            }

            return drawnCards;
        }
    }
}