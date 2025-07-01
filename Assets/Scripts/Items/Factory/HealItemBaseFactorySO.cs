using BlueRacconGames.Pool;
using UnityEngine;

namespace Game.Item.Factory
{
    [CreateAssetMenu(fileName = nameof(HealItemBaseFactorySO), menuName = nameof(Game) + "/" + nameof(Item.Factory) + "/" + nameof(HealItemBaseFactorySO))]
    public class HealItemBaseFactorySO : ActionItemFactorySO
    {
        [field: SerializeField] public int HealValue {  get; private set; }
        [field: SerializeField] public ParticlePoolItem HealVFX { get; private set; }

        public override IItem CreateItem()
        {
            return new HealItemBase(this);
        }
    }
}
