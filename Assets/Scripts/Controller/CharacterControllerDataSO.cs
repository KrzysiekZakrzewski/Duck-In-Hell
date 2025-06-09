using UnityEngine;

namespace Game.CharacterController.Data
{
    [CreateAssetMenu(fileName = nameof(CharacterControllerDataSO), menuName = nameof(Game) + "/" + nameof(CharacterController.Data) + "/" + nameof(CharacterControllerDataSO))]

    public class CharacterControllerDataSO : ScriptableObject
    {
        [field: SerializeField] public int WalkSpeedBase { get; protected set; }
        [field: SerializeField] public int RunSpeedBase { get; protected set; }
        [field: SerializeField] public int MaxSpeedBase { get; protected set; }
        [field: SerializeField] public CharacterController2D.BaseSpriteState BaseSpriteState { get; protected set; }
    }
}