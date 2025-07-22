using BlueRacconGames.AI.Data;
using BlueRacconGames.AI.Factory;
using System.Collections.Generic;
using UnityEngine;

namespace BlueRacconGames.AI.Implementation
{
    public abstract class AIModeBase : IAIMode
    {
        private readonly AIControllerBase aIController;

        protected HashSet<IAIModule> modules;
        protected Transform playerTransform;

        public AIControllerBase AIController => aIController;
        public HashSet<IAIModule> Modules => modules;

        public AIModeBase(AIControllerBase aiController, BaseAIDataSO initializeData, IAIModeFactory factoryData)
        {
            this.aIController = aiController;
            playerTransform = aIController.PlayerTransform;
            
            modules = new HashSet<IAIModule>();

            foreach(var module in factoryData.Modules)
            {
                module.Initialize(aiController);
                modules.Add(module);
            }
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