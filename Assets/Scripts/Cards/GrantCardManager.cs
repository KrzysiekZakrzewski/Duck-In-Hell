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
            var randomizePool = new HashSet<CardFactorySO>(cardsPool);

            randomizePool.ExceptWith(excludedCards);

            float totalChance = 0f;

            foreach (CardFactorySO cardData in randomizePool)
            {
                totalChance += cardData.BasePercentChance;
            }

            float randomValue = Random.Range(0, totalChance);

            float cumulativeChance = 0f;

            foreach (CardFactorySO cardData in randomizePool)
            {
                cumulativeChance += cardData.BasePercentChance;

                if (randomValue > cumulativeChance) continue;

                return cardData;
            }

            return randomizePool.ToList()[randomizePool.Count - 1];
        }
    }
}