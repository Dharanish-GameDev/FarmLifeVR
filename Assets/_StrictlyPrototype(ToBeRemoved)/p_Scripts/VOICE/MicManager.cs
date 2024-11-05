using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Meta.WitAi.Lib;

namespace FARMLIFEVR.VOICEEXPERIENCE
{
    public class MicManager : MonoBehaviour
    {
        public static MicManager Instance { get; private set; }

        [SerializeField] private List<string> micNames = new List<string>();
        [SerializeField] private string currentMic;
        [SerializeField][Required] private TMP_Dropdown micDropdown; // Reference to the TMP_Dropdown
        private int micCount = 0; // To track the number of mics
        private Coroutine micCoroutine;

        [SerializeField]
        private Mic mic;

        public string CurrentMic => currentMic;

        private void Awake()
        {
            Instance = this;

            UpdateMicrophoneList();
            if (micNames.Count > 0)
            {
                currentMic = micNames[0]; // Set the default mic
            }

            PopulateDropdown(); // Populate dropdown with mics
            micDropdown.onValueChanged.AddListener(delegate { OnMicDropdownValueChanged(); }); // Add listener to dropdown
            micCoroutine = StartCoroutine(CheckForMicChanges()); // Start the mic check coroutine

            Invoke(nameof(CheckAndChangeWitAIMicrophone), 1);
        }

        private void OnDisable()
        {
            StopCoroutine(micCoroutine);
        }

        private void UpdateMicrophoneList()
        {
            micNames.Clear(); // Clear the list before updating

            for (int i = 0; i < Microphone.devices.Length; i++)
            {
                micNames.Add(Microphone.devices[i]); // Add new mic devices
            }

            micCount = Microphone.devices.Length; // Update mic count
        }

        private void PopulateDropdown()
        {
            micDropdown.ClearOptions(); // Clear existing options in dropdown
            micDropdown.AddOptions(micNames); // Add microphone names as dropdown options
        }

        public void SwitchMic(string newMic)
        {
            if (micNames.Contains(newMic))
            {
                currentMic = newMic;
                CheckAndChangeWitAIMicrophone();
            }
            else
            {
                Debug.LogWarning("Microphone not found: " + newMic);
            }
        }

        public void OnMicDropdownValueChanged()
        {
            string selectedMic = micDropdown.options[micDropdown.value].text; // Get selected mic from dropdown
            SwitchMic(selectedMic); // Switch to the selected mic
        }
        private IEnumerator CheckForMicChanges()
        {
            while (true)
            {
                if (Microphone.devices.Length != micCount)
                {
                    Debug.Log("Microphone change detected, updating list...");
                    UpdateMicrophoneList();

                    if (!micNames.Contains(currentMic))
                    {
                        // If the current mic is removed, switch to the default mic (devices[0])
                        currentMic = micNames[0];
                        Debug.Log("Current mic was removed, switching to default mic: " + currentMic);

                        // Update TMP_Dropdown to select the new default mic
                        micDropdown.value = 0;
                        SwitchMic(currentMic); // Switch to default mic
                    }

                    PopulateDropdown(); // Update the dropdown options if mics change
                }

                yield return new WaitForSeconds(3f); // Check every  3 second
            }
        }


        private void CheckAndChangeWitAIMicrophone()
        {
            if (CheckForMicComponentPresence())
            {
                ChangeInputMicrophoneOfWitAI();
            }
        }

        private bool CheckForMicComponentPresence()
        {
            if (mic == null)
            {
                mic = FindObjectOfType<Mic>();
            }
            return mic != null;
        }

        private void ChangeInputMicrophoneOfWitAI()
        {
            int index = GetMicIndexFromName(currentMic);
            if (mic.CurrentDeviceIndex == index) return;
            mic.ChangeMicDevice(index);
        }

        private int GetMicIndexFromName(string micName)
        {
            if (micNames.Contains(micName))
            {
                return micNames.IndexOf(micName);
            }
            return 0;
        }
    }
}
