using UnityEngine;
using ViewSystem.Implementation;

namespace Engagement.UI
{
    public class EngagementUIController : SingleViewTypeStackController
    {
        [SerializeField] private EngagementView engagementView;
        [SerializeField] private IntroMovieView introMovieView;

        public void OpenFirstView()
        {
            TryOpenSafe<IntroMovieView>();
        }

        public EngagementView GetEngagementView()
        {
            return engagementView;
        }

        public void ShowEngagementView()
        {
            introMovieView.ParentStack.TryPushSafe(engagementView);
        }
    }
}