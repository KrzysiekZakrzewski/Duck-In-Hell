using BlueRacconGames.MeleeCombat.Implementation;
using Units;

namespace BlueRacconGames.MeleeCombat
{
    public class BaseTouchMeleeWeapon : MeleeWeaponBase
    {
        public BaseTouchMeleeWeapon(BaseTouchMeleeWeaponFactorySO initialData) : base(initialData)
        {
        }

        public override bool Use(IUnit source)
        {
            throw new System.NotImplementedException();
        }
    }
}