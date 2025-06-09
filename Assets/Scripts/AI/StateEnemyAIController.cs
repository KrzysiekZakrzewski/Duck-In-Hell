using BlueRacconGames.AI.Data;
using UnityEngine;

namespace BlueRacconGames.AI
{
    public class StateEnemyAIController : AIControllerBase
    {
        [SerializeField] private IdleEnemyAIDataSO testEnemySO;

        private void Awake()
        {
            Initialize(testEnemySO);
        }
    }
}