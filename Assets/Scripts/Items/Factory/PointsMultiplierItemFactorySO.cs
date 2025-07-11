using UnityEngine;

namespace Game.Item.Factory
{
    [CreateAssetMenu(fileName = nameof(PointsMultiplierItemFactorySO), menuName = nameof(Game) + "/" + nameof(Item.Factory) + "/" + nameof(PointsMultiplierItemFactorySO))]
    public class PointsMultiplierItemFactorySO : BuffStatMultiplierItemFactorySO
    {
        public override IItem CreateItem()
        {
            return new PointsMultiplierItem(this);
        }
    }
}
