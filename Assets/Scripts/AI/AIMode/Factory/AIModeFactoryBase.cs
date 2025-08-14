using BlueRacconGames.AI.Data;
using System;
using UnityEngine;

namespace BlueRacconGames.AI.Factory
{
    [Serializable]
    public abstract class AIModeFactoryBase : IAIModeFactory
    {
        [field: SerializeField] protected bool InfinityDistance { get; private set; }
        [field: SerializeField, HideIf(nameof(InfinityDistance), true)] protected float ValidateDistance { get; private set; }
        [field: SerializeReference, ReferencePicker] public IAIModuleFactory[] FactoryModules { get; private set; }

        public abstract IAIMode CreateAIMode(AIControllerBase aIController, BaseAIDataSO aIData);
        public abstract bool ChangeValidator(float distanceToPlayer);
    }
}
