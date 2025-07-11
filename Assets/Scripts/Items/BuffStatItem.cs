using Game.Item.Factory;
using Units;

namespace Game.Item
{
    public abstract class BuffStatItem : ActionItem
    {
        protected readonly int buffValue;

        public BuffStatItem(BuffStatItemFactorySO initialData) : base(initialData)
        {
            buffValue = initialData.BuffValue;
        }
    }
}
