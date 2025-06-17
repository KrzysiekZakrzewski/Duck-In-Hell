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

        private BaseAIDataSO aIDataSO;
        private WonderAIMode wonderAI;

        public Transform PlayerTransform { get; private set; }
        public bool IsWondering => wonderAI.IsWondering;

        protected virtual void Update()
        {
            if (aIMode == null || wonderAI.IsWondering) return;

            aIMode.Update();
        }
        protected virtual void OnDestroy()
        {
            wonderAI.OnStartWonderE -= OnStartWonder;
            wonderAI.OnEndWonderE -= OnEndWonder;

            if (aIMode == null) return;

            aIMode.OnDestory();
        }

        public virtual void Initialize(BaseAIDataSO aIDataSO)
        {
            this.aIDataSO = aIDataSO;

            PlayerTransform = FindAnyObjectByType<PlayerController>().transform;// TO DO Change this 

            wonderAI = new WonderAIMode();

            wonderAI.OnStartWonderE += OnStartWonder;
            wonderAI.OnEndWonderE += OnEndWonder;

            ForceChangeAIMode(aIDataSO.InitializeAIModeData);
        }
        public void TryChangeAIMode()
        {
            if (wonderAI.IsWondering) return;

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

        protected virtual void OnStartWonder()
        {
            aIMode.OnStartWonder();
        }
        protected virtual void OnEndWonder()
        {
            aIMode.OnEndWonder();

            if (!aIMode.CanChangeMode(out var factory)) return;

            ChangeState(factory);
        }

        private void ChangeState(IAIModeFactory modeFactory)
        {
            aIMode?.OnDestory();

            aIMode = modeFactory.CreateAIMode(this, aIDataSO);

            Debug.Log(aIMode);
        }
    }
}