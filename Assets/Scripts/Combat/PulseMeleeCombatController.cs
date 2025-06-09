using BlueRacconGames.MeleeCombat.Implementation;
using DG.Tweening;
using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    public class PulseMeleeCombatController : MeleeCombatControllerBase
    {
        [SerializeField] protected BasePulseMeleeWeaponFactorySO basePulseTouchWeapon;

        private Sequence scaleSequence;

        protected override void Awake()
        {
            base.Awake();

            weapon = basePulseTouchWeapon.CreateWeapon();

            Initialize();
        }

        public void Initialize()
        {
            BasePulseMeleeWeapon pulseWeapon = weapon as BasePulseMeleeWeapon;

            scaleSequence = pulseWeapon.SetupAttackSequence(attackPoint, Attack, ResetTargets);
        }
        public void DeInitialize()
        {
            scaleSequence?.Kill();
            scaleSequence = null;
        }
        [ContextMenu("Pause Attack")]
        public void PauseAttack()
        {
            scaleSequence?.Pause();
        }
        [ContextMenu("Unpause Attack")]
        public void UnPauseAttack()
        {
            scaleSequence?.Play();
        }
    }
}