using Damageable;
using UnityEngine;

namespace BasicImplementationSample.Scripts.Effects
{
    [CreateAssetMenu(fileName = nameof(ProjectileDealDamageDataSO), menuName = "Projectiles/ProjectileEffects/" + nameof(ProjectileDealDamageDataSO))]
    public class ProjectileDealDamageDataSO : ScriptableObject
    {
        [SerializeField] private bool isRandomizeDamageValue;
        [SerializeField, ShowIf(nameof(isRandomizeDamageValue), false)] private int baseDamageValue;
        [SerializeField, ShowIf(nameof(isRandomizeDamageValue), true)] private int baseMinDamageValue;
        [SerializeField, ShowIf(nameof(isRandomizeDamageValue), true)] private int baseMaxDamageValue;
        [field: SerializeField] public DamageMode DamageType { get; private set; }

        public int GetDamageValue()
        {
            return isRandomizeDamageValue ? RandomizeDamage() : baseDamageValue;
        }

        private int RandomizeDamage()
        {
            return Random.Range(baseMinDamageValue, baseMaxDamageValue);
        }
    }
}
