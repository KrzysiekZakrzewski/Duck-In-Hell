using UnityEngine;

public class ParticleDebuger : MonoBehaviour
{
    [SerializeField] private float radius;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
