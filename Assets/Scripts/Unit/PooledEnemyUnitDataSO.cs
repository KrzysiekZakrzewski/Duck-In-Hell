using BlueRacconGames.AI.Data;
using Damageable;
using Damageable.Implementation;
using UnityEngine;

namespace Units.Implementation
{
    [CreateAssetMenu(fileName = nameof(PooledEnemyUnitDataSO), menuName = nameof(Units) + "/" + nameof(Units.Implementation) + "/" + nameof(PooledEnemyUnitDataSO))]
    public class PooledEnemyUnitDataSO : UnitDataSO
    {
        [SerializeField] private PooledEnemyDamagableDataSO enemyDamagableDataSO;
        [field: SerializeField] public BaseAIDataSO AIDataSO { get; private set; }
        public override DamagableDataSO DamagableDataSO => enemyDamagableDataSO;
    }
}