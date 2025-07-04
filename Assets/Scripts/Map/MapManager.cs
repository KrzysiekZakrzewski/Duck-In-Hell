using UnityEngine;

namespace Game.Map
{
    public class MapManager : MonoBehaviour
    {
        [SerializeField] private MapBounds bounds;

        private void Awake()
        {
            bounds.SetupMapBounds();
        }

        public MapData GetMapData()
        {
            return bounds.GetMapBoundsData();
        }
    }
}