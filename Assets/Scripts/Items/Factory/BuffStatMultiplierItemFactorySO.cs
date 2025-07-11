using UnityEngine;

namespace Game.Item.Factory
{
    [CreateAssetMenu(fileName = nameof(PointsBuffItemFactorySO), menuName = nameof(Game) + "/" + nameof(Item.Factory) + "/" + nameof(PointsBuffItemFactorySO))]
    public abstract class BuffStatMultiplierItemFactorySO : ActionItemFactorySO
    {
        [field: SerializeField] public float Multiplier { get; protected set; }
        [field: SerializeField] public int Duration { get; protected set; }
    }
}
