using Interactable;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Managers
{
    public class ItemInteractableManager : MonoBehaviour
    {
        private List<ItemInteractable> itemsLUT = new();
        private GameplayManager gameplayManager;

        [Inject]
        private void Inject(GameplayManager gameplayManager)
        {
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
            gameplayManager.OnGameplayRestart -= GameplayManager_OnGameplayRestart;
            gameplayManager.OnGameplayEnded -= GameplayManager_OnGameplayEnded;
        }
        private void GameplayManager_OnGameplayEnded()
        {
            GameplayManager_OnGameplayRestart();
        }

        public void AddItem(ItemInteractable item)
        {
            if(itemsLUT.Contains(item)) return;

            itemsLUT.Add(item);
        }
        public void RemoveItem(ItemInteractable item)
        {
            if (!itemsLUT.Contains(item)) return;

            itemsLUT.Remove(item);
        }

        private void ExpireAllActiveItems()
        {
            foreach(ItemInteractable item in itemsLUT)
            {

            }
        }
    }
}