using BlueRacconGames.AI.Data;
using BlueRacconGames.AI.Implementation;
using System;
using UnityEngine;

namespace BlueRacconGames.AI.Factory
{
    public class WalkableAIModeFactoryBase : AIModeFactoryBase
    {
        [field: SerializeReference, ReferencePicker] public AIModeFactoryBase[] ChangeAIModeOptions { private set; get; }

        public override IAIMode CreateAIMode(AIControllerBase aIController, BaseAIDataSO aIData)
        {
            return new WalkableAIModeBase(aIController, aIData as BaseWalkableAIDataSO, this);
        }
        public override bool ChangeValidator(float distanceToPlayer)
        {
            float validateDistance = InfinityDistance ? Mathf.Infinity : ValidateDistance;

            return true;
        }
    }
}
