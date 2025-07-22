using BlueRacconGames.AI.Data;
using System;
using UnityEngine;

namespace BlueRacconGames.AI.Factory
{
    [Serializable]
    public abstract class AIModeFactoryBase : IAIModeFactory
    {
        [field: SerializeField] protected float ValidateDistance { get; private set; }
        [field: SerializeReference, ReferencePicker] public IAIModule[] Modules { get; private set; }

        public abstract IAIMode CreateAIMode(AIControllerBase aIController, BaseAIDataSO aIData);
        public abstract bool ChangeValidator(float distanceToPlayer);
    }
}
