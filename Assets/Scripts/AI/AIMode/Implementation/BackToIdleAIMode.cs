using BlueRacconGames.AI.Data;
using BlueRacconGames.AI.Factory;

namespace BlueRacconGames.AI.Implementation
{
    public class BackToIdleAIMode : AIModeBase
    {
        public BackToIdleAIMode(AIControllerBase aiController, BaseAIDataSO initializeData, IAIModeFactory factoryData) : base(aiController, initializeData, factoryData)
        {
        }

        public override bool CanChangeMode(out IAIModeFactory modeFactory)
        {
            throw new System.NotImplementedException();
        }

        public override void OnEndWonder()
        {

        }

        public override void OnStartWonder()
        {

        }
    }
}
