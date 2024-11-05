using FARMLIFEVR.EVENTSYSTEM;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using FARMLIFEVR.USERINTERFACE;
using UnityEngine.Assertions;
using FARMLIFEVR.VOICEEXPERIENCE;

namespace FARMLIFEVR.FARMTOOLS
{
    public class Tool_MegaphoneInteractable : XRGrabInteractable
    {
        #region Private Variables

        [Space(5)]

        [Header("Audio Dedection")]
        [Space(3)]
        [SerializeField][Required] private AudioLoudnessDetection audioLoudnessDedection;
        [SerializeField][Required] private BirdShoutingCanvasUI birdShoutingCanvasUI;


        [SerializeField] private float loudnessSensiblity = 100;
        [SerializeField] private float minThreshold = 0.1f;
        [SerializeField] private float threshold = 0.1f;
        [SerializeField] private float waitTime = 1.0f;

        private bool isRecordingVoice = false;
        private bool canUpdateTheCount = true;
        private bool maxShoutCountReached = false;
        private Transform initialTransform;
        [SerializeField][Range(0, 30)] private float loudness = 0;

        private float preLoudness;

        #endregion

        #region Properties

        public bool MaxShoutCountReached => maxShoutCountReached;

        #endregion

        #region LifeCycle Methods

        protected override void Awake()
        {
            base.Awake();
            ValidateConstraints();
            initialTransform = transform;
        }

        private void Start()
        {
            EventManager.StartListening(EventNames.MaxShoutCountReached, OnMaxShoutCountReached);
        }
        protected override void OnDestroy()
        {
            base.OnDestroy();
            EventManager.StopListening(EventNames.MaxShoutCountReached, OnMaxShoutCountReached);
        }
        #endregion

        #region Private Methods

        private void ValidateConstraints()
        {
            Assert.IsNotNull(audioLoudnessDedection, "AudioLoudnessDedection is Null!");
            Assert.IsNotNull(birdShoutingCanvasUI, "BirdShouting Canvas is Null!");
        }

        #endregion

        #region Overriden Methods

        protected override void OnActivated(ActivateEventArgs args)
        {
            base.OnActivated(args);
            isRecordingVoice = true;
            loudness = 0;
            audioLoudnessDedection.StartMicrophoneRecording();
            birdShoutingCanvasUI.ResetFill();
        }

        protected override void OnDeactivated(DeactivateEventArgs args)
        {
            base.OnDeactivated(args);
            isRecordingVoice = false;
            loudness = 0;
            audioLoudnessDedection.StopMicrophoneRecording();
            birdShoutingCanvasUI.ResetFill();
        }

        public override void ProcessInteractable(XRInteractionUpdateOrder.UpdatePhase updatePhase)
        {
            base.ProcessInteractable(updatePhase);
            if (isRecordingVoice)
            {
                preLoudness = audioLoudnessDedection.GetLoudnessFromMicrophone() * loudnessSensiblity;
                if (preLoudness < minThreshold)
                {
                    loudness = 0;
                    return;
                }
                loudness = preLoudness;
                birdShoutingCanvasUI.SetFillAmount(loudness);
                if (loudness > threshold)
                {
                    ThresholdReached();
                }
            }
        }


        private void ThresholdReached()
        {
            if (canUpdateTheCount)
            {
                if (birdShoutingCanvasUI.CanUpdateShoutCount)
                {
                    birdShoutingCanvasUI.CurrentShoutCount++;
                }
                canUpdateTheCount = false;
                Invoke(nameof(EnableCanUpdateTheCount), waitTime);
            }
        }

        private void EnableCanUpdateTheCount()
        {
            canUpdateTheCount = true;
        }

        private void OnMaxShoutCountReached()
        {
            maxShoutCountReached = true;
        }

        #endregion

        #region Public Methods

        public void ShowMegaphoneInteractable()
        {
            maxShoutCountReached = false;
            birdShoutingCanvasUI.ShowCanvas();
            gameObject.SetActive(true);
        }

        public void HideMegaphoneInteractable()
        {
            maxShoutCountReached = false;
            birdShoutingCanvasUI.ResetCanvas();
            birdShoutingCanvasUI.HideCanvas();
            gameObject.SetActive(false);
            transform.position = initialTransform.position;
            transform.rotation = initialTransform.rotation;
        }


        //Must be Removed
        public void SetMaxShoutCount()
        {
            maxShoutCountReached = true;
        }


        #endregion
    }
}
