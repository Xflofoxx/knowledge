using UnityEngine;
using System;
using System.Collections.Generic;

namespace Knowledge.Core
{
    public class EventSystem : MonoBehaviour
    {
        public static EventSystem Instance { get; private set; }

        private Dictionary<string, List<Action<object>>> eventListeners = new Dictionary<string, List<Action<object>>>();
        private Dictionary<string, List<Action>> simpleEventListeners = new Dictionary<string, List<Action>>();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void Subscribe(string eventName, Action listener)
        {
            if (!simpleEventListeners.ContainsKey(eventName))
            {
                simpleEventListeners[eventName] = new List<Action>();
            }
            if (!simpleEventListeners[eventName].Contains(listener))
            {
                simpleEventListeners[eventName].Add(listener);
            }
        }

        public void Unsubscribe(string eventName, Action listener)
        {
            if (simpleEventListeners.ContainsKey(eventName))
            {
                simpleEventListeners[eventName].Remove(listener);
            }
        }

        public void Subscribe<T>(string eventName, Action<T> listener)
        {
            if (!eventListeners.ContainsKey(eventName))
            {
                eventListeners[eventName] = new List<Action<object>>();
            }
            
            Action<object> wrappedListener = (data) =>
            {
                if (data is T typedData)
                {
                    listener(typedData);
                }
            };
            
            if (!eventListeners[eventName].Contains(wrappedListener))
            {
                eventListeners[eventName].Add(wrappedListener);
            }
        }

        public void Unsubscribe<T>(string eventName, Action<T> listener)
        {
            if (eventListeners.ContainsKey(eventName))
            {
                Action<object> wrappedListener = (data) =>
                {
                    if (data is T typedData)
                    {
                        listener(typedData);
                    }
                };
                eventListeners[eventName].Remove(wrappedListener);
            }
        }

        public void Publish(string eventName)
        {
            if (simpleEventListeners.ContainsKey(eventName))
            {
                var listeners = new List<Action>(simpleEventListeners[eventName]);
                foreach (var listener in listeners)
                {
                    try
                    {
                        listener?.Invoke();
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"EventSystem: Error invoking event '{eventName}': {e.Message}");
                    }
                }
            }
        }

        public void Publish<T>(string eventName, T data)
        {
            if (eventListeners.ContainsKey(eventName))
            {
                var listeners = new List<Action<object>>(eventListeners[eventName]);
                foreach (var listener in listeners)
                {
                    try
                    {
                        listener?.Invoke(data);
                    }
                    catch (Exception e)
                    {
                        Debug.LogError($"EventSystem: Error invoking event '{eventName}': {e.Message}");
                    }
                }
            }
        }

        public void ClearAllEvents()
        {
            eventListeners.Clear();
            simpleEventListeners.Clear();
        }

        public void ClearEvent(string eventName)
        {
            if (eventListeners.ContainsKey(eventName))
            {
                eventListeners[eventName].Clear();
            }
            if (simpleEventListeners.ContainsKey(eventName))
            {
                simpleEventListeners[eventName].Clear();
            }
        }

        public int GetListenerCount(string eventName)
        {
            int count = 0;
            if (eventListeners.ContainsKey(eventName))
            {
                count += eventListeners[eventName].Count;
            }
            if (simpleEventListeners.ContainsKey(eventName))
            {
                count += simpleEventListeners[eventName].Count;
            }
            return count;
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
            ClearAllEvents();
        }
    }

    public static class GameEvents
    {
        public const string GamePaused = "GamePaused";
        public const string GameResumed = "GameResumed";
        public const string GameStarted = "GameStarted";
        public const string GameOver = "GameOver";
        public const string LevelUp = "LevelUp";
        public const string SaveLoaded = "SaveLoaded";
        public const string SaveSaved = "SaveSaved";

        public const string PlayerHealthChanged = "PlayerHealthChanged";
        public const string PlayerEnergyChanged = "PlayerEnergyChanged";
        public const string PlayerDied = "PlayerDied";
        public const string ItemCollected = "ItemCollected";
        public const string ItemUsed = "ItemUsed";
        public const string EquipmentChanged = "EquipmentChanged";

        public const string ElementDiscovered = "ElementDiscovered";
        public const string RecipeUnlocked = "RecipeUnlocked";
        public const string CraftingComplete = "CraftingComplete";
        public const string KnowledgePointsGained = "KnowledgePointsGained";

        public const string WeatherChanged = "WeatherChanged";
        public const string TimeOfDayChanged = "TimeOfDayChanged";
        public const string EraChanged = "EraChanged";

        public const string QuestStarted = "QuestStarted";
        public const string QuestCompleted = "QuestCompleted";
        public const string QuestFailed = "QuestFailed";

        public const string AchievementUnlocked = "AchievementUnlocked";
    }
}
