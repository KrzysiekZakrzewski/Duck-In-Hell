using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Map
{
    public class MapBounds : MonoBehaviour
    {
        [SerializeField] private PhysicsMaterial2D boundsMaterial;
        [SerializeField] private Grid tilesGrid;
        [SerializeField] private TilemapCollider2D boundsCollider;

        public void SetupMapBounds()
        {
            boundsCollider.sharedMaterial = boundsMaterial;
        }

        public Vector2 GetMapBounds()
        {
            Vector2 bounds = boundsCollider.bounds.extents - tilesGrid.cellSize;

            bounds = new Vector2(Mathf.RoundToInt(bounds.x), Mathf.RoundToInt(bounds.y));

            return bounds;
        }
    }
}