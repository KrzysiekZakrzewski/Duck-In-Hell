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
        event Action OnHealE;
        event Action<IUnit> OnDeadE;
        event Action<IDamageable> OnExpireE;

        void Launch(DamagableDataSO damagableDataSO);
        void TakeDamage(int damageValue, out bool isFatalDamage, DamageMode damageMode = DamageMode.Normal);
        void Heal(int healValue);
        void IncreaseHealt(int increaseValue);
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