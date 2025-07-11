using Game.Item.Factory;
using Game.Points;
using TimeTickSystems;

namespace Game.Item
{
    public class PointsMultiplierItem : BuffStatMultiplierItem
    {
        public PointsMultiplierItem(PointsMultiplierItemFactorySO initialData) : base(initialData)
        {

        }

        protected override void ApplyBuff()
        {
            base.ApplyBuff();
            PointsController.AddMultiplier(this);
        }

        protected override void RemoveBuff()
        {
            base.RemoveBuff();

            PointsController.RemoveMultiplier(this);
        }
    }
}
