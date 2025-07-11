using Game.Item.Factory;
using Units;

namespace Game.Item
{
    internal class InventoryItem : ItemBase
    {
        public InventoryItem(ItemFactorySO initialData) : base(initialData)
        {
        }

        public override bool Use(IUnit source)
        {
            throw new System.NotImplementedException();
        }
    }
}
