using BlueRacconGames.MeleeCombat;
using Game.HUD;

namespace Units.Implementation
{
    public class PlayerUnit : PooledUnitBase
    {
        private MeleeCombatControllerBase meleeCombatController;

        public void SetupPlayerUnit(PlayerUnitDataSO playerUnitDataSO, PlayerHUD playerHUD, MeleeCombatControllerBase meleeCombatController)
        {
            unitHUD = playerHUD;
            this.meleeCombatController = meleeCombatController;

            SetUnitData(playerUnitDataSO);
        }
    }
}