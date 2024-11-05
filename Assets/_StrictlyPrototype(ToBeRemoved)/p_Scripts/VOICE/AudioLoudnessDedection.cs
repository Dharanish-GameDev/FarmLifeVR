using System.Collections;
using UnityEngine;

namespace FARMLIFEVR.VOICEEXPERIENCE
{
    public class AudioLoudnessDetection : MonoBehaviour
    {
        #region Private Variables

        [SerializeField] private int sampleWindow = 64;
        [SerializeField] private AudioSource audioSource;  // Reference to the AudioSource
        private AudioClip microphoneClip;

        #endregion

        #region Public Methods

        public void StartMicrophoneRecording()
        {
            // Start microphone recording
            microphoneClip = Microphone.Start(MicManager.Instance.CurrentMic, true, 20, AudioSettings.outputSampleRate);

            // Wait until the microphone has started recording
            while (!(Microphone.GetPosition(MicManager.Instance.CurrentMic) > 0)) { }

            // Assign the microphone clip to the AudioSource
            audioSource.clip = microphoneClip;

            // Ensure the AudioSource loops to continue playing live microphone audio
            audioSource.loop = true;

            // Play the AudioSource (will start streaming the microphone input)
            audioSource.Play();
        }

        public float GetLoudnessFromMicrophone()
        {
            return GetLoudnessAudioClip(Microphone.GetPosition(MicManager.Instance.CurrentMic), microphoneClip);
        }

        public float GetLoudnessAudioClip(int clipPos, AudioClip audioClip)
        {
            int startPos = clipPos - sampleWindow;

            if (startPos < 0) return 0;

            float[] waveData = new float[sampleWindow];
            audioClip.GetData(waveData, startPos);

            float totalLoudness = 0;

            for (int i = 0; i < sampleWindow; i++)
            {
                totalLoudness += Mathf.Abs(waveData[i]);
            }

            return totalLoudness / sampleWindow;
        }

        public void StopMicrophoneRecording()
        {
            if (Microphone.IsRecording(MicManager.Instance.CurrentMic))
            {
                Microphone.End(MicManager.Instance.CurrentMic);
                audioSource.Stop();
            }
        }

        #endregion
    }
}