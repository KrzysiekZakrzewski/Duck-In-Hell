using BlueRacconGames.AI.Data;
using BlueRacconGames.AI.Factory;
using System.Collections.Generic;

namespace BlueRacconGames.AI
{
    public interface IAIMode
    {
        AIControllerBase AIController {  get; }
        HashSet<IAIModule> Modules { get; }

        void Update();
        void OnDestory();
        bool CanChangeMode(out IAIModeFactory modeFactory);
        void OnStartWonder();
        void OnEndWonder();
        void StartSimulate();
        void StopSimulate();
    }
}