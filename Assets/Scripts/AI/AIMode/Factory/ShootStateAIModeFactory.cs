using BlueRacconGames.AI.Data;
using BlueRacconGames.AI.Implementation;
using UnityEngine;

namespace BlueRacconGames.AI.Factory
{
    public class ShootStateAIModeFactory : StateAIModeFactoryBase
    {
        [field: SerializeField] public int ShootTickCountdown { get; private set; }
        public override IAIMode CreateAIMode(AIControllerBase aIController, BaseAIDataSO aIData)
        {
            return new StateShootAIMode(aIController, aIData as ShootStateAIDataSO, this);
        }
        public override bool ChangeValidator(float distanceToPlayer)
        {
            return distanceToPlayer < ValidateDistance;
        }
    }
}
