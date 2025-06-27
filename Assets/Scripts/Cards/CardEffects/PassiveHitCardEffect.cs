using BlueRacconGames.MeleeCombat;
using BlueRacconGames.Pool;
using UnityEngine;

namespace BlueRacconGames.Cards.Effects
{
    public abstract class PassiveHitCardEffect : CardEffectBase
    {
        [SerializeField] protected int damageAmount;

        public abstract void Execute(IDamagableTarget target, DefaultPooledEmitter pooledEmitter);
    }
}