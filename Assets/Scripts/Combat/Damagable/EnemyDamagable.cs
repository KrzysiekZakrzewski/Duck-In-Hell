using UnityEngine;

namespace Damageable.Implementation
{
    public class EnemyDamagable : DamageableBase
    {
        public override void Launch(DamagableDataSO damagableDataSO)
        {
            initialData = damagableDataSO as EnemyDamagableDataSO;

            base.Launch(damagableDataSO);
        }
    }
}