using Game.Item.Factory;
using Game.Points;
using Units;

namespace Game.Item
{
    public class PointsBuffItem : BuffStatItem
    {
        public PointsBuffItem(PointsBuffItemFactorySO initialData) : base(initialData)
        {
        }

        public override bool Use(IUnit source)
        {
            PointsController.AddPoints(buffValue);

            return true;
        }
    }
}