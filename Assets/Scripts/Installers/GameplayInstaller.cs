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
        [SerializeField] private EnemyWavesManager enemyWavesManager;
        [SerializeField] private GameHud gameHud;
        [SerializeField] private SelectCardManager selectCardManager;

        public override void InstallBindings()
        {
            Container.BindInstance(pooledEmitterBase).AsSingle();
            Container.BindInstance(projectilePoolEmitter).AsSingle();
            Container.BindInstance(enemyWavesManager).AsSingle();
            Container.BindInstance(gameHud).AsSingle();
            Container.BindInstance(selectCardManager).AsSingle();

            TimeTickSystem.Create();
        }
    }
}