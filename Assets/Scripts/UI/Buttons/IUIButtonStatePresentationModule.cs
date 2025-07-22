using UnityEngine.UI;

namespace BlueRacconGames.UI
{
    public interface IUIButtonStatePresentationModule 
    {
        void UpdateIcon(Image icon);
        void UpdateState();
        void UpdateState(bool state);
        void UpdateStateAndIcon(Image icon);
        void UpdateStateAndIcon(Image icon, bool state);
    }
}