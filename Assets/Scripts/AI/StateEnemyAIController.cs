using BlueRacconGames.AI.Data;
using UnityEngine;

namespace BlueRacconGames.AI
{
    public class StateEnemyAIController : AIControllerBase
    {
        [SerializeField] private BaseStateAIDataSO testEnemySO;

        private void Awake()
        {
            Initialize(testEnemySO);
        }
    }
}