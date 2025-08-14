using Projectiles;
using Projectiles.Implementation;
using UnityEngine;

namespace BasicImplementationSample.Scripts.Effects
{
    [CreateAssetMenu(fileName = nameof(ProjectileDealDamageFactorySo), menuName = "Projectiles/ProjectileEffects/" + nameof (ProjectileDealDamageFactorySo))]
    public class ProjectileDealDamageFactorySo : ProjectileTargetEffectFactorySo
    {
        [SerializeField] private ProjectileDealDamageDataSO initialData;
        public override IProjectileTargetEffect CreateEffect()
        {
            return new DealDamageEffect(initialData);
        }
    }
}
