using BlueRacconGames.AI;
using BlueRacconGames.Pool;
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
        public override void UpdateUnitEnable(bool enableValue, StopUnitType stopType = StopUnitType.Movement)
        {
            base.UpdateUnitEnable(enableValue, stopType);

            AIController.ForceStartStopSimulate(enableValue);
        }

        protected override void ExpireInternal()
        {
            base.ExpireInternal();
            aiController.OnExpire();
        }
        protected override void UpdateUnitAttackEnable(bool enableValue)
        {
            var aiMode = AIController.AIMode;

            if (aiMode.Modules == null || aiMode.Modules.Count == 0) return;

            AttackAIModule attackAIModule = null;

            foreach (IAIModule module in aiMode.Modules)
            {
                if (module is not AttackAIModule) continue;

                attackAIModule = module as AttackAIModule;
                break;
            }

            if (attackAIModule == null) return;

            if (enableValue)
                attackAIModule.EnableAttack();
            else
                attackAIModule.DisableAttack();
        }
    }
}
