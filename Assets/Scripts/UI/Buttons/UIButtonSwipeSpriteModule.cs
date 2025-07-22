using UnityEngine.UI;
using UnityEngine;

namespace BlueRacconGames.UI
{
    public class UIButtonSwipeSpriteModule : IUIButtonStatePresentationModule
    {
        [SerializeField] private Sprite enableImage;
        [SerializeField] private Sprite disableImage;

        private bool state;

        public void UpdateIcon(Image icon)
        {
            icon.sprite = state ? enableImage : disableImage;
        }
        public void UpdateState() => state = !state;
        public void UpdateState(bool state)
        {
            this.state = state;
        }
        public void UpdateStateAndIcon(Image icon)
        {
            UpdateState();

            UpdateIcon(icon);
        }
        public void UpdateStateAndIcon(Image icon, bool state)
        {
            UpdateState(state);

            UpdateIcon(icon);
        }
    }
}
