using System;
using System.Collections.Generic;
using UnityEngine;

namespace FARMLIFEVR.EVENTSYSTEM
{
    public static class EventManager
    {
        private static Dictionary<string, List<Delegate>> eventDictionary = new Dictionary<string, List<Delegate>>();

        // Add a listener with dynamic parameters
        public static void StartListening(string eventName, Action<object[]> listener)
        {
            if (!eventDictionary.ContainsKey(eventName))
            {
                eventDictionary[eventName] = new List<Delegate>();
            }
            eventDictionary[eventName].Add(listener);
        }

        // Remove a listener with dynamic parameters
        public static void StopListening(string eventName, Action<object[]> listener)
        {
            if (eventDictionary.ContainsKey(eventName))
            {
                eventDictionary[eventName].Remove(listener);
                if (eventDictionary[eventName].Count == 0)
                {
                    eventDictionary.Remove(eventName);
                }
            }
        }

        // Trigger an event with dynamic parameters
        public static void TriggerEvent(string eventName, params object[] parameters)
        {
            if (eventDictionary.TryGetValue(eventName, out var listeners))
            {
                foreach (var listener in listeners)
                {
                    if (listener is Action<object[]> action)
                    {
                        try
                        {
                            action.Invoke(parameters);
                        }
                        catch (Exception ex)
                        {
                            Debug.LogError($"Error invoking event '{eventName}': {ex.Message}");
                        }
                    }
                }
            }
        }

        // Add a listener for events with no parameters
        public static void StartListening(string eventName, Action listener)
        {
            StartListening(eventName, parameters => listener());
        }

        // Remove a listener for events with no parameters
        public static void StopListening(string eventName, Action listener)
        {
            StopListening(eventName, parameters => listener());
        }

        // Trigger an event with no parameters
        public static void TriggerEvent(string eventName)
        {
            TriggerEvent(eventName, new object[] { });
        }

        /// <summary>
        /// |------------------------------------------------------------------------------------------------------------|
        /// | NOTE : CALL THIS METHOD IN THE ONENABLE OF A SCRIPT WHO'S DEFAULT EXECUTION ORDER IS LESS THAN ZERO ("0"). |
        /// |------------------------------------------------------------------------------------------------------------|
        /// 
        /// This Method Clear All the Event's Registered During The previous Game.
        /// </summary>
        public static void InitializeEventDictionary()
        {
            foreach (var eventName in eventDictionary.Keys)
            {
                Debug.Log(eventName);
            }
            Debug.Log("<color=green> Event Dictionary Cleared! </color>");
            eventDictionary.Clear();
        }
    }
}
