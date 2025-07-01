using BlueRacconGames.Animation;
using BlueRacconGames.MeleeCombat;
using UnityEngine;

namespace Damageable
{
    public abstract class DamagableDataSO : ScriptableObject
    {
        [field: SerializeField] public int MaxHealth { get; private set; }
        [field: SerializeField] public bool ExpireOnDead { get; private set; }
        [field: SerializeField] public AnimationDataSO GetHitAnimation { get; private set; }
        [field: SerializeField] public ExpireEffectFactorySO[] ExpireEffectFactorySO { get; private set; }
    }
}
