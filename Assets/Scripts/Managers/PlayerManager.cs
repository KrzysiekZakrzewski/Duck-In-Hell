using BlueRacconGames.Cards;
using BlueRacconGames.Cards.Effects;
using BlueRacconGames.Cards.Effects.Data;
using BlueRacconGames.MeleeCombat;
using BlueRacconGames.Pool;
using Damageable.Implementation;
using Game.HUD;
using System;
using System.Linq;
using Units;
using Units.Implementation;
using UnityEngine;
using Zenject;

namespace Game.Managers
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private BuffIdSO[] buffIdLUT;
        [SerializeField] private PlayerUnitDataSO playerDataSO;
        [SerializeField] private Transform playerSpawnPosition;
        [SerializeField] private PlayerUnit playerUnitPrefab;
        [SerializeField] PlayerHUD playerHUD;

        private PlayerUnit playerUnit;
        private PlayerDamagable playerDamagable;
        private UnitPoolEmitter unitSpawner;

        private GameplayManager gameplayManager;
        private CardsController cardsController;
        private DefaultPooledEmitter pooledEmitter;
        private BuffsManager buffsManager;
        protected MeleeCombatControllerBase meleeCombatController;

        [Inject]
        private void Inject(UnitPoolEmitter unitSpawner, DefaultPooledEmitter pooledEmitter, 
            CardsController cardsController, GameplayManager gameplayManager, BuffsManager buffsManager)
        {
            this.unitSpawner = unitSpawner;
            this.pooledEmitter = pooledEmitter;
            this.cardsController = cardsController;
            this.gameplayManager = gameplayManager;
            this.buffsManager = buffsManager;

            gameplayManager.OnGameplaySetup += GameplayManager_OnGameplaySetup;
        }

        public void SetupPlayer()
        {
            playerUnit = unitSpawner.EmitItem<PlayerUnit>(playerUnitPrefab, playerSpawnPosition.position, Vector3.zero);
            meleeCombatController = playerUnit.GameObject.GetComponent<MeleeCombatControllerBase>();
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

        private void GameplayManager_OnGameplaySetup()
        {
            SetupPlayer();

            gameplayManager.OnGameplayRun += GameplayManager_OnGameplayRun;
            gameplayManager.OnGameplayStop += GameplayManager_OnGameplayStop;
            gameplayManager.OnGameOver += GameplayManager_OnGameOver;
            buffsManager.OnBuffAddedE += BuffsManager_OnBuffAddedE;
            buffsManager.OnBuffRemovedE += BuffsManager_OnBuffRemovedE;
        }
        private void GameplayManager_OnGameplayRun()
        {
            Debug.Log("GM Run");
            playerUnit.UpdateUnitEnable(true, StopUnitType.Full);
        }
        private void GameplayManager_OnGameplayStop()
        {
            Debug.Log("GM Stop");
            playerUnit.UpdateUnitEnable(false, StopUnitType.Full);
        }
        private void GameplayManager_OnGameOver()
        {
            gameplayManager.OnGameplayRun -= GameplayManager_OnGameplayRun;
            gameplayManager.OnGameplayStop -= GameplayManager_OnGameplayStop;
            buffsManager.OnBuffAddedE -= BuffsManager_OnBuffAddedE;
            buffsManager.OnBuffRemovedE -= BuffsManager_OnBuffRemovedE;

            gameplayManager.OnGameplayEnded += GameplayManager_OnGameplayEnded;
            gameplayManager.OnGameplayRestart += GameManager_OnGameStartRestart;
        }
        private void GameManager_OnGameStartRestart()
        {
            gameplayManager.OnGameOver -= GameplayManager_OnGameOver;
            gameplayManager.OnGameplayRestart -= GameManager_OnGameStartRestart;
            gameplayManager.OnGameplayEnded -= GameplayManager_OnGameplayEnded;
        }
        private void GameplayManager_OnGameplayEnded()
        {
            GameManager_OnGameStartRestart();

            gameplayManager.OnGameplaySetup -= GameplayManager_OnGameplaySetup;
        }
        private void PlayerDamagable_OnDeadE(IUnit unit)
        {
            gameplayManager.GameOver();
            playerUnit.UpdateUnitEnable(false, StopUnitType.Full);
        }
        private void BuffsManager_OnBuffAddedE(BuffCardEffect buffCardEffect)
        {
            if(!BuffValidate(buffCardEffect.Id)) return;

            Debug.Log("1");

            buffCardEffect.ExecuteEffect(playerUnit);
        }
        private void BuffsManager_OnBuffRemovedE(BuffCardEffect buffCardEffect)
        {
            if (!BuffValidate(buffCardEffect.Id)) return;

            buffCardEffect.CancelEffect(playerUnit);
        }

        private bool BuffValidate(BuffIdSO Id) => buffIdLUT.Contains(Id);
    }
}