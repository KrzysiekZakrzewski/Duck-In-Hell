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
        private bool isSimulated;

        protected float simulationDistance;
        protected Transform playerTransform;

        public AIControllerBase AIController => aIController;
        public bool IsSimulated => isSimulated;

        public AIModeBase(AIControllerBase aiController, BaseAIDataSO initializeData, IAIModeFactory factoryData)
        {
            this.aIController = aiController;
            playerTransform = aIController.PlayerTransform;

            simulationDistance = initializeData.SimulationDistance;

            TimeTickSystem.OnTick += OnTickSimulateChecker;
        }

        public void Update()
        {
            if(!isSimulated) return;

            InternalUpdate();
        }
        public void OnDestory()
        {
            StopSimulate();

            TimeTickSystem.OnTick -= OnTickSimulateChecker;

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
            if (isSimulated) return;

            isSimulated = true;
        }
        public virtual void StopSimulate()
        {
            if (!isSimulated) return;

            isSimulated = false;
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

        private void OnTickSimulateChecker(object sender, OnTickEventArgs e)
        {
            var inSimulateDistance = CalculateDistance(playerTransform.position) < simulationDistance;

            if(inSimulateDistance == isSimulated) return;

            if (inSimulateDistance)
            {
                StartSimulate();
                return;
            }

            StopSimulate();
        }
    }
}