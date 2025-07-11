using BlueRacconGames.Cards;
using BlueRacconGames.MeleeCombat;
using BlueRacconGames.Pool;
using Damageable.Implementation;
using Game.HUD;
using System;
using Units;
using Units.Implementation;
using UnityEngine;
using Zenject;

namespace Game.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private PlayerUnitDataSO playerDataSO;
        [SerializeField] private Transform playerSpawnPosition;
        [SerializeField] private PlayerUnit playerUnitPrefab;
        [SerializeField] PlayerHUD playerHUD;

        private PlayerUnit playerUnit;
        private PlayerDamagable playerDamagable;
        private UnitPoolEmitter unitSpawner;

        private GameManager gameManager;
        private CardsController cardsController;
        private DefaultPooledEmitter pooledEmitter;

        [Inject]
        private void Inject(UnitPoolEmitter unitSpawner, DefaultPooledEmitter pooledEmitter, 
            CardsController cardsController, GameManager gameManager)
        {
            this.unitSpawner = unitSpawner;
            this.pooledEmitter = pooledEmitter;
            this.cardsController = cardsController;
            this.gameManager = gameManager;
        }

        private void Awake()
        {
            gameManager.OnGameStartSetup += GameManager_OnGameStartSetup;
            gameManager.OnGameRun += GameManager_OnGameRun;
            gameManager.OnGameStoped += GameManager_OnGameStoped;
        }
        private void OnDestroy()
        {
            gameManager.OnGameStartSetup -= GameManager_OnGameStartSetup;
            gameManager.OnGameRun -= GameManager_OnGameRun;
            gameManager.OnGameStoped -= GameManager_OnGameStoped;
        }

        private void GameManager_OnGameStartSetup()
        {
            SetupPlayer();
        }
        private void GameManager_OnGameRun()
        {
            Debug.Log("GM Run");
            playerUnit.UpdateUnitEnable(true, StopUnitType.Full);
        }
        private void GameManager_OnGameStoped()
        {
            Debug.Log("GM Stop");
            playerUnit.UpdateUnitEnable(false, StopUnitType.Full);
        }
        private void PlayerDamagable_OnDeadE(IUnit unit)
        {
            gameManager.GameOver();
            playerUnit.UpdateUnitEnable(false, StopUnitType.Full);
        }

        public void SetupPlayer()
        {
            playerUnit = unitSpawner.EmitItem<PlayerUnit>(playerUnitPrefab, playerSpawnPosition.position, Vector3.zero);
            var meleeCombatController = playerUnit.GameObject.GetComponent<MeleeCombatControllerBase>();
            meleeCombatController.Initialize(pooledEmitter, cardsController);

            playerUnit.SetupPlayerUnit(playerDataSO, playerHUD, meleeCombatController);

            playerDamagable = playerUnit.GetComponent<PlayerDamagable>();
            playerDamagable.OnDeadE += PlayerDamagable_OnDeadE;

            playerUnit.UpdateUnitEnable(false, StopUnitType.Full);
        }
        public PlayerUnit GetPlayerUnit() => playerUnit;
        public void OnOffPlayerMoveable(bool value)
        {
            if(playerUnit == null) return;

            playerUnit.UpdateUnitEnable(value, StopUnitType.Full);
        }
    }
}