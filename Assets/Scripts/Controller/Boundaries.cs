using UnityEngine;

namespace BlueRacconGames.Extras
{
    public class Boundaries : MonoBehaviour
    {
        [SerializeField] private PhysicsMaterial2D boundPhysicsMaterial;

        private BoxCollider2D[] boundColliders = new BoxCollider2D[4];

        private void Awake()
        {
            SetupBoundsColliders();
        }

        private void SetupBoundsColliders()
        {
            var screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

            float screenWorldWidth = Vector2.Distance(new Vector2(-screenBounds.x, screenBounds.y), screenBounds);
            float screenWorldHeight = Vector2.Distance(new Vector2(screenBounds.x, -screenBounds.y), screenBounds);

            var rightBoundsGameObject = new GameObject("rightBounds");
            rightBoundsGameObject.transform.parent = transform;
            rightBoundsGameObject.transform.position = new Vector3(screenBounds.x, 0);
            var rightBoundsCollider = rightBoundsGameObject.AddComponent<BoxCollider2D>();
            rightBoundsCollider.size = new Vector3(0.1f, screenWorldHeight);
            boundColliders[0] = rightBoundsCollider;

            var leftBoundsGameObject = new GameObject("leftBounds");
            leftBoundsGameObject.transform.parent = transform;
            leftBoundsGameObject.transform.position = new Vector3(-screenBounds.x, 0);
            var leftBoundsCollider = leftBoundsGameObject.AddComponent<BoxCollider2D>();
            leftBoundsCollider.size = new Vector3(0.1f, screenWorldHeight);
            boundColliders[1] = leftBoundsCollider;

            var topBoundsGameObject = new GameObject("topBounds");
            topBoundsGameObject.transform.parent = transform;
            topBoundsGameObject.transform.position = new Vector3(0, screenBounds.y);
            var topBoundsCollider = topBoundsGameObject.AddComponent<BoxCollider2D>();
            topBoundsCollider.size = new Vector3(screenWorldWidth, 0.1f);
            boundColliders[2] = topBoundsCollider;

            var bottomBoundsGameObject = new GameObject("bottomBounds");
            bottomBoundsGameObject.transform.parent = transform;
            bottomBoundsGameObject.transform.position = new Vector3(0, -screenBounds.y);
            var bottomBoundsCollider = bottomBoundsGameObject.AddComponent<BoxCollider2D>();
            bottomBoundsCollider.size = new Vector3(screenWorldWidth, 0.1f);
            boundColliders[3] = bottomBoundsCollider;

            foreach(BoxCollider2D boxCollider in boundColliders)
            {
                boxCollider.sharedMaterial = boundPhysicsMaterial;
            }
        }
    }
}