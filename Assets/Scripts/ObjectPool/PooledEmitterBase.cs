using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

namespace BlueRacconGames.Pool
{
    [Serializable]
    public abstract class PooledEmitterBase : MonoBehaviour, IPoolItemEmitter
    {
        private DiContainer container;

        private readonly Dictionary<PoolItemBase, ObjectPool<PoolItemBase>> itemPrefabToPoolLut = new();
        private readonly Dictionary<PoolItemBase, ObjectPool<PoolItemBase>> itemInstanceToPoolLut = new();

        [Inject]
        private void Inject(DiContainer container)
        {
            this.container = container;
        }

        public void Clear()
        {
            itemPrefabToPoolLut.Clear();
            itemInstanceToPoolLut.Clear();
        }

        public T EmitItem<T>(PoolItemBase prefab, Vector3 startPosition, Vector3 direction) where T : class
        {
            PoolItemBase newItem = GetItemFromPool(prefab);
            newItem.Launch(this, startPosition, direction);

            return newItem.GetComponent<T>();
        }

        private PoolItemBase GetItemFromPool(PoolItemBase prefab)
        {
            if (!itemPrefabToPoolLut.TryGetValue(prefab, out ObjectPool<PoolItemBase> pool))
            {
                pool = new ObjectPool<PoolItemBase>(() => CreateItem(prefab), OnGetItem, OnReleaseItem);
                itemPrefabToPoolLut.Add(prefab, pool);
            }

            PoolItemBase newParticle = pool.Get();
            itemInstanceToPoolLut.TryAdd(newParticle, pool);
            return newParticle;
        }

        private void OnGetItem(PoolItemBase item)
        {
            item.OnExpireE += Item_OnExpireE;
        }

        private void OnReleaseItem(PoolItemBase item)
        {

        }

        private void Item_OnExpireE(PoolItemBase item)
        {
            item.OnExpireE -= Item_OnExpireE;
            itemInstanceToPoolLut[item].Release(item);
        }

        private PoolItemBase CreateItem(PoolItemBase item)
        {
            GameObject newGameObject = GameObject.Instantiate(item as MonoBehaviour).gameObject;
            newGameObject.transform.SetParent(transform);
            PoolItemBase poolItem = newGameObject.GetComponent<PoolItemBase>();

            container.Inject(poolItem);

            return poolItem;
        }
    }
}
