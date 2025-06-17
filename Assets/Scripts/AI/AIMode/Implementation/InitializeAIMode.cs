using BlueRacconGames.AI.Data;
using BlueRacconGames.AI.Factory;
using UnityEngine;

namespace BlueRacconGames.AI.Implementation
{
    public class InitializeAIMode : AIModeBase
    {
        protected readonly AIModeFactoryBase[] changeAIModeOptions;

        public InitializeAIMode(AIControllerBase aiController, BaseAIDataSO initializeData, InitializeAIModeFactory factoryData) : base(aiController, initializeData, factoryData)
        {
            changeAIModeOptions = factoryData.ChangeAIModeOptions;
        }

        public override bool CanChangeMode(out IAIModeFactory modeFactory)
        {
            return TryChangeAIMode(out modeFactory);
        }

        protected override void InternalUpdate()
        {
            base.InternalUpdate();

            if (!TryChangeAIMode(out var modeFactory)) return;

            AIController.TryChangeAIMode();
        }
        protected bool TryChangeAIMode(out IAIModeFactory modeFactory)
        {
            bool result = false;
            modeFactory = null;

            var distanceToPlayer = CalculateDistance(playerTransform.position);

            for (int i = 0; i < changeAIModeOptions.Length; i++)
            {
                modeFactory = changeAIModeOptions[i];

                result = modeFactory.ChangeValidator(distanceToPlayer);

                if (result) break;
            }

            return result;
        }
    }
}
