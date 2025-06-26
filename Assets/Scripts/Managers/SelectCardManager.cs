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
        private readonly int cardsToDrawAmount = 3;

        private EnemyWavesManager enemyWavesManager;
        private GrantCardManager grantCardManager;
        private CardsInventory cardsInventory;

        public event Action OnCardSetupedE;
        public event Action<SelectCardData[]> OnCardsDrawnE;
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
            OnCardsDrawnE?.Invoke(CreateCardDatas());

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
        private SelectCardData[] CreateCardDatas()
        {
            var cards = GetCards();

            SelectCardData[] datas = new SelectCardData[cards.Count];

            for (int i = 0; i < cards.Count; i++)
            {
                datas[i] = new()
                {
                    CardFactory = cards[i],
                    InInventoryExist = cardsInventory.IsCardExist(cards[i], out int level),
                    InInventoryLvl = level
                };
            }

            return datas;
        }
    }
    public struct SelectCardData
    {
        public CardFactorySO CardFactory;
        public bool InInventoryExist;
        public int InInventoryLvl;
    }
}