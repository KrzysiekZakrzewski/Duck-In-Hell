using BlueRacconGames;
using Damageable;
using Units.Implementation;

namespace Units 
{
    public interface IUnit : IGameObject
    {
        bool IsDoSomething { get; }
        public IDamageable Damageable { get; }
        void SetUnitData(UnitDataSO unitDataSO);
        void ResetUnit();
        void WakeUpInteraction();
    }
}