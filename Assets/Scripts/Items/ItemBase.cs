using Game.Item.Factory;
using Units;
using UnityEngine;

namespace Game.Item
{
    public abstract class ItemBase : IItem
    {
        protected ItemFactorySO initialData;

        public int Id => initialData.Id;
        public string Name => initialData.Name;
        public string Description => initialData.Description;
        public Sprite Icon => initialData.Icon;

        protected ItemBase(ItemFactorySO initialData)
        {
            this.initialData = initialData;
        }

        public abstract bool Use(IUnit source);
    }
}
