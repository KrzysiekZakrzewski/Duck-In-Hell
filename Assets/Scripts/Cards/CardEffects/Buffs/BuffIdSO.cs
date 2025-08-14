using UnityEngine;

namespace BlueRacconGames.Cards.Effects.Data
{
    [CreateAssetMenu(fileName = nameof(BuffIdSO), menuName = nameof(Effects) + "/" + nameof(Data) + "/" + nameof(BuffIdSO))]
    public class BuffIdSO : ScriptableObject
    {
        [field: HideInInspector] public string UniqueId {  get; private set; }

        public void SetUniqueId()
        {
            if (string.IsNullOrEmpty(UniqueId))
            {
                UniqueId = System.Guid.NewGuid().ToString();
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(this);
                UnityEditor.AssetDatabase.SaveAssets();
#endif
            }
        }
        public bool Compare(string uniqueId) => UniqueId.Equals(uniqueId);
    }
}