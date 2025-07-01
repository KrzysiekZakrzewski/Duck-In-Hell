using Game.Item.Factory;

namespace Game.Item
{
    public abstract class ActionItem : ItemBase
    {
        public ActionItem(ActionItemFactorySO initialData) : base(initialData)
        {

        }
    }
}