using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace RDG.Platforms
{
    public class CorutineSystem : MonoBehaviour
    {
        private static GameObject corutineSystemGameObject;
        private static CorutineSystemObject corutineSystem;
        public static void Create()
        {
            if (corutineSystemGameObject != null)
                return;

            corutineSystemGameObject = new GameObject("CorutineSystemObject");
            corutineSystem = corutineSystemGameObject.AddComponent<CorutineSystemObject>();
        }

        public static Coroutine StartSequnce(IEnumerator enumerator)
        {
            if (corutineSystemGameObject == null)
                Create();

            return corutineSystem.StartCoroutine(enumerator);
        }
        public static void StopSequnce(IEnumerator enumerator)
        {
            if (corutineSystemGameObject == null) return;

            corutineSystem.StopCoroutine(enumerator);
        }
        public static void StopSequnce(Coroutine coroutine)
        {
            if (corutineSystemGameObject == null) return;

            corutineSystem.StopCoroutine(coroutine);
        }
        public static void ForceStopAllCorutines()
        {
            corutineSystem.StopAllCoroutines();
        }

        private class CorutineSystemObject : MonoBehaviour
        {
            
        }
    }
}