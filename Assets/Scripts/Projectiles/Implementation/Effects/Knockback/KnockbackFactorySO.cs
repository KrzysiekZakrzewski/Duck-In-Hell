using Projectiles.Implementation;
using UnityEngine;

namespace Projectiles.Effects.Factory
{
    [CreateAssetMenu(fileName = nameof(KnockbackFactorySO), menuName = "Projectiles/ProjectileEffects/" + nameof(KnockbackFactorySO))]
    public class KnockbackFactorySO : ProjectileTargetEffectFactorySo
    {
        public override IProjectileTargetEffect CreateEffect()
        {
            return new KnockbackEffect();
        }
    }
}