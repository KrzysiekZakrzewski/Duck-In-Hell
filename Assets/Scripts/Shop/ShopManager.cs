using System;
using UnityEngine;
using Zenject;

namespace Game.Managers
{
    public class ShopManager : MonoBehaviour
    {
        private ShopState shopState;

        private object currentItemSelected;
        private int itemAmount;

        private MoneyManager moneyManager;

        public event Action OnItemsGeneratedE;
        public event Action<int, int> OnAmountChangedE;
        public event Action OnItemChangedE;
        public event Action OnTransactionValidateE;

        [Inject]
        private void Inject(MoneyManager moneyManager)
        {
            this.moneyManager = moneyManager;
        }

        public bool TryBuyItem()
        {
            if (currentItemSelected == null) return false;

            int totalCost = 0 * itemAmount;//TO DO Buy Cost

            if (!CanBuyValidate(totalCost)) return false;

            if (!moneyManager.TryRemoveMoney(totalCost)) return false;

            //TO DO Add to inventory

            OnTransactionValidate();

            return true;
        }
        public bool TrySellItem()
        {
            if (!CanSellValidate()) return false;

            int moneyEarned = 0 * itemAmount;//TO DO Sell Cost

            //TO DO Remove to inventory

            moneyManager.AddMoney(moneyEarned);

            OnTransactionValidate();

            return true;
        }
        public bool CanBuyValidate(int totalCost)
        {
            if (currentItemSelected == null || shopState != ShopState.Buy) return false;

            return moneyManager.HaveEnoughtMoney(totalCost);
        }
        public bool CanSellValidate()
        {
            if (currentItemSelected == null || shopState != ShopState.Sell) return false;

            return itemAmount <= 0;//TO DO sprawdŸ czy jest tyle w inventory
        }
        public void GetItem(int slotId)
        {
            
        }
        public void GetItemFromInventory()
        {

        }
        public void SelectItem(int slotId)
        {
            itemAmount = 1;
            //currentItemSelected = GetItem();

            OnItemChangedE?.Invoke();
            OnAmountChangedE.Invoke(itemAmount, CalculateTotalCost());
        }
        public void ChangeItemAmount(bool increase = true)
        {
            if (currentItemSelected == null) return;

            itemAmount = increase ? itemAmount + 1 : itemAmount - 1;

            itemAmount = itemAmount >= 1 ? itemAmount : 1;

            OnAmountChangedE?.Invoke(itemAmount, CalculateTotalCost());
        }
        public void GenerateShopEnviroment()
        {
            /*
            itemsLUT = new();

            for (int i = 0; i < items.Length; i++)
            {
                itemsLUT.Add(i, items[i]);
            }
            */
            OnItemsGeneratedE?.Invoke();

            ClearItem();
        }
        public void GetItemsFromInventory()
        {
        }
        public int GetItemCount()
        {
            return 0;
        }
        public int GetItemAmount()
        {
            return 0;
        }
        public void OnShopStateSwiped(ShopState shopState)
        {
            this.shopState = shopState;

            ClearItem();
        } 

        private void ClearItem()
        {
            OnItemChangedE?.Invoke();
            OnAmountChangedE?.Invoke(0, 0);
        }
        private int CalculateTotalCost()
        {
            //int baseCost = shopState == ShopState.Buy ? currentItemSelected.BaseBuyCost : currentItemSelected.BaseSellCost;
            int baseCost = 0;
            return currentItemSelected != null ? itemAmount * baseCost : 0;
        }
        private void OnTransactionValidate()
        {
            /*
            InventoryItem item = GetItemFromInventory(currentItemSelected);

            int itemAmount = item == null ? 0 : GetItemAmount(item);

            OnTransactionValidateE?.Invoke(currentItemSelected, itemAmount);

            OnAmountChangedE?.Invoke(this.itemAmount, CalculateTotalCost());

            if (itemAmount == 0)
                ClearItem();
            */
        }
    }

    public enum ShopState
    {
        Buy,
        Sell,
        Trade
    }
}