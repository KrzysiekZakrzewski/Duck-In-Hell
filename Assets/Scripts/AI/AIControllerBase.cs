using BlueRacconGames.AI.Data;
using BlueRacconGames.AI.Factory;
using BlueRacconGames.AI.Implementation;
using UnityEngine;
using Game.CharacterController;
using TimeTickSystems;
using Units.Implementation;

namespace BlueRacconGames.AI
{
    [RequireComponent(typeof(CharacterController2D))]
    public abstract class AIControllerBase : MonoBehaviour
    {
        protected bool isSimulating;
        protected bool isExpired;

        private BaseAIDataSO aIDataSO;
        private WonderAIMode wonderAI;
        private bool isForcedSimulateStoped;
        private float simulationDistance;

        public IAIMode AIMode { protected set; get; }
        public Transform PlayerTransform { get; private set; }
        public bool IsWondering => wonderAI != null && wonderAI.IsWondering;
        public bool IsSimulating => isSimulating;

        protected virtual void Update()
        {
            if (AIMode == null || wonderAI.IsWondering) return;

            Debug.Log(AIMode + " " + gameObject.name);

            AIMode.Update();
        }
        protected virtual void OnDestroy()
        {
            if (wonderAI != null) 
            {
                wonderAI.OnStartWonderE -= OnStartWonder;
                wonderAI.OnEndWonderE -= OnEndWonder;
            }

            if (AIMode == null) return;

            AIMode?.OnDestory();
        }

        public virtual void Initialize(BaseAIDataSO initializeData, PlayerUnit playerUnit)
        {
            this.aIDataSO = initializeData;
            simulationDistance = initializeData.InfinitySimulate ? Mathf.Infinity : initializeData.SimulationDistance;
            isForcedSimulateStoped = true;

            PlayerTransform = playerUnit.transform;

            wonderAI = new WonderAIMode();

            TimeTickSystem.OnTick += OnTickSimulateChecker;

            wonderAI.OnStartWonderE += OnStartWonder;
            wonderAI.OnEndWonderE += OnEndWonder;

            ForceChangeAIMode(initializeData.InitializeAIModeData);
        }
        public virtual void UnInitialize()
        {
            ForceStartStopSimulate(false, true);

            TimeTickSystem.OnTick -= OnTickSimulateChecker;

            aIDataSO = null;

            if(wonderAI != null) return;

            wonderAI.ForceStop();

            wonderAI.OnStartWonderE -= OnStartWonder;
            wonderAI.OnEndWonderE -= OnEndWonder;

            wonderAI = null;
        }
        public void OnExpire()
        {
            if(isExpired) return;

            isExpired = true;

            OnExpireInternal();
        }
        public void TryChangeAIMode()
        {
            if (wonderAI == null || wonderAI.IsWondering) return;

            wonderAI.InitializeWonder();
        }
        public void ForceChangeAIMode(IAIModeFactory modeFactory)
        {
            ChangeState(modeFactory);
        }
        public void BackToBaseAIMode()
        {
            ChangeState(aIDataSO.InitializeAIModeData);
        }
        public void ForceStartStopSimulate(bool value, bool ignorePrevStatus = false)
        {
            if(isForcedSimulateStoped == !value && !ignorePrevStatus) return;

            isForcedSimulateStoped = !value;

            if (value)
                OnStartSimulate();
            else
                OnStopSimulate();
        }

        protected void OnStartSimulate()
        {
            isSimulating = true;

            AIMode?.StartSimulate();
        }
        protected void OnStopSimulate()
        {
            isSimulating = false;

            AIMode?.StopSimulate();
        }
        protected virtual void OnStartWonder()
        {
            AIMode.OnStartWonder();
        }
        protected virtual void OnEndWonder()
        {
            AIMode.OnEndWonder();

            if (!AIMode.CanChangeMode(out var factory)) return;

            ChangeState(factory);
        }
        protected virtual void OnExpireInternal()
        {
            UnInitialize();

            AIMode?.OnDestory();
        }
        protected float CalculateDistance(Vector2 destination)
        {
            var distance = Vector2.Distance(transform.position, destination);
            /*
            Gizmos.DrawLine(AIController.transform.position, destination);
            Vector3 midpoint = (AIController.transform.position + new Vector3(destination.x, destination.y)) / 2f;
            UnityEditor.Handles.Label(midpoint, $"Distance: {distance:F2}");
            */
            return distance;
        }

        private void ChangeState(IAIModeFactory modeFactory)
        {
            AIMode?.OnDestory();

            AIMode = modeFactory.CreateAIMode(this, aIDataSO);
        }
        private void OnTickSimulateChecker(object sender, OnTickEventArgs e)
        {
            if(isForcedSimulateStoped) return;

            var inSimulateDistance = CalculateDistance(PlayerTransform.position) < simulationDistance;

            if (inSimulateDistance == isSimulating) return;

            if (inSimulateDistance)
            {
                OnStartSimulate();
                return;
            }

            OnStopSimulate();
        }
    }
}