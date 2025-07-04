using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Game.Map
{
    public class MapBounds : MonoBehaviour
    {
        [SerializeField] private PhysicsMaterial2D boundsMaterial;
        [SerializeField] private Grid tilesGrid;
        [SerializeField] private TilemapCollider2D boundsCollider;
        [SerializeField] private Transform obstacleContainer;

        private List<MapObstacleBase> obstacles;

        public void SetupMapBounds()
        {
            boundsCollider.sharedMaterial = boundsMaterial;

            obstacles = new();

            obstacles = transform.GetComponentsInChildren<MapObstacleBase>().ToList();
        }
        public Vector2 GetMapBounds()
        {
            Vector2 bounds = boundsCollider.bounds.extents - tilesGrid.cellSize;

            bounds = new Vector2(Mathf.RoundToInt(bounds.x), Mathf.RoundToInt(bounds.y));

            return bounds;
        }
        public MapData GetMapBoundsData()
        {
            List<Bounds> blockedSpawnPoositions = new();

            foreach (var obstacle in obstacles)
                blockedSpawnPoositions.Add(obstacle.GetObstacleBounds());

            MapData mapData = new()
            {
                Bounds = GetMapBounds(),
                CellSize = tilesGrid.cellSize,
                BlockedSpawnPoosition = blockedSpawnPoositions.ToArray()
            };

            return mapData;
        }
    }

    public struct MapData
    {
        public Vector2 Bounds;
        public Vector2 CellSize;
        public Bounds[] BlockedSpawnPoosition;
    }
}