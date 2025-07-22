using BlueRacconGames.MeleeCombat;
using BlueRacconGames.Pool;
using Game.Particle;
using TimeTickSystems;
using Units;
using UnityEngine;

namespace BlueRacconGames.Cards.Effects
{
    public class FreezeHitCardEffect : PassiveHitCardEffect
    {
        [SerializeField] private int duration;
        [SerializeField] private float percentChance;
        [SerializeField] private bool overrideParticle;
        [SerializeField, ShowIf(nameof(overrideParticle), true)] private ParticleSetterData[] particleSetterDatas;

        public override void ApplyEffect(CardsController cardsController, IUnit source)
        {
            base.ApplyEffect(cardsController, source);

            var durationInSec = TimeTickSystem.GetTimeInSeconds(duration);

            particleSetterDatas[0].Value = durationInSec;
            particleSetterDatas[1].Value = durationInSec + 1;//TO DO Change this
        }
        public override void DiscardEffect()
        {
            
        }
        public override void Execute(IDamagableTarget target, DefaultPooledEmitter pooledEmitter)
        {
            if (!IsSuccesfull()) return;

            base.Execute(target, pooledEmitter);
        }

        protected override void ExecuteInternal(TargetData targetData, ParticlePoolItem particleEffect)
        {
            var unit = targetData.Unit;

            unit.UpdateUnitEnable(false, Units.Implementation.StopUnitType.Freeze);

            particleEffect.TryGetComponent<ParticleParameterSetter>(out var setter);
            particleEffect.OnExpireE += (PoolItemBase item) => OnEffectEnded(targetData);

            setter.UpdateDatas(particleSetterDatas);
        }
        protected override void OnEffectEnded(TargetData targetData)
        {
            targetData.Unit.UpdateUnitEnable(true, Units.Implementation.StopUnitType.Full);

            targets.Remove(targetData.DamagableTarget);
        }

        private bool IsSuccesfull() => Random.value < percentChance;
    }
}