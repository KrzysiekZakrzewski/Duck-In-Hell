using BlueRacconGames.AI.Data;
using BlueRacconGames.AI.Factory;
using BlueRacconGames.AI.Implementation;
using UnityEngine;
using Game.CharacterController;

namespace BlueRacconGames.AI
{
    [RequireComponent(typeof(CharacterController2D))]
    public abstract class AIControllerBase : MonoBehaviour
    {
        protected IAIMode aIMode;

        private EnemyAIDataBaseSO aIDataSO;
        private WonderAIMode wonderAI;

        public Transform PlayerTransform { get; private set; }

        protected virtual void Update()
        {
            if (aIMode == null || wonderAI.isWondering) return;

            aIMode.Update();
        }
        protected virtual void OnDestroy()
        {
            if (aIMode == null) return;

            aIMode.OnDestory();
        }

        public void TryChangeAIMode()
        {
            if (wonderAI.isWondering) return;

            wonderAI.SetupTimeTickSystem(OnStartWonder, OnEndWonder);
        }
        public void ForceChangeAIMode(IAIModeFactory modeFactory)
        {
            ChangeState(modeFactory);
        }

        public virtual void Initialize(EnemyAIDataBaseSO aIDataSO)
        {
            this.aIDataSO = aIDataSO;

            PlayerTransform = FindAnyObjectByType<PlayerController>().transform;// TO DO Change this 

            wonderAI = new WonderAIMode();

            aIMode = aIDataSO.IdleAIModeOptions.CreateAIMode(this, aIDataSO);
        }

        protected virtual void OnStartWonder()
        {
            aIMode.OnStartWonder();
        }
        protected virtual void OnEndWonder()
        {
            if (!aIMode.CanChangeMode(out var factory)) return;

            ChangeState(factory);
        }

        private void ChangeState(IAIModeFactory modeFactory)
        {
            aIMode = modeFactory.CreateAIMode(this, aIDataSO);
        }
    }
}