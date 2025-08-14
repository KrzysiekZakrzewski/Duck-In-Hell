using BlueRacconGames.AI.Data;
using BlueRacconGames.AI.Implementation;
using System;
using UnityEngine;

namespace BlueRacconGames.AI.Factory
{
    [System.Serializable]
    public class InitializeAIModeFactory : IAIModeFactory
    {
        [field: SerializeReference, ReferencePicker] public AIModeFactoryBase[] ChangeAIModeOptions { private set; get; }
        [field: SerializeReference, ReferencePicker] public IAIModuleFactory[] FactoryModules { private set; get; }

        public IAIMode CreateAIMode(AIControllerBase aIController, BaseAIDataSO aIData)
        {
            return new InitializeAIMode(aIController, aIData, this);
        }
        public bool ChangeValidator(float distanceToPlayer)
        {
            return true;
        }
    }
}
