using BlueRacconGames.AI.Data;
using BlueRacconGames.AI.Factory;
using Saves;
using UnityEngine;

namespace BlueRacconGames.AI.Implementation
{
    public class StateAIModeBase : AIModeBase
    {
        protected IAIModeFactory[] changeAIModeOptions;

        public StateAIModeBase(AIControllerBase aIController, BaseStateAIDataSO initializeData, IAIModeFactory factoryData) : base(aIController, initializeData, factoryData)
        {
            var stateFactoryData = factoryData as StateAIModeFactoryBase;

            changeAIModeOptions = stateFactoryData.ChangeAIModeOptions;
        }

        public override bool CanChangeMode(out IAIModeFactory modeFactory)
        {
            return TryChangeAIMode(out modeFactory);
        }

        protected override void InternalUpdate()
        {
            base.InternalUpdate();

            Debug.Log("AIMode: " + AIController.gameObject.name);

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
