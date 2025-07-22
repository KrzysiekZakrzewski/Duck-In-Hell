using Game.Managers;
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
        private GameplayManager gameplayManager;

        private readonly List<PoolItemBase> activePoolObjectItemLUT = new();
        private readonly Dictionary<PoolItemBase, ObjectPool<PoolItemBase>> itemPrefabToPoolLut = new();
        private readonly Dictionary<PoolItemBase, ObjectPool<PoolItemBase>> itemInstanceToPoolLut = new();

        public GameObject GameObject => gameObject;

        [Inject]
        private void Inject(DiContainer container, GameplayManager gameplayManager)
        {
            this.container = container;

            this.gameplayManager = gameplayManager;

            this.gameplayManager.OnGameplaySetup += GameplayManager_OnGameplaySetup;
        }

        private void GameplayManager_OnGameplaySetup()
        {
            gameplayManager.OnGameplayRestart += GameplayManager_OnGameRestart;
        }

        private void GameplayManager_OnGameRestart()
        {
            gameplayManager.OnGameplayRestart -= GameplayManager_OnGameRestart;

            Clear();
        }

        [ContextMenu("Clear")]
        public void Clear()
        {
            for(int i = activePoolObjectItemLUT.Count - 1;  i >= 0; i--)
            {
                var item = activePoolObjectItemLUT[i];

                itemInstanceToPoolLut[item].Release(item);
            }

            foreach (var item in itemPrefabToPoolLut.Values)
                item.Clear();

            foreach (var item in itemInstanceToPoolLut.Values)
                item.Clear();
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
                pool = new ObjectPool<PoolItemBase>(() => CreateItem(prefab), OnGetItem, OnReleaseItem, OnDestroyItem);
                itemPrefabToPoolLut.Add(prefab, pool);
            }

            PoolItemBase newParticle = pool.Get();
            itemInstanceToPoolLut.TryAdd(newParticle, pool);
            return newParticle;
        }

        private void OnGetItem(PoolItemBase item)
        {
            item.OnExpireE += Item_OnExpireE;
            activePoolObjectItemLUT.Add(item);
        }

        private void OnReleaseItem(PoolItemBase item)
        {
            activePoolObjectItemLUT.Remove(item);
        }
        private void OnDestroyItem(PoolItemBase item)
        {
            Destroy(item.gameObject);
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
