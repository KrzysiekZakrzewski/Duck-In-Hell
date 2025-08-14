using BlueRacconGames.Pool;
using Game.Item;
using UnityEngine;

namespace BlueRacconGames.Cards.Effects
{
    public class BombPassiveLoopCardEffect : SpawnPassiveLoopCardEffect
    {
        [SerializeField] private BombObjectBaseDataSO initialData;

        public override void Execute()
        {
            var levelBoost = (Level - 1) * initialData.LevelPercentBoost;

            float randomValue = Random.value;

            if (randomValue > initialData.BaseSpawnChance * levelBoost) return;

            var bombItem = SpawnItem<BombObject>();
            bombItem.Setup(initialData, Level);
        }
    }
}