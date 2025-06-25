using System.Collections.Generic;
using UnityEngine;

namespace BlueRacconGames.Cards
{
    public class CardsInventory : MonoBehaviour
    {
        private Dictionary<ICardFactory, ICard> cardDeclarationToRuntimeLogicLut = new();

        public void AddCard(ICardFactory cardData)
        {
            if (cardDeclarationToRuntimeLogicLut.ContainsKey(cardData))
            {
                OnHaveCard(cardData);
                return;
            }
            
            var questRuntime = cardData.CreateCard();

            cardDeclarationToRuntimeLogicLut.Add(cardData, questRuntime);
        }

        private void OnHaveCard(ICardFactory cardData)
        {
            cardDeclarationToRuntimeLogicLut.TryGetValue(cardData, out var card);

            card.LevelUp();
        }
    }
}