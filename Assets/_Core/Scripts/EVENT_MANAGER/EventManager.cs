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

        // Trigger an event with dynamic parameters and auto-cleanup of invalid listeners
        public static void TriggerEvent(string eventName, params object[] parameters)
        {
            if (eventDictionary.TryGetValue(eventName, out var listeners))
            {
                List<Delegate> invalidListeners = new List<Delegate>();

                foreach (var listener in listeners)
                {
                    if (listener.Target.Equals(null))
                    {
                        // Add invalid listeners to remove list
                        invalidListeners.Add(listener);
                    }
                    else if (listener is Action<object[]> action)
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

                // Remove invalid listeners
                foreach (var invalidListener in invalidListeners)
                {
                    listeners.Remove(invalidListener);
                }

                // Clean up if no listeners remain
                if (listeners.Count == 0)
                {
                    eventDictionary.Remove(eventName);
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
    }
}