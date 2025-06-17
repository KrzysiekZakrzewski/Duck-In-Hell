using BlueRacconGames.AI.Data;
using BlueRacconGames.AI.Factory;
using RDG.Platforms;
using System.Collections;
using UnityEngine;

namespace BlueRacconGames.AI.Implementation
{
    public class BackToIdleAIMode : AIModeBase
    {
        public BackToIdleAIMode(AIControllerBase aiController, BaseAIDataSO initializeData, BackToIdleAIModeFactory factoryData) : base(aiController, initializeData, factoryData)
        {
            CorutineSystem.StartSequnce(BackToBaseAIModeSequence(factoryData.Duration));
        }

        public override bool CanChangeMode(out IAIModeFactory modeFactory)
        {
            modeFactory = null;

            return true;
        }

        private IEnumerator BackToBaseAIModeSequence(float duration)
        {
            yield return new WaitForSeconds(2f);

            AIController.BackToBaseAIMode();
        }
    }
}
