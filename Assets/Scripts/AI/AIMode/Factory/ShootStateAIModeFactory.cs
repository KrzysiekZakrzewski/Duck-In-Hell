using BlueRacconGames.AI.Data;
using BlueRacconGames.AI.Implementation;

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
            return distanceToPlayer < ValidateDistance;
        }
    }
}
