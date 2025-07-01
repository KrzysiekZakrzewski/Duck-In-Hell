using Game.Item.Factory;
using Units;
using UnityEngine;

namespace Game.Item
{
    public abstract class ItemBase : IItem
    {
        protected ItemFactorySO initialData;

        private int id;
        private string name;
        private string description;
        private Sprite icon;

        public int Id => id;
        public string Name => name;
        public string Description => description;
        public Sprite Icon => icon;

        protected ItemBase(ItemFactorySO initialData)
        {
            id = initialData.Id;
            name = initialData.Name;
            description = initialData.Description;
            icon = initialData.Icon;
        }

        public bool CanUse()
        {
            return true;
        }
        public bool Use(IUnit source)
        {
            if(CanUse()) return false;

            return UseInternal(source);
        }

        protected abstract bool UseInternal(IUnit source);
    }
}
