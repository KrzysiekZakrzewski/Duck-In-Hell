using BlueRacconGames.Pool;
using Damageable;
using UnityEngine;
using Units;
using Game.Item.Factory;

namespace Game.Item
{
    public class HealItemBase : ActionItem
    {
        private readonly int healValue;
        private readonly ParticlePoolItem healVfFX;

        public HealItemBase(HealItemBaseFactorySO initialData) : base(initialData)
        {
            healValue = initialData.HealValue;
            healVfFX = initialData.HealVFX;
        }

        protected override bool UseInternal(IUnit source)
        {
            IDamageable damageable = source.GameObject.GetComponent<IDamageable>();

            damageable.Heal(healValue);

            source.DefaultPooledEmitter.EmitItem<ParticlePoolItem>(healVfFX, source.GameObject.transform.position, Vector3.zero);

            return true;
        }
    }
}