using System.Collections.Generic;
using UnityEngine;
using Oculus.Voice;
using Meta.WitAi; // Updated namespace
using Meta.WitAi.Json; // Updated namespace for WitResponseNode
using UnityEngine.Events;
using TMPro; // Required for TextMeshPro

public class VoiceIntentController : MonoBehaviour
{
    // Reference to the Voice SDK app voice experience
    [SerializeField] private AppVoiceExperience appVoiceExperience;

    // TextMeshPro element to display the recognized speech
    [SerializeField] private TextMeshProUGUI speechText;

    // Define Unity Events for recognized intents
    [System.Serializable]
    public class VoiceCommandEvent : UnityEvent { }
    public VoiceCommandEvent onIntentDetected = new VoiceCommandEvent();

    // Dictionary to map intent names to actions
    private Dictionary<string, UnityAction> intentActions;

    private void Awake()
    {
        // Initialize intent-actions mapping
        intentActions = new Dictionary<string, UnityAction>();

        // Subscribe to the voice events
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
        // Unsubscribe from the voice events
        if (appVoiceExperience != null && appVoiceExperience.VoiceEvents != null)
        {
            appVoiceExperience.VoiceEvents.OnResponse.RemoveListener(OnVoiceResponse);
        }
    }

    private void Update()
    {
        // Start listening when Space is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartListening();
        }

        // Stop listening when Space is released
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StopListening();
        }
    }

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

            // Trigger the corresponding action if the intent exists
            if (intentActions.ContainsKey(intentName))
            {
                intentActions[intentName]?.Invoke();
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

    // Method to add intent-action mapping
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

    // Method to remove intent-action mapping
    public void RemoveIntentAction(string intentName, UnityAction action)
    {
        if (intentActions.ContainsKey(intentName))
        {
            intentActions[intentName] -= action;
        }
    }
}
