using System;
using UnityEngine;

namespace BlueRacconGames.Pool
{
    public abstract class PoolItemBase : MonoBehaviour, IPoolItem
    {
        protected IPoolItemEmitter sourceEmitter;
        protected bool expired;

        public GameObject GameObject => gameObject;

        public event Action<PoolItemBase> OnLaunchE;
        public event Action<PoolItemBase> OnExpireE;

        public virtual void Launch(IPoolItemEmitter sourceEmitter, Vector3 startPosition, Vector3 direction)
        {
            ResetItem();
            this.sourceEmitter = sourceEmitter;
            transform.position = startPosition;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = CalculateRotation(angle);
            gameObject.SetActive(true);
            OnLaunchE?.Invoke(this);
        }
        public virtual void ResetItem()
        {
            expired = false; 
        }
        public virtual void ForceExpire()
        {
            Expire();
        }

        protected void Expire()
        {
            if (expired) return;

            expired = true;

            transform.SetParent(sourceEmitter.GameObject.transform);
            gameObject.SetActive(false);
            OnExpireE?.Invoke(this);

            ExpireInternal();
        }
        protected virtual void ExpireInternal()
        {

        }
        protected virtual Quaternion CalculateRotation(float angle)
        {
            return Quaternion.Euler(0, 0, angle);
        }
    }
}