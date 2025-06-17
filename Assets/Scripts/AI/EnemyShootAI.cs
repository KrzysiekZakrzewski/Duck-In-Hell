using Damageable;
using Game.CharacterController;
using Projectiles.Implementation;
using TimeTickSystems;
using Units;
using UnityEngine;

namespace BlueRacconGames.AI
{
    [RequireComponent(typeof(ProjectileEmitterControllerBase))]
    public class EnemyShootAI : MonoBehaviour
    {
        private ProjectileEmitterControllerBase projectileEmitterControllerBase;

        private bool countdownEnable = false;
        private int shootTickCountdown = 10;
        private int tickRemaning;

        private Transform target;

        private void Launch(IDamageable damageable)
        {
            projectileEmitterControllerBase = GetComponent<ProjectileEmitterControllerBase>();
            target = FindAnyObjectByType<PlayerController>().transform;

            damageable.OnExpireE += OnDeadEvent;

            TimeTickSystem.OnTick += OnTick;

            ResetShootCountdown();
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
            if (!countdownEnable) return;

            tickRemaning--;

            if(tickRemaning > 0) return;

            Shoot();
        }
        private void OnDeadEvent(IDamageable damagable)
        {
            TimeTickSystem.OnTick -= OnTick;
            damagable.OnExpireE -= OnDeadEvent;
        }
    }
}