using BlueRacconGames.AI.Data;
using BlueRacconGames.AI.Implementation;
using UnityEngine;

namespace BlueRacconGames.AI.Factory
{
    public class BackToIdleAIModeFactory : AIModeFactoryBase
    {
        [field: SerializeField] public float Duration { get; private set; } = 1f; 
        public override IAIMode CreateAIMode(AIControllerBase aIController, BaseAIDataSO aIData)
        {
            return new BackToIdleAIMode(aIController, aIData, this);
        }
        public override bool ChangeValidator(float distanceToPlayer)
        {
            return distanceToPlayer > ValidateDistance;
        }
    }
}
