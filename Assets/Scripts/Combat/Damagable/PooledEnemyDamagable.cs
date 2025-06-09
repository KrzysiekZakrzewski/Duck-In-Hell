using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Units.Implementation;
using UnityEngine;

namespace Damageable.Implementation
{
    public class PooledEnemyDamagable : DamageableBase
    {
        private PooledEnemyDamagableDataSO initialData;
        public override bool ExpireOnDead => initialData.ExpireOnDead;

        public override void Launch(IDamagableDataSO damagableDataSO)
        {
            initialData = damagableDataSO as PooledEnemyDamagableDataSO;

            base.Launch(damagableDataSO);
        }
        protected override void OnExpireInternal()
        {
            if (expired) return;

            expired = true;

            SpawnDeathEffect();
        }
        private void SpawnDeathEffect()
        {
            if (initialData.ExpireParticle == null) return;

            Instantiate(initialData.ExpireParticle, transform.position, Quaternion.identity, null);
        }
    }
}
