using BlueRacconGames.Animation;
using Game.CharacterController.Data;
using TimeTickSystems;
using UnityEngine;

namespace Game.CharacterController
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class CharacterController2D : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer presentationGameObject;

        private float walkSpeed;
        private float runSpeed;
        private float maxSpeed = 10f;
        private BaseSpriteState baseSpriteState = BaseSpriteState.Right;
        private Vector2 movement = Vector2.zero;
        private Rigidbody2D rb;
        private UnitAnimationControllerBase animationController;
        private bool facingRight = true;
        private Vector2 direction = Vector2.down;
        private readonly float dragMultiply = 3f;

        public bool CanMove { get; private set; } = true;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            animationController = GetComponent<UnitAnimationControllerBase>();

            rb.gravityScale = 0f;
        }

        public void SetData(CharacterControllerDataSO characterControllerDataSO)
        {
            walkSpeed = characterControllerDataSO.WalkSpeedBase;
            runSpeed = characterControllerDataSO.RunSpeedBase;
            maxSpeed = characterControllerDataSO.MaxSpeedBase;
            baseSpriteState = characterControllerDataSO.BaseSpriteState;

            rb.mass = characterControllerDataSO.UnitPhysicsData.Mass;
            rb.sharedMaterial = characterControllerDataSO.UnitPhysicsData.PhysicsMaterial;

            if (baseSpriteState == BaseSpriteState.Right) return;

            Flip(false);
        }
        public void Move(float horizontal, float vertical, bool run)
        {
            movement = new(horizontal, vertical);

            rb.linearDamping = movement != Vector2.zero ? 0 : Mathf.Lerp(rb.linearDamping, dragMultiply, Time.fixedDeltaTime * dragMultiply);

            float movementSpeed = run ? runSpeed : walkSpeed;

            Vector2 targetVelocity = movementSpeed * Time.fixedDeltaTime * movement;

            if(rb.linearVelocity.magnitude >= maxSpeed) return; 

            rb.AddRelativeForce(targetVelocity, ForceMode2D.Force);

            animationController.WalkableAnimation(rb.linearVelocity.x, rb.linearVelocity.y, rb.linearVelocity.magnitude);

            ControlDirection(rb.linearVelocity);

            if (rb.linearVelocity.x > 0 && !facingRight)
                Flip();
            else if (rb.linearVelocity.x < 0 && facingRight)
                Flip();
            
        }
        public void SetCanMove(bool canMove, bool forceStopForces)
        {
            CanMove = canMove;

            if (!forceStopForces) return;

            rb.linearVelocity = Vector2.zero;
        }
        public bool IsMoving => rb.linearVelocity != Vector2.zero;

        private void ControlDirection(Vector2 newDirection)
        {
            if (direction == newDirection || newDirection == Vector2.zero) return;

            direction = newDirection;

            animationController.IdleAnimation(newDirection.x, newDirection.y);
        }
        private void Flip(bool changeState = true)
        {
            if(changeState)
                facingRight = !facingRight;

            Vector3 theScale = presentationGameObject.transform.localScale;
            theScale.x *= -1;
            presentationGameObject.transform.localScale = theScale;
        }

        public enum BaseSpriteState
        {
            Right,
            Left
        }
    }
}