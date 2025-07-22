using Game.Managers;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace BlueRacconGames.Cards
{
    public class CardsInventory : MonoBehaviour
    {
        private CardsController cardsController;
        private PlayerManager playerManager;
        private GameplayManager gameplayManager;

        private readonly Dictionary<ICardFactory, ICard> cardDeclarationToRuntimeLogicLut = new();

        [Inject]
        private void Inject(CardsController cardsController, PlayerManager playerManager, GameplayManager gameplayManager)
        {
            this.cardsController = cardsController;
            this.playerManager = playerManager;
            this.gameplayManager = gameplayManager;

            gameplayManager.OnGameplaySetup += GameplayManager_OnGameplaySetup;
        }
        private void GameplayManager_OnGameplaySetup()
        {
            gameplayManager.OnGameplayRestart += GameplayManager_OnGameplayRestart;
            gameplayManager.OnGameplayEnded += GameplayManager_OnGameplayEnded;
        }
        private void GameplayManager_OnGameplayRestart()
        {
            cardDeclarationToRuntimeLogicLut.Clear();
            gameplayManager.OnGameplayRestart -= GameplayManager_OnGameplayRestart;
            gameplayManager.OnGameplayEnded -= GameplayManager_OnGameplayEnded;
        }
        private void GameplayManager_OnGameplayEnded()
        {
            GameplayManager_OnGameplayRestart();
        }

        public void AddCard(ICardFactory cardData)
        {
            if (cardDeclarationToRuntimeLogicLut.ContainsKey(cardData))
            {
                OnHaveCard(cardData);
                return;
            }
            
            var card = cardData.CreateCard();

            cardDeclarationToRuntimeLogicLut.Add(cardData, card);
            card.Execute(cardsController, playerManager.GetPlayerUnit());
        }
        public bool IsCardExist(CardFactorySO cardData, out int cardLevel)
        {
            var isExist = cardDeclarationToRuntimeLogicLut.TryGetValue(cardData, out var card);

            cardLevel = isExist ? card.CardLevel + 1 : 1;

            return isExist;
        }
        private void OnHaveCard(ICardFactory cardData)
        {
            cardDeclarationToRuntimeLogicLut.TryGetValue(cardData, out var card);

            card.LevelUp();
        }
    }
}