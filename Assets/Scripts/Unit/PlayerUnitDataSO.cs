using Damageable;
using Damageable.Implementation;
using UnityEngine;

namespace Units.Implementation
{
    [CreateAssetMenu(fileName = nameof(PlayerUnitDataSO), menuName = nameof(Units) + "/" + nameof(Units.Implementation) + "/" + nameof(PlayerUnitDataSO))]
    public class PlayerUnitDataSO : UnitDataSO
    {
        [field: SerializeField] private PlayerDamagableDataSO playerDamagableDataSO;

        public override DamagableDataSO DamagableDataSO => playerDamagableDataSO;
    }
}
