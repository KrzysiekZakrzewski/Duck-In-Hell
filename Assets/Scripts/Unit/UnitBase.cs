using BlueRacconGames.Animation;
using BlueRacconGames.Pool;
using Damageable;
using Game.CharacterController;
using Game.HUD;
using TimeTickSystems;
using UnityEngine;

namespace Units.Implementation
{
    public abstract class UnitBase : MonoBehaviour, IUnit
    {
        protected UnitHUD unitHud;

        protected Collider2D unitCollider2D;
        protected CharacterController2D characterController;
        protected SpriteRenderer spriteRenderer;
        protected UnitAnimationControllerBase animationController;
        protected IDamageable damageable;
        protected bool unitSleepInteraction;
        protected bool isDoSomething;
        protected int doNothingTickDuration = 20;
        protected int tick;

        public GameObject GameObject => gameObject;
        public IDamageable Damageable => damageable;

        public bool IsDoSomething => isDoSomething;

        public bool IsInitialized { get; protected set; }

        public virtual void Lauch()
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
        public virtual void SetUnitData(UnitDataSO unitDataSO)
        {
            spriteRenderer.sprite = unitDataSO.UnitSprite;

            MatchColliderToSprite();
        }
        public void WakeUpInteraction()
        {
            unitSleepInteraction = false;
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
            animationController.DoNothingAnimation();

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