using Game.View;
using UnityEngine;
using Zenject;

namespace Units.Implementation
{
    public class PlayerUnit : UnitBase
    {
        [SerializeField] private PlayerUnitDataSO playerDataSO;

        [Inject]
        private void Inject(GameHud gameHud)
        {
            unitHud = gameHud.PlayerHUD;
        }

        private void Awake()
        {
            Launch();

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