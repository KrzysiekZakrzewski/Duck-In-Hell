using UnityEngine;

namespace Units.Implementation
{
    [CreateAssetMenu(fileName = nameof(UnitPhysicsDataSO), menuName = nameof(Units) + "/" + nameof(Units.Implementation) + "/" + nameof(UnitPhysicsDataSO))]
    public class UnitPhysicsDataSO : ScriptableObject
    {
        [field: SerializeField] public float Mass = 1f;
        [field: SerializeField] public PhysicsMaterial2D PhysicsMaterial;
    }
}