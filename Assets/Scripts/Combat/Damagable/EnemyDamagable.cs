using UnityEngine;

namespace Damageable.Implementation
{
    public class EnemyDamagable : DamageableBase
    {
        private EnemyDamagableDataSO initialData;
        public override bool ExpireOnDead => initialData.ExpireOnDead;

        public override void Launch(IDamagableDataSO damagableDataSO)
        {
            initialData = damagableDataSO as EnemyDamagableDataSO;

            base.Launch(damagableDataSO);
        }

        protected override void OnExpireInternal()
        {
            base.OnExpireInternal();

            SpawnDeathEffect();
        }
        private void SpawnDeathEffect()
        {
            if(initialData.ExpireParticle == null) return;

            Instantiate(initialData.ExpireParticle, transform.position, Quaternion.identity, null);
        }
    }
}