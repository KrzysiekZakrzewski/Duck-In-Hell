using UnityEngine;

namespace Game.Map
{
    public class MapManager : MonoBehaviour
    {
        [SerializeField] private MapBounds bounds;

        public Vector2 GetMapBounds()
        {
            return bounds.GetMapBounds();
        }
    }
}