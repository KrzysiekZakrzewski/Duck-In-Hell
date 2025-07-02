using BlueRacconGames.Animation;
using BlueRacconGames.Pool;
using Damageable;
using Game.CharacterController;
using Game.HUD;
using System.Collections.Generic;
using TimeTickSystems;
using UnityEngine;
using Zenject;

namespace Units.Implementation
{
    public abstract class UnitBase : MonoBehaviour, IUnit
    {
        private List<PoolItemBase> childPooledItem = new();

        protected UnitHUD unitHud;

        protected UnitDataSO initializeData;
        protected Collider2D unitCollider2D;
        protected CharacterController2D characterController;
        protected SpriteRenderer spriteRenderer;
        protected UnitAnimationControllerBase animationController;
        protected IDamageable damageable;
        protected bool unitSleepInteraction;
        protected bool isDoSomething;
        protected int doNothingTickDuration = 20;
        protected int tick;
        protected ParticlePoolItem launchVFX;

        public GameObject GameObject => gameObject;
        public IDamageable Damageable => damageable;
        public bool IsDoSomething => isDoSomething;
        public bool IsInitialized { get; protected set; }
        public DefaultPooledEmitter DefaultPooledEmitter { get; protected set; }

        [Inject]
        private void Inject(DefaultPooledEmitter pooledEmitter)
        {
            DefaultPooledEmitter = pooledEmitter;
        }

        public virtual void Launch()
        {
            unitCollider2D = GetComponent<Collider2D>();
            characterController = GetComponent<CharacterController2D>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            animationController = GetComponent<UnitAnimationControllerBase>();
            damageable ??= GetComponent<IDamageable>();
            damageable.OnTakeDamageE += unitHud.HealthBar.UpdateBar;

            TimeTickSystem.OnTick += OnTick;
            TimeTickSystem.OnBigTick += OnBigTick;

            ResetUnit();
        }
        public virtual void ResetUnit()
        {
            damageable?.ResetDamagable();
        }
        public virtual void SetUnitData(UnitDataSO initializeData)
        {
            this.initializeData = initializeData;

            spriteRenderer.sprite = initializeData.UnitSprite;

            MatchColliderToSprite();
        }
        public void WakeUpInteraction()
        {
            unitSleepInteraction = false;
        }
        public Vector2 GetOnSpritePosition(PositionOnSprite position)
        {
            if (spriteRenderer == null) return Vector2.zero;

            var centerPosition = spriteRenderer.bounds.center;

            switch (position)
            {
                case PositionOnSprite.Middle:
                    return centerPosition;
                case PositionOnSprite.Bottom:
                    return new Vector2(centerPosition.x, transform.position.y - (spriteRenderer.bounds.size.y / 2f));
                default:
                    return centerPosition;
            }
        }
        public void PushPoolItem(PoolItemBase poolItem)
        {
            if (childPooledItem.Contains(poolItem)) return;

            childPooledItem.Add(poolItem);
        }
        public void PopPoolItem(PoolItemBase poolItem)
        {
            if (!childPooledItem.Contains(poolItem)) return;

            childPooledItem.Remove(poolItem);
        }

        private void ExpireChildPoolItem()
        {
            foreach (PoolItemBase poolItem in childPooledItem)
                poolItem.ForceExpire();

            childPooledItem.Clear();
        }
        private void ExpireInternal(IDamageable damageable)
        {
            damageable.OnExpireE -= ExpireInternal;
            damageable.OnTakeDamageE -= unitHud.HealthBar.UpdateBar;

            TimeTickSystem.OnTick -= OnTick;
            TimeTickSystem.OnBigTick -= OnBigTick;
        }
        private void MatchColliderToSprite()
        {
            if (unitCollider2D == null || spriteRenderer == null) return;

            float spriteWidth = spriteRenderer.sprite.bounds.size.x;
            float spriteHeight = spriteRenderer.sprite.bounds.size.y;

            switch (unitCollider2D)
            {
                case CircleCollider2D:
                    float diameter = Mathf.Min(spriteWidth, spriteHeight);
                    float radius = diameter / 2f;

                    CircleCollider2D circleCollider2D = unitCollider2D as CircleCollider2D;
                    circleCollider2D.radius = radius;
                    break;
                case BoxCollider2D:
                    BoxCollider2D boxCollider2D = unitCollider2D as BoxCollider2D;
                    boxCollider2D.size = new Vector2(spriteWidth, spriteHeight);
                    break;
            }
        }
        private void SleepUnitInteraction()
        {
            animationController.PlayAnimation(initializeData.NoPlayAnimation);

            unitSleepInteraction = true;
        }
        private bool CheckUnitDoSomething()
        {
            return characterController.IsMoving;
        }
        private void OnTick(object sender, OnTickEventArgs e)
        {
            if (isDoSomething || unitSleepInteraction) return;

            tick++;

            if (tick < doNothingTickDuration) return;

            SleepUnitInteraction();
        }
        private void OnBigTick(object sender, OnTickEventArgs e)
        {
            isDoSomething = CheckUnitDoSomething();
        }
    }
}