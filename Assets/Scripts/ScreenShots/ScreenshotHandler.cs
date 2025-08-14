using System.IO;
using UnityEditor;
using UnityEngine;

namespace Game.Marketing
{
    public class ScreenshotHandler : MonoBehaviour
    {
        private static int screenshotCount = 1;
#if UNITY_EDITOR
        [ContextMenu("TakeScreenshot")]
        public void TakeScreenshot()
        {
            int width = Screen.width;
            int height = Screen.height;

            Texture2D screenshot = new(width, height, TextureFormat.RGBA32, false);

            SceneView sceneView = SceneView.lastActiveSceneView;

            if (sceneView == null)
            {
                Debug.LogError("Brak aktywnego widoku sceny.");
                return;
            }

            Camera cam = sceneView.camera;
            RenderTexture rt = new RenderTexture(width, height, 24);
            cam.targetTexture = rt;
            cam.Render();

            RenderTexture.active = rt;
            screenshot.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            screenshot.Apply();

            cam.targetTexture = null;
            RenderTexture.active = null;
            DestroyImmediate(rt);

            string directory = "Screenshots";
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            string path = Path.Combine(directory, $"ScreenShoot{screenshotCount}.png");
            screenshotCount++;
            File.WriteAllBytes(path, screenshot.EncodeToPNG());

            Debug.Log($"Screenshot zapisany: {path}");
            AssetDatabase.Refresh();
        }
        [ContextMenu("CaptureScreenshot")]
        public void CaptureScreenshot()
        {
            ScreenCapture.CaptureScreenshot($"Screenshot{screenshotCount}.png");
            screenshotCount++;
        }
#endif
    }
}