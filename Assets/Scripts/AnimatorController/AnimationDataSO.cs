using UnityEngine;

namespace BlueRacconGames.Animation
{
    [CreateAssetMenu(fileName = nameof(AnimationDataSO), menuName = nameof(BlueRacconGames) + "/" + nameof(Animation) + "/" + nameof(AnimationDataSO))]
    public class AnimationDataSO : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int Layer { get; private set; }
        [field: SerializeField] public bool UseCrossFade { get; private set; }
        [field: SerializeField, ShowIf(nameof(UseCrossFade), true)] public float TransitionDuration { get; private set; }
    }
}