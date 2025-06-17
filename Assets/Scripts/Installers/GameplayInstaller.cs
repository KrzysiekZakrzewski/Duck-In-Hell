using BlueRacconGames.Pool;
using EnemyWaves;
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

        public override void InstallBindings()
        {
            Container.BindInstance(pooledEmitterBase).AsSingle();
            Container.BindInstance(projectilePoolEmitter).AsSingle();
            Container.BindInstance(enemyWavesManager).AsSingle();

            TimeTickSystem.Create();
        }
    }
}