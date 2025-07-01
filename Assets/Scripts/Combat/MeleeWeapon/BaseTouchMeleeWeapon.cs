using BlueRacconGames.MeleeCombat.Implementation;
using Units;

namespace BlueRacconGames.MeleeCombat
{
    public class BaseTouchMeleeWeapon : MeleeWeaponBase
    {
        public BaseTouchMeleeWeapon(BaseTouchMeleeWeaponFactorySO initialData) : base(initialData)
        {
        }

        protected override bool UseInternal(IUnit source)
        {
            throw new System.NotImplementedException();
        }
    }
}