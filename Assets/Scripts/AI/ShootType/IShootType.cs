using Projectiles.Implementation;
using UnityEngine;

namespace BlueRacconGames.AI
{
    public interface IShootType
    {
        void Shoot(DefaultProjectileEmitterController projectileEmitterControllerBase, Transform target);
    }
}