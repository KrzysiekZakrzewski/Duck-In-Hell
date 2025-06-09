using Damageable;
using UnityEngine;
using Game.CharacterController.Data;
using Game.HUD;

namespace Units.Implementation
{
    public abstract class UnitDataSO : ScriptableObject
    {
        [field: SerializeField] public Sprite UnitSprite { get; private set; }
        [field: SerializeField] public CharacterControllerDataSO CharacterControllerDataSO { get; private set; }
        [field: SerializeField] public UnitHUD UnitHUD { get; private set; }
        public abstract IDamagableDataSO DamagableDataSO { get; }
    }
}