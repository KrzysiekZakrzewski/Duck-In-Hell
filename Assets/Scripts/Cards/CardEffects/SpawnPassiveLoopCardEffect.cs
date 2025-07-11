using BlueRacconGames.Pool;
using UnityEngine;

namespace BlueRacconGames.Cards.Effects
{
    public abstract class SpawnPassiveLoopCardEffect : PassiveLoopCardEffect
    {
        [SerializeField] private PoolItemBase itemBase;

        protected virtual T SpawnItem<T>() where T : PoolItemBase
        {
            var spawnPosition = source.GameObject.transform.position;

            var item = cardController.PoolEmiter.EmitItem<PoolItemBase>(itemBase, spawnPosition, Vector3.zero);

            return item as T;
        }
    }
}