using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    [CreateAssetMenu(fileName = nameof(AwardingPointsExpireEffectFactorySO), menuName = nameof(BlueRacconGames) + "/" + nameof(MeleeCombat) + "/" + nameof(AwardingPointsExpireEffectFactorySO))]
    public class AwardingPointsExpireEffectFactorySO : ExpireEffectFactorySO
    {
        [SerializeField] private int points;
        public override IExpireEffect CreateExpireEffect()
        {
            return new AwardingPointsExpireEffect(points);
        }
    }
}