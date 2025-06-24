using BlueRacconGames.AI.Data;
using BlueRacconGames.AI.Factory;
using TimeTickSystems;
using Unity.VisualScripting;
using UnityEngine;

namespace BlueRacconGames.AI.Implementation
{
    public abstract class AIModeBase : IAIMode
    {
        private readonly AIControllerBase aIController;

        protected Transform playerTransform;

        public AIControllerBase AIController => aIController;

        public AIModeBase(AIControllerBase aiController, BaseAIDataSO initializeData, IAIModeFactory factoryData)
        {
            this.aIController = aiController;
            playerTransform = aIController.PlayerTransform;
        }

        public void Update()
        {
            if(!aIController.IsSimulating) return;

            InternalUpdate();
        }
        public void OnDestory()
        {
            InternalOnDestory();
        }
        public abstract bool CanChangeMode(out IAIModeFactory modeFactory);
        public virtual void OnStartWonder()
        {

        }
        public virtual void OnEndWonder()
        {

        }
        public virtual void StartSimulate()
        {

        }
        public virtual void StopSimulate()
        {

        }

        protected float CalculateDistance(Vector2 destination)
        {
            var distance = Vector2.Distance(AIController.transform.position, destination);
            /*
            Gizmos.DrawLine(AIController.transform.position, destination);
            Vector3 midpoint = (AIController.transform.position + new Vector3(destination.x, destination.y)) / 2f;
            UnityEditor.Handles.Label(midpoint, $"Distance: {distance:F2}");
            */
            return distance;
        }
        protected virtual void InternalUpdate()
        {

        }
        protected virtual void InternalOnDestory()
        {

        }
    }
}