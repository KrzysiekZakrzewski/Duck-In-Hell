using BlueRacconGames.MeleeCombat;
using Game.Item.Factory;
using Units;

namespace Game.Item
{
    public class AttackRangeBuffItem : BuffStatItem
    {
        public AttackRangeBuffItem(AttackRangeBuffItemFactorySO initialData) : base(initialData)
        {
        }

        public override bool Use(IUnit source)
        {
            MeleeCombatControllerBase meleeCombatController = source.GameObject.GetComponent<MeleeCombatControllerBase>();

            meleeCombatController.IncreaseAttackRange(buffValue);
            return true;
        }
    }
}