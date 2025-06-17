using BlueRacconGames.AI.Data;
using BlueRacconGames.AI.Factory;

namespace BlueRacconGames.AI
{
    public interface IAIMode
    {
        AIControllerBase AIController {  get; }
        bool IsSimulated {  get; }

        void Update();
        void OnDestory();
        bool CanChangeMode(out IAIModeFactory modeFactory);
        void OnStartWonder();
        void OnEndWonder();
        void StartSimulate();
        void StopSimulate();
    }
}