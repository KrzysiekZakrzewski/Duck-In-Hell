using Game.Item;
using Game.Item.Factory.Implementation;
using System;
using System.Collections.Generic;
using static Unity.VisualScripting.Member;

namespace BlueRacconGames.MeleeCombat
{
    public abstract class MeleeWeaponBase : ItemBase, IMeleeWeapon
    {
        private readonly HashSet<IDamagableTarget> hitTargets = new();

        public List<ILaunchAttackEffect> LaunchAttackEffects { get; }
        public List<IMeleeTargetEffect> MeleeWeaponTargetHitEffects { get; }

        public event Action<IDamagableTarget> OnHitE;

        protected MeleeWeaponBase(MeleeWeaponBaseFactorySO initialData) : base(initialData)
        {
            LaunchAttackEffects = new();
            MeleeWeaponTargetHitEffects = new();

            foreach(ILaunchAttackEffectFactory launchAttackEffectFactory in initialData.LaunchAttackEffectFactory)
            {
                LaunchAttackEffects.Add(launchAttackEffectFactory.CreateEffect());
            }

            foreach (IMeleeTargetEffectFactory meleeWeaponTargetEffectFactory in initialData.MeleeWeaponTargetEffectFactory)
            {
                MeleeWeaponTargetHitEffects.Add(meleeWeaponTargetEffectFactory.CreateEffect());
            }
        }

        public void OnAttack(MeleeCombatControllerBase source)
        {
            OnAttackInternal(source);
        }
        public void OnHit(MeleeCombatControllerBase source, IDamagableTarget target)
        {
            if (hitTargets.Contains(target))
                return;

            hitTargets.Add(target);
            OnHitInternal(source, target);
        }
        public void ResetWeapon()
        {
            hitTargets.Clear();
        }

        private void OnAttackInternal(MeleeCombatControllerBase source)
        {
            foreach(ILaunchAttackEffect effect in LaunchAttackEffects)
            {
                effect.Execute(source);
            }
        }
        private void OnHitInternal(MeleeCombatControllerBase source, IDamagableTarget target)
        {
            OnHitE?.Invoke(target);

            foreach (IMeleeTargetEffect meleeWeaponTargetHitEffect in MeleeWeaponTargetHitEffects)
            {
                meleeWeaponTargetHitEffect.Execute(source, target);
            }
        }

        public void IncreaseDamageValue(int value)
        {
            foreach (IMeleeTargetEffect meleeWeaponTargetHitEffect in MeleeWeaponTargetHitEffects)
            {
                if(meleeWeaponTargetHitEffect is DealDamageTargetEffect)
                {
                    DealDamageTargetEffect dealDamageTargetEffect = meleeWeaponTargetHitEffect as DealDamageTargetEffect;
                }
            }
        }

        public void DecreaseDamageValue(int value)
        {
            throw new NotImplementedException();
        }
    }
}