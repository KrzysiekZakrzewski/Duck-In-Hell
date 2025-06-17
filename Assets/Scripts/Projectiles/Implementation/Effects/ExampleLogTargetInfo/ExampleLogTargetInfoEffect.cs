using BlueRacconGames.MeleeCombat;
using BlueRacconGames.Pool;
using Projectiles;
using UnityEngine;

namespace BasicImplementationSample.Scripts.Effects.ExampleLogTargetInfo
{
    public class ExampleLogTargetInfoEffect : IProjectileTargetEffect
    {
        public void Execute(IPoolItemEmitter sourceEmitter, IDamagableTarget target)
        {
            Debug.Log($"Hit {target.GameObject.name}");
        }
    }
}
