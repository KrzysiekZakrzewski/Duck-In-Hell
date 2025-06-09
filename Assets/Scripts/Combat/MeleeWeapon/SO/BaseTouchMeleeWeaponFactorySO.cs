using Game.Item;
using Game.Item.Factory.Implementation;
using UnityEngine;

namespace BlueRacconGames.MeleeCombat.Implementation
{
    [CreateAssetMenu(fileName = nameof(BaseTouchMeleeWeaponFactorySO), menuName = nameof(MeleeCombat) + "/" + nameof(Implementation) + "/" + nameof(BaseTouchMeleeWeaponFactorySO))]
    public class BaseTouchMeleeWeaponFactorySO : MeleeWeaponBaseFactorySO
    {
        public override IItemRuntimeLogic CreateItem()
        {
            return new BaseTouchMeleeWeapon(this);
        }

        public IMeleeWeapon CreateWeapon()
        {
            return CreateItem() as IMeleeWeapon;
        }
    }
}