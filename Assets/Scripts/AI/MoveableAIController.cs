using BlueRacconGames.AI.Data;
using Game.CharacterController;
using Pathfinding;
using Units.Implementation;
using UnityEngine;

namespace BlueRacconGames.AI
{
    [RequireComponent(typeof(CharacterController2D))]
    public class MoveableAIController : AIControllerBase
    {
        private float speed = 10f;
        private int currentWaypointId;
        private Seeker agent;
        private CharacterController2D characterController;
        private Rigidbody2D rb;
        private Path path;

        public bool IsStoped { get; private set; }


        public override void Initialize(BaseAIDataSO aIDataSO, PlayerUnit playerUnit)
        {
            base.Initialize(aIDataSO, playerUnit);

            rb = GetComponent<Rigidbody2D>();
            agent = GetComponent<Seeker>();
            characterController = GetComponent<CharacterController2D>();
        }
        public void UpdatePath(Vector3 target)
        {
            if (agent == null || !agent.IsDone()) return;

            agent.StartPath(rb.position, target, OnPathCompleted);
            currentWaypointId = 0;
        }
        public void StartNavMeshAgent()
        {
            IsStoped = false;
        }
        public void StopNavMeshAgent()
        {
            IsStoped = true;
        }

        protected void Move()
        {
            if (path == null) return;

            if (currentWaypointId >= path.vectorPath.Count)
                return;

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypointId] - rb.position).normalized;

            Vector2 force = speed * Time.deltaTime * direction;

            characterController.Move(force.x, force.y, false);

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypointId]);
            /*
            if (distance < aIMode.ReachDestinationDistance)
                currentWaypointId++;
            */
        }
        protected void SetupAgentAI()
        {
            //speed = aIMode.MoveSpeed;
        }

        private void OnPathCompleted(Path path)
        {
            if (path.error) return;

            this.path = path;
        }
        private void NaturalBreaking()
        {
            var currentPos = transform.position;

            Vector3 breakPos = currentPos + transform.forward;

            UpdatePath(breakPos);
        }
    }
}