using BlueRacconGames.AI.Data;
using BlueRacconGames.AI.Implementation;

namespace BlueRacconGames.AI.Factory
{
    public class BackToIdleAIModeFactory : AIModeFactoryBase
    {
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
