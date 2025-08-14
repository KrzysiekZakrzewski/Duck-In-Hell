using BlueRacconGames.AI.Data;
using BlueRacconGames.AI.Implementation;
using System;
using UnityEngine;


namespace BlueRacconGames.AI.Factory
{
    public class StateAIModeFactoryBase : AIModeFactoryBase
    {
        [field: SerializeReference, ReferencePicker] public AIModeFactoryBase[] ChangeAIModeOptions { private set; get; }

        public override IAIMode CreateAIMode(AIControllerBase aIController, BaseAIDataSO aIData)
        {
            return new StateAIModeBase(aIController, aIData as BaseStateAIDataSO, this);
        }
        public override bool ChangeValidator(float distanceToPlayer)
        {
            float validateDistance = InfinityDistance ? Mathf.Infinity : ValidateDistance;

            return distanceToPlayer < validateDistance;
        }
    }
}
