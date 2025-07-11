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
            var bombItem = SpawnItem<BombObject>();
            bombItem.Setup(initialData, Level);
        }
    }
}