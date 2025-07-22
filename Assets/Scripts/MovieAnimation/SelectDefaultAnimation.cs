using System.Collections;
using UnityEngine;

namespace Game.Animation
{
    public class SelectDefaultAnimation : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private string animationKey;
        [SerializeField] private float minStartDelay;
        [SerializeField] private float maxStartDelay;

        private void Start()
        {
            StartCoroutine(WaitForStart());
        }

        private IEnumerator WaitForStart()
        {
            var delay = Random.Range(minStartDelay, maxStartDelay);

            yield return new WaitForSeconds(delay);

            animator.Play(animationKey);

            StartCoroutine(WaitForStart());
        }
    }
}