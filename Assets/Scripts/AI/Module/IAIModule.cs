namespace BlueRacconGames.AI
{
    public interface IAIModule
    {
        void Initialize(AIControllerBase aIController);
        void DeInitialize(AIControllerBase aIController);
    }
}