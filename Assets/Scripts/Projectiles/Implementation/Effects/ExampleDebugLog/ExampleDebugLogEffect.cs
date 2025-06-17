using BlueRacconGames.MeleeCombat;
using BlueRacconGames.Pool;
using Projectiles;
using UnityEngine;

namespace BasicImplementationSample.Scripts.Effects.ExampleDebugLog
{
    public class ExampleDebugLogEffect : IProjectileTargetEffect
    {
        private readonly string message;
        
        public ExampleDebugLogEffect(string message)
        {
            this.message = message;
        }
        
        public void Execute(IPoolItemEmitter sourceEmitter, IDamagableTarget target)
        {
            Debug.Log(message);
        }
    }
}
