using UnityEngine;

namespace Game.Item.Factory
{
    public abstract class BuffStatItemFactorySO : ActionItemFactorySO
    {
        [field: SerializeField] public int BuffValue { get; private set; }
    }
}