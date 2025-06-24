using BlueRacconGames.Pool;
using Projectiles.Implementation;
using UnityEngine;
using Zenject;

namespace Units.Implementation
{
    public class PooledShootEnemyUnit : PooledUnitBase
    {
        private PooledShootEnemyUnitDataSO unitDataSO;
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

            this.unitDataSO = unitDataSO as PooledShootEnemyUnitDataSO;

            damageable?.Launch(unitDataSO.DamagableDataSO);

            characterController.SetData(unitDataSO.CharacterControllerDataSO);

            projectileEmitterController.Launch(projectilePoolEmitter);

            aiController.Initialize(this.unitDataSO.AIDataSO);

            ResetUnit();
        }
        public override void Launch(IPoolItemEmitter sourceEmitter, Vector3 startPosition, Vector3 direction)
        {
            base.Launch(sourceEmitter, startPosition, direction);

            projectileEmitterController = GetComponent<DefaultProjectileEmitterController>();
        }
    }
}