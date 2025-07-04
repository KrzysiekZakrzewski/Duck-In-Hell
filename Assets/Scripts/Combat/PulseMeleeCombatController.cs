using BlueRacconGames.Cards;
using BlueRacconGames.MeleeCombat.Implementation;
using BlueRacconGames.Pool;
using DG.Tweening;
using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    public class PulseMeleeCombatController : MeleeCombatControllerBase
    {
        [SerializeField] protected BasePulseMeleeWeaponFactorySO basePulseTouchWeapon;

        private Sequence scaleSequence;

        public override void Initialize(DefaultPooledEmitter pooledEmitter, CardsController cardController)
        {
            base.Initialize(pooledEmitter, cardController);

            weapon = basePulseTouchWeapon.CreateWeapon();

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