using UnityEngine;

namespace Game.Map
{
    public class MapObstacleBase : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Collider2D collider;

        private readonly float boundsOffSet = 0.1f;

        public Bounds GetObstacleBounds()
        {
            var size = collider.bounds.size + collider.bounds.size * boundsOffSet;

            return new Bounds(collider.bounds.center, size);
        }
    }
}