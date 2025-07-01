using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    public abstract class ExpireEffectFactorySO : ScriptableObject, IExpireEffectFactory
    {
        public abstract IExpireEffect CreateExpireEffect();
    }
}