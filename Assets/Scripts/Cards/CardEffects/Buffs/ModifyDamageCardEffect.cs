using BlueRacconGames.MeleeCombat;
using System;
using Units;
using UnityEngine;

namespace BlueRacconGames.Cards.Effects
{
    public class ModifyDamageCardEffect : BuffCardEffect
    {
        [SerializeField] private int value;

        public override void CancelEffect(IUnit unit)
        {
            var meleeWeapon = unit.GameObject.GetComponent<MeleeCombatControllerBase>();
        }

        public override void ExecuteEffect(IUnit unit)
        {
            var meleeWeapon = unit.GameObject.GetComponent<MeleeCombatControllerBase>();

            meleeWeapon.IncreaseDamageValue(value);
        }

        public override object GetValue(bool changeToOpposite) => changeToOpposite ? -value : value;
    }
}
