using Units.Implementation;
using UnityEngine;

namespace BlueRacconGames.Pool
{
    public class UnitPoolEmitter : PooledEmitterBase
    {
        public PooledUnitBase SpawnUnit(PooledUnitBase unitPrefab, ParticlePoolItem launchVFX, Vector3 spawnPoint, Vector3 direction)
        {
            var unit = EmitItem<PooledUnitBase>(unitPrefab, spawnPoint, direction);
            EmitItem<ParticlePoolItem>(launchVFX, spawnPoint, direction);
            return unit;
        }
    }
}
