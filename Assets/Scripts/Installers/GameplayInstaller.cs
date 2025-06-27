using BlueRacconGames.Cards;
using BlueRacconGames.Pool;
using EnemyWaves;
using Game.Managers;
using Game.View;
using TimeTickSystems;
using UnityEngine;
using Zenject;

namespace Game.Installer
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private DefaultPooledEmitter pooledEmitterBase;
        [SerializeField] private ProjectilePoolEmitter projectilePoolEmitter;
        [SerializeField] private UnitPoolEmitter unitSpawner;
        [SerializeField] private EnemyWavesManager enemyWavesManager;
        [SerializeField] private GameHud gameHud;
        [SerializeField] private SelectCardManager selectCardManager;
        [SerializeField] private CardsInventory cardsInventory;
        [SerializeField] private GrantCardManager grantsCardManager;
        [SerializeField] private CardsController cardsController;

        public override void InstallBindings()
        {
            Container.BindInstance(pooledEmitterBase).AsSingle();
            Container.BindInstance(projectilePoolEmitter).AsSingle();
            Container.BindInstance(unitSpawner).AsSingle();
            Container.BindInstance(enemyWavesManager).AsSingle();
            Container.BindInstance(gameHud).AsSingle();
            Container.BindInstance(selectCardManager).AsSingle();
            Container.BindInstance(cardsInventory).AsSingle();
            Container.BindInstance(grantsCardManager).AsSingle();
            Container.BindInstance(cardsController).AsSingle();

            TimeTickSystem.Create();
        }
    }
}