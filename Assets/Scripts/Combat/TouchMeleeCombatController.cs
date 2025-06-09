using BlueRacconGames.MeleeCombat.Implementation;
using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    public class TouchMeleeCombatController : MeleeCombatControllerBase
    {
        [SerializeField] protected BaseTouchMeleeWeaponFactorySO baseTouchWeapon;

        protected override void Awake()
        {
            base.Awake();

            weapon = baseTouchWeapon.CreateWeapon();
        }

        private void FixedUpdate()
        {
            Attack();
        }
    }
}