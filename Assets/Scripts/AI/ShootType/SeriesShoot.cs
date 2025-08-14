using Projectiles.Implementation;
using RDG.Platforms;
using System.Collections;
using UnityEngine;

namespace BlueRacconGames.AI
{
    public class SeriesShoot : SimpleSingleShot
    {
        [SerializeField] private int seriesCount = 3;
        [SerializeField] private float delayBetweenProjectiles = 1f;
        [SerializeField] private float maxAngleOffset;

        public override void Shoot(DefaultProjectileEmitterController projectileEmitterControllerBase, Transform target)
        {
            CorutineSystem.StartSequnce(ShootSequence(projectileEmitterControllerBase, target));
        }

        private IEnumerator ShootSequence(DefaultProjectileEmitterController projectileEmitterControllerBase, Transform target)
        {
            yield return null;

            for (int i = 0; i < seriesCount; i++)
            {
                float angle = Random.Range(0f, maxAngleOffset);
                float x = Mathf.Cos(angle * Mathf.Deg2Rad);
                float y = Mathf.Sin(angle * Mathf.Deg2Rad);
                Vector2 directionOffSet = new Vector2(x, y).normalized;

                base.Shoot(projectileEmitterControllerBase, target);

                yield return new WaitForSeconds(delayBetweenProjectiles);
            }
        }
    }
}