using Audio.Manager;
using Engagement.UI;
using Game.Managers;
using Game.SceneLoader;
using Settings;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using Zenject;

namespace Engagement
{
    public class EngagementController : MonoBehaviour
    {
        public event Action OnEngagementStart;
        public event Action OnEngagement;
        public event Action OnEngagementCompleted;

        [SerializeField] private EngagementUIController bootUIController;
        [SerializeField] private SceneDataSo sceneToLoadData;
        [SerializeField] private float delayTime;

        private SceneLoadManagers sceneLoadManagers;
        private SettingsManager settingsManager;
        private ScoreSaveVersionManager scoreSaveVersionController;
        private AudioManager audioManager;
        private bool isInitialized;

        #region VideoPrivateVerbs
        private VideoPlayer video;
        private bool videoEnded;
        private float delayOffset = 0.1f;
        #endregion

        [Inject]
        private void Inject(SceneLoadManagers sceneLoadManagers,
             SettingsManager settingsManager, AudioManager audioManager)
        {
            this.sceneLoadManagers = sceneLoadManagers;
            this.settingsManager = settingsManager;
            this.audioManager = audioManager;
        }

        private void Awake()
        {
            isInitialized = false;

            video = GetComponent<VideoPlayer>();

            video.loopPointReached += EndVideoAction;
        }

        private void Start()
        {
            StartCoroutine(StartEngagement());
        }

        private void EndVideoAction(VideoPlayer vp)
        {
            videoEnded = true;
            bootUIController.TryOpenSafe<EngagementView>();
        }

        private IEnumerator PlayVideoWithDelay()
        {
            yield return new WaitForSeconds(delayTime + delayOffset);

#if UNITY_WEBGL
            EndVideoAction(null);
            yield break;
#else
            video.Play();
#endif
            yield return null;

            bootUIController.OpenFirstView();
        }

        private IEnumerator StartEngagement()
        {
            OnEngagementStart?.Invoke();

            yield return null;

            StartCoroutine(PlayVideoWithDelay());

            OnEngagement?.Invoke();

            settingsManager.LoadSettings();

            yield return new WaitUntil(CheckEngagemntWasFinished);

            audioManager.PlayRandomMusic();

            var engagement = bootUIController.GetEngagementView();

            OnEngagementCompleted?.Invoke();

            engagement.ShowContinueText();

            isInitialized = true;
        }

        private bool CheckEngagemntWasFinished()
        {
            return settingsManager.InitializeFinished; //videoEnded &&
        }

        public void FinishEngagement()
        {
            if (!isInitialized) return;

            sceneLoadManagers.LoadLocation(sceneToLoadData);
        }
    }
}