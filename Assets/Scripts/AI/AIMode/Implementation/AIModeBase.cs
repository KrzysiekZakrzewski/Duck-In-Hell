using BlueRacconGames.AI.Data;
using BlueRacconGames.AI.Factory;
using UnityEngine;

namespace BlueRacconGames.AI.Implementation
{
    public abstract class AIModeBase : IAIMode
    {
        private AIControllerBase aIController;
        private bool isSimulated;

        protected Transform playerTransform;

        public AIControllerBase AIController => aIController;
        public bool IsSimulated => isSimulated;

        public virtual void Initialize(AIControllerBase aIController, EnemyAIDataBaseSO enemyAIDataBaseSO)
        {
            this.aIController = aIController;
            playerTransform = aIController.PlayerTransform;
        }
        public virtual void Update()
        {

        }
        public virtual void OnDestory()
        {

        }
        public abstract bool CanChangeMode(out IAIModeFactory modeFactory);
        public abstract void OnStartWonder();
        public abstract void OnEndWonder();
        public virtual void StartSimulate()
        {
            if (isSimulated) return;

            isSimulated = true;
        }
        public virtual void StopSimulate()
        {
            if (!isSimulated) return;

            isSimulated = false;
        }
    }
}
