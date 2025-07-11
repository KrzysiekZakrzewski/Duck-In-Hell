using System;
using UnityEngine;

namespace Game.Item.Factory
{
    [CreateAssetMenu(fileName = nameof(PointsBuffItemFactorySO), menuName = nameof(Game) + "/" + nameof(Item.Factory) + "/" + nameof(PointsBuffItemFactorySO))]
    public class PointsBuffItemFactorySO : BuffStatItemFactorySO
    {
        public override IItem CreateItem()
        {
            return new PointsBuffItem(this);
        }
    }
}