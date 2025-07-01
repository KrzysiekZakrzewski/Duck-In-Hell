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
        public override void Launch(DamagableDataSO initialData)
        {
            base.initialData = initialData as PooledEnemyDamagableDataSO;

            base.Launch(initialData);
        }
    }
}
