using BlueRacconGames.Pool;
using Game.HUD;
using Projectiles.Implementation;
using UnityEngine;
using Zenject;

namespace Units.Implementation
{
    public class PooledShootEnemyUnit : PooledEnemyUnit
    {
        private ProjectilePoolEmitter projectilePoolEmitter;
        private DefaultProjectileEmitterController projectileEmitterController;

        [Inject]
        private void Inject(ProjectilePoolEmitter projectilePoolEmitter)
        {
            this.projectilePoolEmitter = projectilePoolEmitter;
        }
        public override void SetUnitData(UnitDataSO unitDataSO)
        {
            base.SetUnitData(unitDataSO);

            var shootUnitDataSO = unitDataSO as PooledShootEnemyUnitDataSO;

            projectileEmitterController.Launch(projectilePoolEmitter);

            aiController.Initialize(shootUnitDataSO.AIDataSO);
        }
        public override void Launch(IPoolItemEmitter sourceEmitter, Vector3 startPosition, Vector3 direction)
        {
            base.Launch(sourceEmitter, startPosition, direction);

            projectileEmitterController = GetComponent<DefaultProjectileEmitterController>();
        }
    }
}