using System.Collections.Generic;
using UnityEngine;
using Oculus.Voice;
using Meta.WitAi.Json;
using UnityEngine.Events;
using TMPro;


namespace FARMLIFEVR.VOICEEXPERIENCE
{
    /// <summary>
    /// This Class Manages to Receive the Voice Input and Convert it into text and then process the Text using Wit.ai then return the Intent if its exixts.
    /// </summary>
    public class VoiceIntentController : MonoBehaviour
    {
        #region Private Variables

        [SerializeField][Required] private AppVoiceExperience appVoiceExperience;

        [SerializeField][Required] private TextMeshProUGUI speechText; // TextMeshPro element to display the recognized speech

        // Dictionary to map intent names to actions
        private Dictionary<string, UnityAction> intentActions;

        #endregion

        #region Public Variables

        //[HideInInspector]
        //public VoiceCommandEvent onIntentDetected = new VoiceCommandEvent();

        //// Define Unity Events for recognized intents
        //[System.Serializable]
        //public class VoiceCommandEvent : UnityEvent { }

        #endregion

        #region LifeCycle Methods

        private void Awake()
        {
            // Initialize intent-actions mapping dictionary
            intentActions = new Dictionary<string, UnityAction>();

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
            // Binding OnResponse of the AppVoiceExperience to our OnVoiceResponse Method
            if (appVoiceExperience != null && appVoiceExperience.VoiceEvents != null)
            {
                appVoiceExperience.VoiceEvents.OnResponse.RemoveListener(OnVoiceResponse);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// This Method calls when getting Response from the AppVoiceExperience after Listening with the WitResponseNode Param
        /// </summary>
        /// <param name="response"></param>
        private void OnVoiceResponse(WitResponseNode response)
        {
            if (response == null)
            {
                Debug.LogWarning("Voice Response is null");
                return;
            }

            // Extract the recognized speech from the response
            string recognizedText = response["text"].Value;

            // Update UI Text with the recognized speech
            UpdateSpeechText(recognizedText);

            // Extract the first intent from the response
            var intents = response["intents"];
            if (intents != null && intents.Count > 0)
            {
                string intentName = intents[0]["name"].Value;

                if (intentActions.ContainsKey(intentName))
                {
                    intentActions[intentName]?.Invoke(); // Invoking if the intent Exists in the Dictionary
                }
                else
                {
                    Debug.LogWarning("Intent not recognized: " + intentName);
                }
            }
            else
            {
                Debug.LogWarning("No intents found in the response.");
            }
        }

        // Method to update the UI Text with the recognized speech
        private void UpdateSpeechText(string text)
        {
            if (speechText != null)
            {
                speechText.text = text;
            }
        }

        #endregion

        #region Public Methods
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

        /// <summary>
        /// Method to Subscribe to the Specific Intent Found while OnResponse Callback
        /// </summary>
        /// <param name="intentName"></param>
        /// <param name="action"></param>
        public void AddIntentAction(string intentName, UnityAction action)
        {
            if (!intentActions.ContainsKey(intentName))
            {
                intentActions.Add(intentName, action);
            }
            else
            {
                intentActions[intentName] = action;
            }
        }

        /// <summary>
        /// Method to UnSubscribe to the Specific Intent Found while OnResponse Callback
        /// </summary>
        /// <param name="intentName"></param>
        /// <param name="action"></param>
        public void RemoveIntentAction(string intentName, UnityAction action)
        {
            if (intentActions.ContainsKey(intentName))
            {
                intentActions[intentName] -= action;
            }
        }

        #endregion
    }
}

