using BlueRacconGames.AI.Data;
using System;
using UnityEngine;

namespace BlueRacconGames.AI.Factory
{
    [Serializable]
    public abstract class AIModeFactoryBase : IAIModeFactory
    {
        [field: SerializeField] protected float ValidateDistance { get; private set; }

        public abstract IAIMode CreateAIMode(AIControllerBase aIController, BaseAIDataSO aIData);
        public abstract bool ChangeValidator(float distanceToPlayer);
    }
}
