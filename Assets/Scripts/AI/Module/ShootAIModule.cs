using Damageable;
using Projectiles.Implementation;
using System.Data;
using TimeTickSystems;
using UnityEngine;

namespace BlueRacconGames.AI
{
    public class ShootAIModule : AttackAIModule
    {
        [SerializeField] private int shootTickCountdown = 10;

        private ProjectileEmitterControllerBase projectileEmitterControllerBase;
        private bool countdownEnable = false;

        private int tickRemaning;
        private Transform target;

        protected override void Damageable_OnExpireE(IDamageable damageable)
        {
            base.Damageable_OnExpireE(damageable);

            TimeTickSystem.OnTick -= OnTick;
        }

        public override void Initialize(AIControllerBase aIController)
        {
            base.Initialize(aIController);

            projectileEmitterControllerBase = aIController.GetComponent<ProjectileEmitterControllerBase>();

            TimeTickSystem.OnTick += OnTick;

            ResetShootCountdown();

            target = aIController.PlayerTransform;
        }

        private void Shoot()
        {
            projectileEmitterControllerBase.EmitProjectile(TargetAngle(target.position));

            ResetShootCountdown();
        }
        private Vector2 TargetAngle(Vector3 targetPoint)
        {
            return (targetPoint - projectileEmitterControllerBase.ProjectileSpawnPoint.position).normalized;
        }
        private void ResetShootCountdown()
        {
            tickRemaning = shootTickCountdown;
            countdownEnable = true;
        }
        private void OnTick(object sender, OnTickEventArgs e)
        {
            if (!countdownEnable || aIController.IsWondering || !aIController.IsSimulating || !attackEnabled) return;

            tickRemaning--;

            if (tickRemaning > 0) return;

            Shoot();
        }
    }
}