using BlueRacconGames.Pool;
using Damageable;
using UnityEngine;

namespace Game.Item
{
    [CreateAssetMenu(fileName = nameof(BombObjectBaseDataSO), menuName = nameof(Game) + "/" + nameof(Item) + "/" + nameof(BombObjectBaseDataSO))]
    public class BombObjectBaseDataSO : ScriptableObject
    {
        [field: SerializeField] public float BaseSpawnChance { get; private set; }
        [field: SerializeField] public float ExplodeDuration { get; private set; } = 5f;
        [field: SerializeField] public float ExplosionRadius { get; private set; }
        [field: SerializeField] public LayerMask DamageableLayer { get; private set; }
        [field: SerializeField] public DamageMode DamageMode { get; private set; }
        [field: SerializeField] public ParticlePoolItem SpawnVFX { get; private set; }
        [field: SerializeField] public ParticlePoolItem ExplodeVFX { get; private set; }
        [field: SerializeField] public float BaseAnimationSpeed { get; private set; } = 1f;
        [field: SerializeField] public float MaxAnimationSpeed { get; private set; } = 3f;
        [field: SerializeField] public float BasePulseDuration { get; private set; } = 0.5f;
        [field: SerializeField] public float ScaleMultiplayer { get; private set; } = 1.3f;
        [field: SerializeField] public int DamageValue { get; private set; } = 5;
        [field: SerializeField] public float LevelPercentBoost { get; private set; } = 0.2f;
    }
}