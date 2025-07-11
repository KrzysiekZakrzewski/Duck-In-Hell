using BlueRacconGames.AI;
using BlueRacconGames.Pool;
using Game.HUD;
using UnityEngine;

namespace Units.Implementation
{
    public class PooledEnemyUnit : PooledUnitBase
    {
        protected AIControllerBase aiController;

        public AIControllerBase AIController => aiController;

        public override void Launch(IPoolItemEmitter sourceEmitter, Vector3 startPosition, Vector3 direction)
        {
            aiController = GetComponent<AIControllerBase>();

            base.Launch(sourceEmitter, startPosition, direction);
        }

        protected override void ExpireInternal()
        {
            base.ExpireInternal();
            aiController.OnExpire();
        }

        protected override void UpdateUnitAttackEnable(bool enableValue)
        {

        }
    }
}
