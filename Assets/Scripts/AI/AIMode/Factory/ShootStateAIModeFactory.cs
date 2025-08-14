using BlueRacconGames.AI.Data;
using BlueRacconGames.AI.Implementation;
using UnityEngine;

namespace BlueRacconGames.AI.Factory
{
    public class ShootStateAIModeFactory : StateAIModeFactoryBase
    {
        public override IAIMode CreateAIMode(AIControllerBase aIController, BaseAIDataSO aIData)
        {
            return new StateShootAIMode(aIController, aIData as ShootStateAIDataSO, this);
        }
        public override bool ChangeValidator(float distanceToPlayer)
        {
            float validateDistance = InfinityDistance ? Mathf.Infinity : ValidateDistance;

            return distanceToPlayer < validateDistance;
        }
    }
}