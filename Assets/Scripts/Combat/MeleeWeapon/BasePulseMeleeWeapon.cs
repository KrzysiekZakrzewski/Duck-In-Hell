using BlueRacconGames.MeleeCombat.Implementation;
using DG.Tweening;
using System;
using Units;
using UnityEngine;

namespace BlueRacconGames.MeleeCombat
{
    public class BasePulseMeleeWeapon : MeleeWeaponBase
    {
        private readonly float scaleMultiplayer;
        private readonly float animationDuration;
        private readonly float pulseDelay;
        private readonly float pulseSequenceDelay;
        private readonly int pulseAmountPerAction;

        public BasePulseMeleeWeapon(BasePulseMeleeWeaponFactorySO initialData) : base(initialData)
        { 
            scaleMultiplayer = initialData.ScaleMultiplayer;
            animationDuration = initialData.AnimationDuration;
            pulseDelay = initialData.PulseDelay;
            pulseSequenceDelay = initialData.PulseSequenceDelay;
            pulseAmountPerAction = initialData.PulseAmountPerAction;
        }

        public Sequence SetupAttackSequence(Transform attackPoint, Action completedPulseCallback, Action resetCallback)
        {
            Sequence pulseSequnce = SetupPulseAttackSequnce(attackPoint, completedPulseCallback, resetCallback);

            Sequence attackSequnce = DOTween.Sequence();

            attackSequnce.Append(pulseSequnce.Play());
            attackSequnce.SetDelay(pulseSequenceDelay);
            attackSequnce.SetLoops(-1, LoopType.Restart);

            return attackSequnce;
        }

        public override bool Use(IUnit source)
        {
            throw new NotImplementedException();
        }

        private Sequence SetupPulseAttackSequnce(Transform attackPoint, Action completedPulseCallback, Action resetCallback)
        {
            var targetScale = attackPoint.localScale + (attackPoint.localScale * scaleMultiplayer);

            Sequence pulseSequnce = DOTween.Sequence();
            pulseSequnce.Pause();
            pulseSequnce.SetAutoKill(false);

            pulseSequnce.Append(attackPoint.transform.DOScale(targetScale, animationDuration)
                .OnComplete(() => completedPulseCallback?.Invoke()));
            pulseSequnce.Append(attackPoint.transform.DOScale(Vector3.one, animationDuration)
                .OnComplete(() => resetCallback?.Invoke()));
            pulseSequnce.SetDelay(pulseDelay);
            pulseSequnce.SetLoops(pulseAmountPerAction, LoopType.Restart)
                .OnComplete(() => pulseSequnce.Restart());

            return pulseSequnce;
        }
    }
}