using Damageable;
using Game.CharacterController;
using UnityEngine;

namespace Units.Implementation
{
    public class PlayerUnit : UnitBase
    {
        [SerializeField] private PlayerUnitDataSO playerDataSO;

        private void Awake()
        {
            Lauch();

            SetUnitData(playerDataSO);
        }

        public override void SetUnitData(UnitDataSO unitDataSO)
        {
            base.SetUnitData(unitDataSO);

            this.playerDataSO = unitDataSO as PlayerUnitDataSO;

            damageable?.Launch(unitDataSO.DamagableDataSO);

            characterController.SetData(unitDataSO.CharacterControllerDataSO);
        }
    }
}