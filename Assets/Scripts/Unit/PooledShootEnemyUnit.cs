using BlueRacconGames.Pool;
using Game.HUD;
using Game.Managers;
using Projectiles.Implementation;
using UnityEngine;
using Zenject;

namespace Units.Implementation
{
    public class PooledShootEnemyUnit : PooledEnemyUnit
    {
        private ProjectilePoolEmitter projectilePoolEmitter;
        private PlayerManager playerManager;
        private DefaultProjectileEmitterController projectileEmitterController;

        [Inject]
        private void Inject(ProjectilePoolEmitter projectilePoolEmitter, PlayerManager playerManager)
        {
            this.projectilePoolEmitter = projectilePoolEmitter;
            this.playerManager = playerManager;
        }
        public override void SetUnitData(UnitDataSO unitDataSO)
        {
            base.SetUnitData(unitDataSO);

            var shootUnitDataSO = unitDataSO as PooledShootEnemyUnitDataSO;

            projectileEmitterController.Launch(projectilePoolEmitter);

            aiController.Initialize(shootUnitDataSO.AIDataSO, playerManager.GetPlayerUnit());
        }
        public override void Launch(IPoolItemEmitter sourceEmitter, Vector3 startPosition, Vector3 direction)
        {
            base.Launch(sourceEmitter, startPosition, direction);

            projectileEmitterController = GetComponent<DefaultProjectileEmitterController>();
        }
    }
}