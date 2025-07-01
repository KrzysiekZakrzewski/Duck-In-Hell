using Game.Item.Factory;
using Units;

namespace Game.Item
{
    internal class InventoryItem : ItemBase
    {
        public InventoryItem(ItemFactorySO initialData) : base(initialData)
        {
        }

        protected override bool UseInternal(IUnit source)
        {
            throw new System.NotImplementedException();
        }
    }
}
