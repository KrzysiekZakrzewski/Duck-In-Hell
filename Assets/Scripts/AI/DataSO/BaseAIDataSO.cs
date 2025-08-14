using BlueRacconGames.AI.Factory;
using UnityEngine;

namespace BlueRacconGames.AI.Data
{
    public class BaseAIDataSO : ScriptableObject
    {
        [field: SerializeField] public InitializeAIModeFactory InitializeAIModeData { private set; get; }
        [field: SerializeField] public bool InfinitySimulate { private set; get; } = false;
        [field: SerializeField, HideIf(nameof(InfinitySimulate), true)] public float SimulationDistance { private set; get; } = 20f;
    }
}