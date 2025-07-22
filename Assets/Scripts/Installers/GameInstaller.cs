using Audio.Manager;
using Game.Managers;
using Game.SceneLoader;
using Settings;
using UnityEngine;
using Zenject;

namespace Game.Installer
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private SceneLoadManagers sceneLoadManagers;
        [SerializeField] private AudioManager audioManager;
        [SerializeField] private SettingsManager settingsManager;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private ScoreSaveVersionManager scoreSaveVersionManager;

        public override void InstallBindings()
        {
            Container.BindInstance(sceneLoadManagers).AsSingle();
            Container.BindInstance(audioManager).AsSingle();
            Container.BindInstance(settingsManager).AsSingle();
            Container.BindInstance(gameManager).AsSingle();
            Container.BindInstance(scoreSaveVersionManager).AsSingle();
        }
    }
}