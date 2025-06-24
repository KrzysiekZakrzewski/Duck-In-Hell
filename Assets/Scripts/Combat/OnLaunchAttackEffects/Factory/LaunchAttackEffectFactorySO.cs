using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    public abstract class LaunchAttackEffectFactorySO : ScriptableObject, ILaunchAttackEffectFactory
    {
        public abstract ILaunchAttackEffect CreateEffect();
    }
}