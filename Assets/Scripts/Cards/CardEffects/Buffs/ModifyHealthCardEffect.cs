using Damageable.Implementation;
using System;
using Units;
using UnityEngine;

namespace BlueRacconGames.Cards.Effects
{
    public class ModifyHealthCardEffect : BuffCardEffect
    {
        [SerializeField] private int value;

        public override void CancelEffect(IUnit unit)
        {
            var damageable = unit.Damageable;

            damageable.DecreaseHealt(value);
        }

        public override void ExecuteEffect(IUnit unit)
        {
            var damageable = unit.Damageable;

            damageable.IncreaseHealt(value);
        }

        public override object GetValue(bool changeToOpposite) => changeToOpposite ? -value : value;
    }
}