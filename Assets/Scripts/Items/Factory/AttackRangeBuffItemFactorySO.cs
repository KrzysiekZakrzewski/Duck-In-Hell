using System;

namespace Game.Item.Factory
{
    public class AttackRangeBuffItemFactorySO : BuffStatItemFactorySO
    {
        public override IItem CreateItem()
        {
            return new AttackRangeBuffItem(this);
        }
    }
}
