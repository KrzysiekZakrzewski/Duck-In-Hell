using Zenject;
using UnityEngine;
using Game.Managers;

namespace Game.Installer
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private MainMenuManager mainMenuManager;

        public override void InstallBindings()
        {
            Container.BindInstance(mainMenuManager).AsSingle();
        }
    }
}