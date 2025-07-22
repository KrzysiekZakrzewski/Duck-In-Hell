using UnityEngine;

namespace Saves.Object
{
    [CreateAssetMenu(fileName = nameof(HighScoreSaveObject), menuName = nameof(Saves) + "/" + "Assets/Objects" + "/" + nameof(HighScoreSaveObject))]
    public class HighScoreSaveObject : SaveObject
    {
        public SaveValue<int> HighScore = new(SaveKeyUtilities.HighScore);
    }
}
