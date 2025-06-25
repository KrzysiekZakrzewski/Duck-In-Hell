using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace BlueRacconGames.Cards
{
    public class GrantCardManager : MonoBehaviour
    {
        [SerializeField] private CardFactorySO[] allCards;

        private HashSet<CardFactorySO> cardsPool;

        private void Awake()
        {
            SortCardsByPercent();
        }

        public void SortCardsByPercent()
        {
            cardsPool = new HashSet<CardFactorySO>(allCards);

            cardsPool = cardsPool.OrderBy(x => x.BasePercentChance).ToHashSet();
        }
        public CardFactorySO RandomizeCardData(List<CardFactorySO> excludedCards)
        {
            var randomizePool = cardsPool;

            randomizePool.ExceptWith(excludedCards);

            float roll = Random.value;
            float cumulative = 0f;

            foreach (CardFactorySO cardData in cardsPool)
            {
                cumulative += cardData.BasePercentChance;

                if (roll > cumulative) continue;

                return cardData;
            }

            return randomizePool.ToList()[0];
        }
    }
}