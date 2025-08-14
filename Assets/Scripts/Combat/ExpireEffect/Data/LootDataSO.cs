using BlueRacconGames.Pool;
using Game.Item.Factory;
using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    [CreateAssetMenu(fileName = nameof(LootDataSO), menuName = nameof(BlueRacconGames) + "/" + nameof(MeleeCombat) + "/" + nameof(LootDataSO))]
    public class LootDataSO : ScriptableObject
    {
        [field: SerializeField] public PoolItemBase ItemPrefab { get; private set; }
        [field: SerializeField, Range(0f, 1f)] public float PercentLootChance { get; private set; }
        [field: SerializeField] public LootData[] DropItems { get; private set; }
    }

    [System.Serializable]
    public class LootData
    {
        [SerializeField] private LootAmountType lootAmountType;
        [SerializeField, ShowIf(nameof(lootAmountType), LootAmountType.Base)] private int baseLootAmount;
        [SerializeField, ShowIf(nameof(lootAmountType), LootAmountType.Random)] private int minLootAmount;
        [SerializeField, ShowIf(nameof(lootAmountType), LootAmountType.Random)] private int maxLootAmount;
        [field: SerializeField, Range(-1f, 1f)] public float PercentChanceToDropModifier { get; private set; }
        [field: SerializeField] public ItemFactorySO ItemFactorySO { get; private set; }

        public int GetAmount()
        {
            return lootAmountType switch
            {
                LootAmountType.Base => baseLootAmount,
                LootAmountType.Random => Random.Range(minLootAmount, maxLootAmount),
                _ => 0,
            };
        }
        public float PercentChanceToDrop 
            => ItemFactorySO.BasePercentChance + (ItemFactorySO.BasePercentChance * PercentChanceToDropModifier);

        private enum LootAmountType
        {
            Base,
            Random
        }
    }
}
