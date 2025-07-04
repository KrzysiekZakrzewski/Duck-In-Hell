using BlueRacconGames.Cards;
using BlueRacconGames.MeleeCombat;
using BlueRacconGames.Pool;
using Game.HUD;
using Units.Implementation;
using UnityEngine;
using Zenject;
using static UnityEngine.Rendering.DebugUI;

namespace Game.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private PlayerUnitDataSO playerDataSO;
        [SerializeField] private Transform playerSpawnPosition;
        [SerializeField] private PlayerUnit playerUnitPrefab;
        [SerializeField] PlayerHUD playerHUD;

        private PlayerUnit playerUnit;
        private UnitPoolEmitter unitSpawner;

        private CardsController cardsController;
        private DefaultPooledEmitter pooledEmitter;

        [Inject]
        private void Inject(UnitPoolEmitter unitSpawner, DefaultPooledEmitter pooledEmitter, CardsController cardsController)
        {
            this.unitSpawner = unitSpawner;
            this.pooledEmitter = pooledEmitter;
            this.cardsController = cardsController;
        }

        private void Start()
        {
            playerUnit = unitSpawner.EmitItem<PlayerUnit>(playerUnitPrefab, playerSpawnPosition.position, Vector3.zero);
            var meleeCombatController = playerUnit.GameObject.GetComponent<MeleeCombatControllerBase>();
            meleeCombatController.Initialize(pooledEmitter, cardsController);
            playerUnit.SetupPlayerUnit(playerDataSO, playerHUD, meleeCombatController);

            playerUnit.OnOffUnit(false, StopUnitType.Full);
        }

        public PlayerUnit GetPlayerUnit() => playerUnit;
        public void OnOffPlayerMoveable(bool value)
        {
            if(playerUnit == null) return;

            playerUnit.OnOffUnit(value, StopUnitType.Full);
        }
    }
}