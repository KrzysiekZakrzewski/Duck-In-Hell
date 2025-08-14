using Game.HUD;
using UnityEngine;

namespace Game.Map
{
    public class MapDecorationObject : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private int decorationId = -1;
        [SerializeField] private Animator animator;

        private Collider2D collider;

        private void Awake()
        {
            SetupDecorationObject();

            DetectCollider();
        }
        private void SetupDecorationObject()
        {
            if (animator == null) return;

            int animationCount = animator.runtimeAnimatorController.animationClips.Length;

            int decorationId = this.decorationId == -1 ? Random.Range(0, animationCount) : this.decorationId;

            var clip = animator.runtimeAnimatorController.animationClips[decorationId];

            animator.Play(clip.name);
        }
        private void DetectCollider()
        {
            if(!TryGetComponent<Collider2D>(out collider) || spriteRenderer == null) return;

            MatchColliderToSprite();
        }
        private void MatchColliderToSprite()
        {
            float spriteWidth = spriteRenderer.sprite.bounds.size.x;
            float spriteHeight = spriteRenderer.sprite.bounds.size.y;

            switch (collider)
            {
                case CircleCollider2D:
                    float diameter = Mathf.Min(spriteWidth, spriteHeight);
                    float radius = diameter / 2f;

                    CircleCollider2D circleCollider2D = collider as CircleCollider2D;
                    circleCollider2D.radius = radius;
                    break;
                case BoxCollider2D:
                    BoxCollider2D boxCollider2D = collider as BoxCollider2D;
                    boxCollider2D.size = new Vector2(spriteWidth, spriteHeight);
                    break;
            }
        }
    }
}