using BlueRacconGames.AI.Data;

namespace BlueRacconGames.AI.Factory
{
    public interface IAIModeFactory
    {
        IAIMode CreateAIMode(AIControllerBase aIController, BaseAIDataSO aIData);
        bool ChangeValidator(float distanceToPlayer);
    }
}
