using UnityEngine;

namespace BlueRacconGames.Pool
{
    public interface IPoolItemEmitter : IGameObject
    {
        T EmitItem<T>(PoolItemBase item, Vector3 startPosition, Vector3 direction) where T : class;
        void Clear();
    }
}