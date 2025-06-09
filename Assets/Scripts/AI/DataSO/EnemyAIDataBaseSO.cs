using BlueRacconGames.AI.Factory;
using UnityEngine;

namespace BlueRacconGames.AI.Data
{
    public class EnemyAIDataBaseSO : ScriptableObject
    {
        [field: SerializeReference, ReferencePicker] public IAIModeFactory IdleAIModeOptions { private set; get; }
    }
}