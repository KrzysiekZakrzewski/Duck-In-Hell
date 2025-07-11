using BlueRacconGames.Pool;
using UnityEngine;
using DG.Tweening;
using Timers;
using Damageable;
using Zenject;

namespace Game.Item
{
    public class BombObject : PoolItemBase
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private BaseCountdownPresentation countdownPresentation;

        private BombObjectBaseDataSO initialData;

        protected Vector3 baseScale;
        private float explodeDuration;
        private float scaleMultiplayer;
        private float explosionRadius;
        private int damageValue;
        private Sequence sequence;
        private Countdown countdown;

        [Inject]
        private void Inject()
        {
            baseScale = transform.localScale;
        }

        public void Setup(BombObjectBaseDataSO initialData, int level)
        {
            this.initialData = initialData;

            transform.localScale = baseScale;

            CalculateStats(level);

            sourceEmitter.EmitItem<ParticlePoolItem>(initialData.SpawnVFX, transform.position, Vector3.zero);

            SetupExplodeAnimation();
            countdown = new Countdown(countdownPresentation);
            countdown.OnCountdownE += Explode;
            countdown.OnCountdownUpdatedE += CalculatePulseSpeed;

            StartExplodeCountdown();
        }

        protected override void ExpireInternal()
        {
            base.ExpireInternal();

            sequence?.Kill();
            sequence = null;
        }

        private void StartExplodeCountdown()
        {
            countdown.StartCountdown(explodeDuration, 1f);
            sequence.Play();
        }
        private void SetupExplodeAnimation()
        {
            Vector3 targetScale = transform.localScale * scaleMultiplayer;

            sequence = DOTween.Sequence();
            sequence.Pause();
            sequence.SetAutoKill(true);

            sequence.Append(transform.DOScale(targetScale, initialData.BasePulseDuration));
            sequence.Append(transform.DOScale(baseScale, initialData.BasePulseDuration));
            sequence.SetLoops(-1, LoopType.Restart);
        }
        private void Explode()
        {
            countdown.OnCountdownE -= Explode;
            countdown.OnCountdownUpdatedE -= CalculatePulseSpeed;

            sequence?.Kill();
            sequence = null;

            var explodeVFX = sourceEmitter.EmitItem<ParticlePoolItem>(initialData.ExplodeVFX, transform.position, Vector3.zero);

            explodeVFX.UpdateScale(explosionRadius);

            DealDamageToTargets();

            Expire();
        }
        private void CalculatePulseSpeed(float remainingTime)
        {
            var speed = initialData.BaseAnimationSpeed + ((initialData.MaxAnimationSpeed - initialData.BaseAnimationSpeed * 
                (remainingTime - explodeDuration)) / explodeDuration);

            sequence.timeScale = speed;
        }
        private void DealDamageToTargets()
        {
            var colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, initialData.DamageableLayer);

            foreach (var collider in colliders)
            {
                if (!collider.gameObject.TryGetComponent<IDamageable>(out var target)) continue;

                target.TakeDamage(damageValue, initialData.DamageMode);
            }
        }
        private void CalculateStats(int level)
        {
            var levelBoost = (level - 1) * initialData.LevelPercentBoost;

            int scaleMultiplayerFactor = 10;

            explodeDuration = initialData.ExplodeDuration;
            damageValue = Mathf.RoundToInt(initialData.DamageValue + initialData.DamageValue * levelBoost);
            explosionRadius = initialData.ExplosionRadius + initialData.ExplosionRadius * levelBoost;
            scaleMultiplayer = initialData.ScaleMultiplayer + (initialData.ScaleMultiplayer * levelBoost) / scaleMultiplayerFactor;
        }
    }
}