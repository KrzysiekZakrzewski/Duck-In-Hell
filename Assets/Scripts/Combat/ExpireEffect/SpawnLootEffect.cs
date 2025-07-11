using BlueRacconGames.Pool;
using Damageable;
using Game.Item.Factory;
using Interactable;
using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    public class SpawnLootEffect : IExpireEffect
    {
        private readonly LootDataSO lootData;

        public SpawnLootEffect(LootDataSO lootDataSO)
        {
            this.lootData = lootDataSO;
        }

        public void Execute(IDamageable damageable, DefaultPooledEmitter defaultPooledEmitter)
        {
            DropData randomLootData = RandomizeLoot();

            if(randomLootData.Item == null) return;

            for (int i = 0; i < randomLootData.Amount; i++)
            {
                var item = defaultPooledEmitter.EmitItem<ItemInteractable>(lootData.ItemPrefab, damageable.GameObject.transform.position, Vector3.zero);

                item.SetupData(randomLootData.Item);
            }
        }

        private DropData RandomizeLoot()
        {
            float randomValue = Random.value;

            if (randomValue > lootData.PercentLootChance) return default;

            float totalChance = 0f;

            foreach (LootData data in lootData.DropItems)
                totalChance += data.PercentChanceToDrop;

            randomValue = Random.Range(0, totalChance);

            float cumulativeChance = 0f;

            LootData lootItemData = null;

            foreach (LootData data in lootData.DropItems)
            {
                cumulativeChance += data.PercentChanceToDrop;

                if (randomValue > cumulativeChance || randomValue == 0) continue;

                lootItemData = data;
            }

            if (lootItemData == null) return default;

            DropData dropData = new()
            {
                Item = lootItemData.ItemFactorySO,
                Amount = lootItemData.GetAmount()
            };

            return dropData;
        }

        private struct DropData 
        {
            public ItemFactorySO Item;
            public int Amount;
        }
    }
}