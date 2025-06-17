using BlueRacconGames.AI.Data;
using BlueRacconGames.AI.Factory;
using Damageable;
using Game.CharacterController;
using Projectiles.Implementation;
using System;
using TimeTickSystems;
using UnityEngine;

namespace BlueRacconGames.AI.Implementation
{
    public class StateShootAIMode : StateAIModeBase
    {
        private ProjectileEmitterControllerBase projectileEmitterControllerBase;

        private bool countdownEnable = false;
        private int shootTickCountdown = 10;
        private int tickRemaning;

        private Transform target;

        public StateShootAIMode(AIControllerBase aIController, ShootStateAIDataSO initializeData, ShootStateAIModeFactory factoryData) : base(aIController, initializeData, factoryData)
        { 
            shootTickCountdown = initializeData.ShootTickCountdown;
            projectileEmitterControllerBase = aIController.GetComponent<ProjectileEmitterControllerBase>();

            var damageable = aIController.GetComponent<IDamageable>();
            damageable.OnExpireE += OnDeadEvent;

            TimeTickSystem.OnTick += OnTick;

            ResetShootCountdown();

            target = playerTransform;
        }

        protected override void InternalOnDestory()
        {
            base.InternalOnDestory();

            TimeTickSystem.OnTick -= OnTick;
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
            if (!countdownEnable || AIController.IsWondering) return;

            tickRemaning--;

            if (tickRemaning > 0) return;

            Shoot();
        }
        private void OnDeadEvent(IDamageable damagable)
        {
            TimeTickSystem.OnTick -= OnTick;
            damagable.OnExpireE -= OnDeadEvent;
        }
    }
}
