using BlueRacconGames.AI.Data;
using BlueRacconGames.AI.Factory;
using System;
using UnityEngine;

namespace BlueRacconGames.AI.Implementation
{
    public class WalkableAIModeBase : AIModeBase
    {
        private float reachDestinationDistance;
        private Vector2 destination;
        private float walkableAreaRadius;

        public WalkableAIModeBase(AIControllerBase aIController, BaseWalkableAIDataSO enemyAIDataSO, IAIModeFactory factoryData) : base(aIController, enemyAIDataSO, factoryData)
        {

        }

        public override bool CanChangeMode(out IAIModeFactory modeFactory)
        {
            throw new NotImplementedException();
        }

        public override void OnEndWonder()
        {
            throw new NotImplementedException();
        }

        public override void OnStartWonder()
        {
            throw new NotImplementedException();
        }
    }
}
