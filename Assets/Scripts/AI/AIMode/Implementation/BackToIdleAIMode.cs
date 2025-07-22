using BlueRacconGames.AI.Data;
using BlueRacconGames.AI.Factory;
using Damageable;
using RDG.Platforms;
using System.Collections;
using UnityEngine;

namespace BlueRacconGames.AI.Implementation
{
    public class BackToIdleAIMode : AIModeBase
    {
        private Coroutine coroutine;
        private IDamageable damageable;

        public BackToIdleAIMode(AIControllerBase aiController, BaseAIDataSO initializeData, BackToIdleAIModeFactory factoryData) : base(aiController, initializeData, factoryData)
        {
            coroutine = CorutineSystem.StartSequnce(BackToBaseAIModeSequence(factoryData.Duration));

            damageable = AIController.GetComponent<IDamageable>();
            damageable.OnExpireE += Damageable_OnExpire;
        }

        public override bool CanChangeMode(out IAIModeFactory modeFactory)
        {
            modeFactory = null;

            return true;
        }

        private IEnumerator BackToBaseAIModeSequence(float duration)
        {
            yield return new WaitForSeconds(2f);

            if (AIController == null) yield break;

            AIController.BackToBaseAIMode();

            coroutine = null;
        }

        private void Damageable_OnExpire(IDamageable damageable)
        {
            if (coroutine != null)
                CorutineSystem.StopSequnce(coroutine);

            damageable.OnExpireE -= Damageable_OnExpire;
        }

        protected override void InternalOnDestory()
        {
            base.InternalOnDestory();

            Damageable_OnExpire(damageable);
        }
    }
}
