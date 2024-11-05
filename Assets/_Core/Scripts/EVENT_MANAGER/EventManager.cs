using System;
using System.Collections.Generic;
using UnityEngine;

namespace FARMLIFEVR.EVENTSYSTEM
{
    public static class EventManager
    {
        private static Dictionary<string, List<Delegate>> eventDictionary = new Dictionary<string, List<Delegate>>();
        private static Dictionary<Action, Action<object[]>> actionMappings = new Dictionary<Action, Action<object[]>>();

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
            Action<object[]> wrappedAction = parameters => listener();

            // Store the mapping so we can use the same delegate when unsubscribing
            if (!actionMappings.ContainsKey(listener))
            {
                actionMappings[listener] = wrappedAction;
            }

            StartListening(eventName, wrappedAction);
        }

        // Remove a listener for events with no parameters
        public static void StopListening(string eventName, Action listener)
        {
            // Retrieve the wrapped delegate
            if (actionMappings.TryGetValue(listener, out var wrappedAction))
            {
                StopListening(eventName, wrappedAction);
                actionMappings.Remove(listener);  // Remove the mapping once done
            }
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
            actionMappings.Clear();  // Clear mappings as well
        }

        public static void PrintEventsInDictionary()
        {
            foreach (var eventName in eventDictionary.Keys)
            {
                Debug.Log(eventName);
            }
        }
    }
}
