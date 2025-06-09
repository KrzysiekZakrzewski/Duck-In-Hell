using UnityEngine;

namespace Damageable.Implementation
{
    [CreateAssetMenu(fileName = nameof(PooledEnemyDamagableDataSO), menuName = nameof(Damageable) + "/" + nameof(Damageable.Implementation) + "/" + nameof(PooledEnemyDamagableDataSO))]

    public class PooledEnemyDamagableDataSO : ScriptableObject, IDamagableDataSO
    {
        [field: SerializeField] public int MaxHealth { get; private set; }
        [field: SerializeField] public bool ExpireOnDead { get; private set; }
        [field: SerializeField] public ParticleSystem ExpireParticle { get; private set; }
    }
}