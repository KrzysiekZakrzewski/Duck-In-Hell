using System;
using System.Collections.Generic;
using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    public interface IMeleeWeapon
    {
        List<ILaunchAttackEffect> LaunchAttackEffects { get; }
        List<IMeleeTargetEffect> MeleeWeaponTargetHitEffects { get; }
        event Action<IDamagableTarget> OnHitE;

        void OnAttack(MeleeCombatControllerBase source);
        void ResetWeapon();
        void OnHit(MeleeCombatControllerBase source, IDamagableTarget target);
        void IncreaseDamageValue(int value);
        void DecreaseDamageValue(int value);
    }
}