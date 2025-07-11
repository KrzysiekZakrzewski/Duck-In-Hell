using BlueRacconGames.Pool;
using Game.Item.Factory;
using System.Collections;
using Units;
using UnityEngine;
using Zenject;

namespace Interactable
{
    public class ItemInteractable : InteractableBase
    {
        private ItemFactorySO initialData;

        private DefaultPooledEmitter pooledEmitter;

        [Inject]
        private void Inject(DefaultPooledEmitter pooledEmitter)
        {
            this.pooledEmitter = pooledEmitter;
        }

        public void SetupData(ItemFactorySO initialData)
        {
            this.initialData = initialData;

            spriteRenderer.sprite = initialData.Icon;

            MatchColliderToSprite();
        }

        protected override void InteractInternal(InteractorControllerBase interactor)
        {
            if(!ProcessItem(interactor)) return;

            pooledEmitter.EmitItem<ParticlePoolItem>(initialData.PickUpVFX, transform.position, Quaternion.identity.eulerAngles);

            Expire();
        }

        private bool ProcessItem(InteractorControllerBase interactor)
        {
            bool result = false;

            switch (initialData)
            {
                case ActionItemFactorySO:
                    var itemAction = initialData.CreateItem();
                    IUnit unit = interactor.GetComponent<IUnit>();
                    result = itemAction.Use(unit);
                    break;
                case InventoryItemFactorySO:
                    //TO DO Add to Inventory 
                    result = false;
                    break;
                default:
                    result = false;
                    break;
            }

            if(!result)
                SwitchInteractable(true);

            return result;
        }
    }
}