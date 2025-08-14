using BlueRacconGames;
using System;
using Units;

namespace Damageable
{
    public interface IDamageable : IGameObject
    {
        int MaxHealth { get; }
        int CurrentHealth { get; }
        bool IsDead { get; }
        bool DamagableIsOn { get; }
        bool ExpireOnDead { get; }

        event Action<int, int> OnTakeDamageE;
        event Action<int, int> OnHealE;
        event Action<IUnit> OnDeadE;
        event Action<IDamageable> OnExpireE;

        void Launch(DamagableDataSO damagableDataSO);
        void TakeDamage(int damageValue, DamageMode damageMode = DamageMode.Normal);
        bool Heal(int healValue);
        void IncreaseHealt(int increaseValue, bool instantHeal = false);
        void DecreaseHealt(int decreaseValue);
        void OnDead();
        void ResetDamagable();
        void SetDamagableOn(bool value);
    }
    public enum DamageMode
    {
        Normal,
        Passive,
        IgnoreImmue
    }
}