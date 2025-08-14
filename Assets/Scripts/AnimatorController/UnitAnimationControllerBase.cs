
using UnityEngine;

namespace BlueRacconGames.Animation
{
    public abstract class UnitAnimationControllerBase : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private bool hasAnimator;

        private int animIDHorizontal;
        private int animIDHorizontalDirection;
        private int animIDVertical;
        private int animIDVerticalDirection;
        private int animIDSpeed;

        private void Awake()
        {
            hasAnimator = animator != null;

            AssignAnimationIDs();
        }

        public void PlayAnimation(AnimationDataSO animationData)
        {
            if(!hasAnimator) return;

            if (animationData.UseCrossFade)
            {
                PlayCrossFadeAnimation(animationData);
                return;
            }

            animator.Play(animationData.Name, animationData.Layer);
        }
        public void WalkableAnimation(float horizontal, float vertical, float speed)
        {
            if(!hasAnimator) return;

            animator.SetFloat(animIDHorizontal, horizontal);
            animator.SetFloat(animIDVertical, vertical);
            animator.SetFloat(animIDSpeed, speed);
        }
        public void ForceStopWalkaalbleAnimation()
        {
            if (!hasAnimator) return;

            animator.SetFloat(animIDHorizontal, 0f);
            animator.SetFloat(animIDVertical, 0f);
            animator.SetFloat(animIDSpeed, 0f);
        }
        public void IdleAnimation(float horizontal, float vertical)
        {
            if(!hasAnimator) return;

            animator.SetFloat(animIDHorizontalDirection, horizontal);
            animator.SetFloat(animIDVerticalDirection, vertical);
        }
        public bool GetBoolParameter(string name)
        {
            return animator.GetBool(name);
        }
        public bool IsAnimationPlayingOnLayer(AnimationDataSO animationData)
        {
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(animationData.Layer);
            return stateInfo.IsName(animationData.Name) && stateInfo.normalizedTime < 1.0f;
        }
        public void SetupAnimatorController(RuntimeAnimatorController animatorController)
        {
            animator.runtimeAnimatorController = animatorController;
        }

        protected virtual void AssignAnimationIDs()
        {
            if (!hasAnimator) return;

            animIDSpeed = Animator.StringToHash("Speed");
            animIDHorizontal = Animator.StringToHash("Horizontal");
            animIDVertical = Animator.StringToHash("Vertical");
            animIDHorizontalDirection = Animator.StringToHash("HorizontalDirection");
            animIDVerticalDirection = Animator.StringToHash("VerticalDirection");
        }

        private void PlayCrossFadeAnimation(AnimationDataSO animationData)
        {
            if (!hasAnimator) return;

            animator.CrossFade(animationData.Name, animationData.TransitionDuration, animationData.Layer);
        }
    }
}