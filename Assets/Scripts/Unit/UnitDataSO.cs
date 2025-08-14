using Damageable;
using UnityEngine;
using Game.CharacterController.Data;
using BlueRacconGames.Pool;
using BlueRacconGames.Animation;

namespace Units.Implementation
{
    public abstract class UnitDataSO : ScriptableObject
    {
        [field: SerializeField] public Sprite UnitSprite { get; private set; }
        [field: SerializeField] public RuntimeAnimatorController AnimatorController { get; private set; }
        [field: SerializeField] public CharacterControllerDataSO CharacterControllerDataSO { get; private set; }
        [field: SerializeField] public ParticlePoolItem LaunchVFX { get; private set; }
        [field: SerializeField] public int DoNothingTickDuration { get; private set; }
        [field: SerializeField] public AnimationDataSO NoPlayAnimation { get; private set; }
        public abstract DamagableDataSO DamagableDataSO { get; }
    }
}