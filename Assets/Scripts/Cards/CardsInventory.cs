using System.Collections.Generic;
using UnityEngine;

namespace BlueRacconGames.Cards
{
    public class CardsInventory : MonoBehaviour
    {
        [SerializeField] private CardsController cardsController;

        private readonly Dictionary<ICardFactory, ICard> cardDeclarationToRuntimeLogicLut = new();

        public void AddCard(ICardFactory cardData)
        {
            if (cardDeclarationToRuntimeLogicLut.ContainsKey(cardData))
            {
                OnHaveCard(cardData);
                return;
            }
            
            var card = cardData.CreateCard();

            cardDeclarationToRuntimeLogicLut.Add(cardData, card);
            card.Execute(cardsController);
        }
        public bool IsCardExist(CardFactorySO cardData, out int cardLevel)
        {
            var isExist = cardDeclarationToRuntimeLogicLut.TryGetValue(cardData, out var card);

            cardLevel = isExist ? card.CardLevel : 1;

            return isExist;
        }

        private void OnHaveCard(ICardFactory cardData)
        {
            cardDeclarationToRuntimeLogicLut.TryGetValue(cardData, out var card);

            card.LevelUp();
        }
    }
}