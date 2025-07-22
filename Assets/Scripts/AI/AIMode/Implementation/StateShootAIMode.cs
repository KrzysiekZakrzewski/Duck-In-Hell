using BlueRacconGames.AI.Data;
using BlueRacconGames.AI.Factory;

namespace BlueRacconGames.AI.Implementation
{
    public class StateShootAIMode : StateAIModeBase
    {
        public StateShootAIMode(AIControllerBase aIController, BaseStateAIDataSO initializeData, IAIModeFactory factoryData) : base(aIController, initializeData, factoryData)
        {
        }
    }
}
