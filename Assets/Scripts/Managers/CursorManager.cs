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

        public static void UpdateCursorVisable(bool value)
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            Cursor.visible = value;
#endif
        }
    }
}