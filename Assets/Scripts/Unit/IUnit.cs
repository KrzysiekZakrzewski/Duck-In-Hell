using BlueRacconGames;
using BlueRacconGames.Pool;
using Damageable;
using Units.Implementation;
using UnityEngine;

namespace Units 
{
    public interface IUnit : IGameObject
    {
        DefaultPooledEmitter DefaultPooledEmitter { get; }
        public IDamageable Damageable { get; }
        void SetUnitData(UnitDataSO unitDataSO);
        void ResetUnit();
        void WakeUpInteraction();
        Vector2 GetOnSpritePosition(PositionOnSprite position);
        void PushPoolItem(PoolItemBase poolItem);
        void PopPoolItem(PoolItemBase poolItem);
        void UpdateUnitEnable(bool enableValue, StopUnitType stopType = StopUnitType.Movement);
    }

    public enum PositionOnSprite
    {
        Middle,
        Top,
        Bottom,
        TopLeftCorner,
        TopRightCorner,
        BottomLeftCorner,
        BottomRightCorner
    }
}