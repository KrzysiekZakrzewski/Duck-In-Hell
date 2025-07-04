using UnityEngine;

namespace Game.Map
{
    public class MapDecorationObject : MonoBehaviour
    {
        [SerializeField] private int decorationId = -1;
        [SerializeField] private Animator animator;

        private void Awake()
        {
            SetupDecorationObject();
        }
        [ContextMenu("SetupDecorationObject")]
        private void SetupDecorationObject()
        {
            int animationCount = animator.runtimeAnimatorController.animationClips.Length;

            int decorationId = this.decorationId == -1 ? Random.Range(0, animationCount) : this.decorationId;

            var clip = animator.runtimeAnimatorController.animationClips[decorationId];

            animator.Play(clip.name);
        }
    }
}