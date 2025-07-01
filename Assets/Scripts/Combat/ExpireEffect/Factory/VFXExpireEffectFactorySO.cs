using BlueRacconGames.Pool;
using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    [CreateAssetMenu(fileName = nameof(VFXExpireEffectFactorySO), menuName = nameof(BlueRacconGames) + "/" + nameof(MeleeCombat) + "/" + nameof(VFXExpireEffectFactorySO))]
    public class VFXExpireEffectFactorySO : ExpireEffectFactorySO
    {
        [SerializeField] private ParticlePoolItem vfxEffect;
        public override IExpireEffect CreateExpireEffect()
        {
            return new VFXExpireEffect(vfxEffect);
        }
    }
}
