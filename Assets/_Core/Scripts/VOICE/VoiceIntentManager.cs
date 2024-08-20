using System.Collections.Generic;
using UnityEngine;
using Oculus.Voice;
using Meta.WitAi.Json;
using TMPro;
using FARMLIFEVR.EVENTSYSTEM;

namespace FARMLIFEVR.VOICEEXPERIENCE
{
    public class VoiceIntentManager : MonoBehaviour
    {
        [SerializeField][Required] private AppVoiceExperience appVoiceExperience;

        [SerializeField][Required] private TextMeshProUGUI speechText;

        private void Awake()
        {
            // Binding OnResponse of the AppVoiceExperience to our OnVoiceResponse Method
            if (appVoiceExperience != null && appVoiceExperience.VoiceEvents != null)
            {
                appVoiceExperience.VoiceEvents.OnResponse.AddListener(OnVoiceResponse);
            }
            else
            {
                Debug.LogError("AppVoiceExperience or VoiceEvents is not assigned or initialized properly.");
            }
        }

        private void OnDestroy()
        {
            if (appVoiceExperience != null && appVoiceExperience.VoiceEvents != null)
            {
                appVoiceExperience.VoiceEvents.OnResponse.RemoveListener(OnVoiceResponse);
            }
        }

        private void OnVoiceResponse(WitResponseNode response)
        {
            if (response == null)
            {
                Debug.LogWarning("Voice Response is null");
                return;
            }

            string recognizedText = response["text"].Value;
            UpdateSpeechText(recognizedText);

            var intents = response["intents"];
            if (intents != null && intents.Count > 0)
            {
                string intentName = intents[0]["name"].Value;

                // Trigger the event associated with the intent
                EventManager.TriggerEvent(intentName);
            }
            else
            {
                Debug.Log("<color=yellow> No intents found in the response :( </color>");
            }
        }

        private void UpdateSpeechText(string text)
        {
            if (speechText != null)
            {
                speechText.text = text;
            }
        }


        // Method to start listening
        public void StartListening()
        {
            if (appVoiceExperience != null && !appVoiceExperience.Active)
            {
                appVoiceExperience.Activate();
                Debug.Log("Listening activated.");
            }
        }

        // Method to stop listening
        public void StopListening()
        {
            if (appVoiceExperience != null && appVoiceExperience.Active)
            {
                appVoiceExperience.Deactivate();
                Debug.Log("Listening deactivated.");
            }
        }
    }
}
