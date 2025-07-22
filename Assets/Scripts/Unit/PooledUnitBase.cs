using BlueRacconGames.Animation;
using BlueRacconGames.Pool;
using Damageable;
using Game.CharacterController;
using Game.HUD;
using System.Collections.Generic;
using TimeTickSystems;
using UnityEngine;
using Zenject;
using static UnityEngine.Rendering.DebugUI;

namespace Units.Implementation
{
    public abstract class PooledUnitBase : PoolItemBase, IUnit
    {
        [SerializeField] protected bool useOtherHUD;

        [SerializeField, HideIf(nameof(useOtherHUD), true)] protected UnitHUD unitHUD;
        [SerializeField, HideIf(nameof(useOtherHUD), true)] protected Vector2 hudPositionOffset;

        private readonly List<PoolItemBase> childPooledItem = new();

        protected UnitDataSO initializeData;
        protected Collider2D unitCollider2D;
        protected CharacterController2D characterController;
        protected SpriteRenderer spriteRenderer;
        protected UnitAnimationControllerBase animationController;
        protected IDamageable damageable;
        protected bool isDoSomething;
        protected bool isLazzy;
        protected int tick;
        public IDamageable Damageable => damageable;

        public DefaultPooledEmitter DefaultPooledEmitter {  get; private set; }

        [Inject]
        private void Inject(DefaultPooledEmitter pooledEmitter)
        {
            DefaultPooledEmitter = pooledEmitter;
        }

        public override void Launch(IPoolItemEmitter sourceEmitter, Vector3 startPosition, Vector3 direction)
        {
            base.Launch(sourceEmitter, startPosition, direction);

            unitCollider2D = GetComponent<Collider2D>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            animationController = GetComponent<UnitAnimationControllerBase>();
            characterController = GetComponent<CharacterController2D>();
            damageable ??= GetComponent<IDamageable>();

            TimeTickSystem.OnTick += OnTick;
            TimeTickSystem.OnBigTick += OnBigTick;
        }
        public virtual void SetUnitData(UnitDataSO unitDataSO)
        {
            this.initializeData = unitDataSO;
            spriteRenderer.sprite = unitDataSO.UnitSprite;

            damageable?.Launch(unitDataSO.DamagableDataSO);
            damageable.OnExpireE += (IDamageable damageable) => Expire();
            damageable.OnTakeDamageE += unitHUD.HealthBar.UpdateBar;
            damageable.OnHealE += unitHUD.HealthBar.UpdateBar;

            characterController.SetData(unitDataSO.CharacterControllerDataSO);

            ResetUnit();

            MatchColliderToSprite();
        }
        public virtual void ResetUnit()
        {
            damageable?.ResetDamagable();

            unitHUD.HealthBar.Launch(damageable.CurrentHealth, damageable.MaxHealth);
        }
        public void WakeUpInteraction()
        {

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
            if(childPooledItem.Contains(poolItem)) return;

            childPooledItem.Add(poolItem);
        }
        public void PopPoolItem(PoolItemBase poolItem)
        {
            if (!childPooledItem.Contains(poolItem)) return;

            childPooledItem.Remove(poolItem);
        }
        public virtual void UpdateUnitEnable(bool enableValue, StopUnitType stopType = StopUnitType.Movement)
        {
            switch (stopType)
            {
                case StopUnitType.Movement:
                    UpdateUnitMoveEnable(enableValue, false);
                    break;
                case StopUnitType.MovementWithForces:
                    UpdateUnitMoveEnable(enableValue, true);
                    break;
                case StopUnitType.Damage:
                    UpdateUnitDamageableEnable(enableValue);
                    break;
                case StopUnitType.Attack:
                    UpdateUnitAttackEnable(enableValue);
                    break;
                case StopUnitType.Freeze:
                    UpdateUnitMoveEnable(enableValue, true);
                    UpdateUnitAttackEnable(enableValue);
                    break;
                case StopUnitType.Full:
                    UpdateUnitMoveEnable(enableValue, true);
                    UpdateUnitDamageableEnable(enableValue);
                    UpdateUnitAttackEnable(enableValue);
                    break;
                default:
                    break;
            }
        }

        protected abstract void UpdateUnitAttackEnable(bool enableValue);
        protected virtual void UpdateUnitMoveEnable(bool enableValue, bool stopForces)
        {
            characterController.SetCanMove(enableValue, stopForces);
        }
        protected virtual void UpdateUnitDamageableEnable(bool enableValue)
        {
            damageable.SetDamagableOn(enableValue);
        }
        protected override void ExpireInternal()
        {
            base.ExpireInternal();

            damageable.OnExpireE -= (IDamageable damageable) => Expire();
            damageable.OnTakeDamageE -= unitHUD.HealthBar.UpdateBar;
            damageable.OnHealE -= unitHUD.HealthBar.UpdateBar;

            TimeTickSystem.OnTick -= OnTick;
            TimeTickSystem.OnBigTick -= OnBigTick;

            Debug.Log("Remove");
            ExpireChildPoolItem();
        }

        private void ExpireChildPoolItem()
        {
            foreach (PoolItemBase poolItem in childPooledItem)
                poolItem.ForceExpire();

            childPooledItem.Clear();
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

            if (useOtherHUD) return;

            Vector3 hudPosition = new(hudPositionOffset.x, spriteHeight / 2 + hudPositionOffset.y, transform.position.z);

            unitHUD.transform.localPosition = hudPosition;
        }
        private void UnitDoNothing()
        {
            if(initializeData.NoPlayAnimation == null) return;

            isLazzy = true;

            animationController.PlayAnimation(initializeData.NoPlayAnimation);
        }
        private bool CheckUnitDoSomething()
        {
            return characterController.IsMoving;
        }
        private void OnTick(object sender, OnTickEventArgs e)
        {
            if (isDoSomething || isLazzy) return;

            tick++;

            if (tick < initializeData.DoNothingTickDuration) return;

            UnitDoNothing();
        }
        private void OnBigTick(object sender, OnTickEventArgs e)
        {
            isDoSomething = CheckUnitDoSomething();

            if (!isDoSomething) return;

            isLazzy = false;
        }
    }
    public enum StopUnitType
    {
        Movement,
        MovementWithForces,
        Damage,
        Attack,
        Freeze,
        Full
    }
}
