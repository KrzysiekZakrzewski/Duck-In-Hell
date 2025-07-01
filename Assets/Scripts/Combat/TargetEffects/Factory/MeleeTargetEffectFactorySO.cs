using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    public abstract class MeleeTargetEffectFactorySO : ScriptableObject, IMeleeTargetEffectFactory
    {
        public abstract IMeleeTargetEffect CreateEffect();
    }
}