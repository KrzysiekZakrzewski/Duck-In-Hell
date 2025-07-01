using BlueRacconGames.Pool;
using Game.Item;
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

        private readonly float expireDelay = 0.1f;
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

        protected override bool InteractInternal(InteractorControllerBase interactor)
        {
            if(!IsInteractable) return false;

            SwitchInteractable(false);

            StartCoroutine(WaitForExpire(interactor));

            return true;
        }
        private IEnumerator WaitForExpire(InteractorControllerBase interactor)
        {
            pooledEmitter.EmitItem<ParticlePoolItem>(initialData.PickUpVFX, transform.position, Quaternion.identity.eulerAngles);

            yield return null;

            ProcessItem(interactor);

            yield return new WaitForSeconds(expireDelay);

            Expire();
        }
        private void ProcessItem(InteractorControllerBase interactor)
        {
            switch (initialData)
            {
                case ActionItemFactorySO:
                    var itemAction = initialData.CreateItem();
                    IUnit unit = interactor.GetComponent<IUnit>();
                    itemAction.Use(unit);
                    break;
                case InventoryItemFactorySO:
                    //TO DO Add to Inventory 
                    break;
            }
        }
    }
}