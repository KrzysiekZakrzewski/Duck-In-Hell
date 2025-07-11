using BlueRacconGames.Pool;
using UnityEngine;
using Units;
using Game.Item.Factory;

namespace Game.Item
{
    public class HealItemBase : ActionItem
    {
        private bool isFullHealthTakas;
        private readonly int healValue;
        private readonly ParticlePoolItem healVfFX;

        public HealItemBase(HealItemBaseFactorySO initialData) : base(initialData)
        {
            healValue = initialData.HealValue;
            healVfFX = initialData.HealVFX;
            isFullHealthTakas = initialData.IsFullHealthTakes;
        }

        public override bool Use(IUnit source)
        {
            if(!source.Damageable.Heal(healValue) && !isFullHealthTakas) return false;

            source.DefaultPooledEmitter.EmitItem<ParticlePoolItem>(healVfFX, source.GameObject.transform.position, Vector3.zero);

            return true;
        }
    }
}