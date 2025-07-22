using System.Collections.Generic;
using UnityEngine;

namespace Game.Map
{
    public class MapDecorationRandomPlacer : MonoBehaviour
    {
        [SerializeField] private GameObject[] mapDecorationObjects;
        [SerializeField] private int minDecorationAmount;
        [SerializeField] private int maxDecorationAmount;
        [SerializeField] private Transform boundCenterPos;
        [SerializeField] private Vector2 boundSize;

        private List<GameObject> spawnedMapDecorationsLUT = new();

        [ContextMenu("Randomize details")]
        private void RandomizeObjects()
        {
            if (spawnedMapDecorationsLUT.Count > 0)
                Clear();

            int amount = Random.Range(minDecorationAmount, maxDecorationAmount);

            for (int i = 0; i < amount; i++)
            {
                int randomItemId = Random.Range(0, mapDecorationObjects.Length);

                var position = GetRandomPositionInBounds();

                var gm = Instantiate(mapDecorationObjects[randomItemId], position, Quaternion.identity, transform);

                spawnedMapDecorationsLUT.Add(gm);
            }
        }
        private Vector2 GetRandomPositionInBounds()
        {
            var x = Random.Range(-boundSize.x / 2, boundSize.x / 2);
            var y = Random.Range(-boundSize.y / 2, boundSize.y / 2);

            return new Vector2(x, y);
        }
        [ContextMenu("Clear")]
        private void Clear()
        {
            for(int i = 0; i < spawnedMapDecorationsLUT.Count; i++)
                DestroyImmediate(spawnedMapDecorationsLUT[i]);

            spawnedMapDecorationsLUT.Clear();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireCube(boundCenterPos.position, boundSize);
        }
    }
}