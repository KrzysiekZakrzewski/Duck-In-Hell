using UnityEngine;

namespace Game.GameCursor
{
    public class CursorManager : MonoBehaviour
    {
        private void Awake()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }
    }
}