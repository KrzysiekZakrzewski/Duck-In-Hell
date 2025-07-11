using Game.Particle;
using Units;
using UnityEngine;
using Zenject;

namespace BlueRacconGames.Pool
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticlePoolItem : PoolItemBase
    {
        private ParticleSystem system;

        private ParticleToRadiusScale scaler;

        private void Awake()
        {
            system = GetComponent<ParticleSystem>();
            scaler = GetComponent<ParticleToRadiusScale>();
            var main = system.main;
            main.stopAction = ParticleSystemStopAction.Callback;
        }

        private void OnParticleSystemStopped()
        {
            Expire();
        }

        public override void ForceExpire()
        {
            system = GetComponent<ParticleSystem>();
            system.Stop();
            Expire();
            Debug.Log("ExpireForce");
        }

        public void UpdateScale(float scaleFactor)
        {
            if (scaler == null) return;

            scaler.UpdateParticleScale(scaleFactor);
        }
    }
}