using BlueRacconGames.Animation;
using BlueRacconGames.Pool;
using Damageable;
using Game.CharacterController;
using Game.HUD;
using TimeTickSystems;
using UnityEngine;

namespace Units.Implementation
{
    public abstract class PooledUnitBase : PoolItemBase, IUnit
    {
        [SerializeField] protected UnitHUD unitHUD;

        [SerializeField] protected Vector2 hudPositionOffset;
        protected Collider2D unitCollider2D;
        protected CharacterController2D characterController;
        protected SpriteRenderer spriteRenderer;
        protected UnitAnimationControllerBase animationController;
        protected IDamageable damageable;
        protected bool isDoSomething;
        protected int doNothingTickDuration;
        protected int tick;
        public IDamageable Damageable => damageable;
        public bool IsDoSomething => isDoSomething;

        public override void Launch(IPoolItemEmitter sourceEmitter, Vector3 startPosition)
        {
            base.Launch(sourceEmitter, startPosition);

            unitCollider2D = GetComponent<Collider2D>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            animationController = GetComponent<UnitAnimationControllerBase>();
            characterController = GetComponent<CharacterController2D>();
            damageable ??= GetComponent<IDamageable>();
            damageable.OnExpireE += ExpireInternal;
            damageable.OnTakeDamageE += unitHUD.HealthBar.UpdateBar;

            TimeTickSystem.OnTick += OnTick;
            TimeTickSystem.OnBigTick += OnBigTick;

            ResetUnit();
        }
        public virtual void ResetUnit()
        {
            damageable?.ResetDamagable();
            unitHUD.HealthBar.Launch(damageable.CurrentHealth, damageable.MaxHealth);
        }
        public virtual void SetUnitData(UnitDataSO unitDataSO)
        {
            spriteRenderer.sprite = unitDataSO.UnitSprite;

            MatchColliderToSprite();
        }
        public void WakeUpInteraction()
        {

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

            Vector3 hudPosition = new(hudPositionOffset.x, spriteHeight / 2 + hudPositionOffset.y, transform.position.z);

            unitHUD.transform.localPosition = hudPosition;
        }
        private void ExpireInternal(IDamageable damageable)
        {
            damageable.OnExpireE -= ExpireInternal;
            damageable.OnTakeDamageE -= unitHUD.HealthBar.UpdateBar;
            TimeTickSystem.OnTick -= OnTick;
            TimeTickSystem.OnBigTick -= OnBigTick;

            Expire();
        }
        private void UnitDoNothing()
        {
            animationController.DoNothingAnimation();
        }
        private bool CheckUnitDoSomething()
        {
            return characterController.IsMoving;
        }
        private void OnTick(object sender, OnTickEventArgs e)
        {
            if (isDoSomething) return;

            tick++;

            if (tick < doNothingTickDuration) return;

            UnitDoNothing();
        }
        private void OnBigTick(object sender, OnTickEventArgs e)
        {
            isDoSomething = CheckUnitDoSomething();
        }
    }
}
