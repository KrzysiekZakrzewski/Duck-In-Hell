using BlueRacconGames.AI.Factory;
using Damageable;
using Projectiles.Implementation;
using TimeTickSystems;
using UnityEngine;

namespace BlueRacconGames.AI
{
    public class ShootAIModule : AttackAIModule
    {
        private IShootType shootType;
        private int shootTickCountdown;
        private DefaultProjectileEmitterController projectileEmitterControllerBase;
        private bool countdownEnable = false;

        private int tickRemaning;
        private Transform target;

        public ShootAIModule(ShootAIModuleFactory initialData)
        {
            shootType = initialData.ShootType;
            shootTickCountdown = initialData.ShootTickCountdown;
        }

        protected override void Damageable_OnExpireE(IDamageable damageable)
        {
            base.Damageable_OnExpireE(damageable);

            TimeTickSystem.OnTick -= ShootAIModule_OnTick;

            Debug.Log("Damageable_OnExpireE");
        }

        public override void Initialize(AIControllerBase aIController)
        {
            base.Initialize(aIController);

            projectileEmitterControllerBase = aIController.GetComponent<DefaultProjectileEmitterController>();

            TimeTickSystem.OnTick += ShootAIModule_OnTick;

            ResetShootCountdown();

            target = aIController.PlayerTransform;
        }
        public override void DeInitialize(AIControllerBase aIController)
        {
            base.DeInitialize(aIController);

            TimeTickSystem.OnTick -= ShootAIModule_OnTick;
        }
        private void Shoot()
        {
            shootType?.Shoot(projectileEmitterControllerBase, target);
            Debug.Log("Shoot" + projectileEmitterControllerBase.gameObject.name);
            ResetShootCountdown();
        }
        private void ResetShootCountdown()
        {
            tickRemaning = shootTickCountdown;
            countdownEnable = true;
        }
        private void ShootAIModule_OnTick(object sender, OnTickEventArgs e)
        {
            if (!countdownEnable || aIController.IsWondering || !aIController.IsSimulating || !attackEnabled) return;

            tickRemaning--;

            if (tickRemaning > 0) return;

            Shoot();
        }
    }
}