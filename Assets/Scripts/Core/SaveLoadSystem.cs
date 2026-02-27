using UnityEngine;
using System.IO;
using System.Collections.Generic;

namespace Knowledge.Core
{
    [System.Serializable]
    public class GameData
    {
        public string playerName;
        public int knowledgePoints;
        public string currentEra;
        public long timestamp;
        public PlayerSaveData playerData;
        public DiscoverySaveData discoveryData;
        public WorldSaveData worldData;
    }

    [System.Serializable]
    public class PlayerSaveData
    {
        public float health;
        public float energy;
        public float hunger;
        public float thirst;
        public int level;
        public int experience;
    }

    [System.Serializable]
    public class DiscoverySaveData
    {
        public List<string> discoveredItems;
        public List<string> knownRecipes;
    }

    [System.Serializable]
    public class WorldSaveData
    {
        public string currentScene;
        public float timeOfDay;
        public string weather;
    }

    public class SaveLoadSystem : MonoBehaviour
    {
        public static SaveLoadSystem Instance { get; private set; }

        [Header("Save Settings")]
        [SerializeField] private string saveFolderName = "Saves";
        [SerializeField] private float autoSaveIntervalMinutes = 5f;
        [SerializeField] private int maxSaveSlots = 10;

        private string saveDirectory;
        private float autoSaveTimer;
        private bool autoSaveEnabled = true;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            saveDirectory = Application.persistentDataPath + "/" + saveFolderName + "/";
            EnsureSaveDirectoryExists();
        }

        private void Update()
        {
            if (autoSaveEnabled && autoSaveIntervalMinutes > 0)
            {
                autoSaveTimer += Time.deltaTime;
                if (autoSaveTimer >= autoSaveIntervalMinutes * 60f)
                {
                    autoSaveTimer = 0f;
                    AutoSave();
                }
            }
        }

        private void EnsureSaveDirectoryExists()
        {
            if (!Directory.Exists(saveDirectory))
            {
                Directory.CreateDirectory(saveDirectory);
            }
        }

        public bool SaveGame(string slotId, GameData data)
        {
            if (string.IsNullOrEmpty(slotId))
            {
                Debug.LogError("SaveLoadSystem: Slot ID cannot be empty");
                return false;
            }

            try
            {
                EnsureSaveDirectoryExists();
                data.timestamp = System.DateTime.Now.Ticks;
                string json = JsonUtility.ToJson(data, true);
                string filePath = saveDirectory + slotId + ".json";
                File.WriteAllText(filePath, json);
                Debug.Log($"Game saved to slot: {slotId}");
                return true;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"SaveLoadSystem: Failed to save - {e.Message}");
                return false;
            }
        }

        public GameData LoadGame(string slotId)
        {
            if (string.IsNullOrEmpty(slotId))
            {
                Debug.LogError("SaveLoadSystem: Slot ID cannot be empty");
                return null;
            }

            try
            {
                string filePath = saveDirectory + slotId + ".json";
                if (!File.Exists(filePath))
                {
                    Debug.LogWarning($"SaveLoadSystem: Save file not found - {slotId}");
                    return null;
                }

                string json = File.ReadAllText(filePath);
                GameData data = JsonUtility.FromJson<GameData>(json);
                Debug.Log($"Game loaded from slot: {slotId}");
                return data;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"SaveLoadSystem: Failed to load - {e.Message}");
                return null;
            }
        }

        public string[] GetSaveSlots()
        {
            EnsureSaveDirectoryExists();
            string[] files = Directory.GetFiles(saveDirectory, "*.json");
            string[] slots = new string[files.Length];

            for (int i = 0; i < files.Length; i++)
            {
                slots[i] = Path.GetFileNameWithoutExtension(files[i]);
            }

            return slots;
        }

        public bool DeleteSave(string slotId)
        {
            if (string.IsNullOrEmpty(slotId))
            {
                return false;
            }

            try
            {
                string filePath = saveDirectory + slotId + ".json";
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    Debug.Log($"Save deleted: {slotId}");
                    return true;
                }
                return false;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"SaveLoadSystem: Failed to delete - {e.Message}");
                return false;
            }
        }

        public void SetAutoSaveInterval(float minutes)
        {
            autoSaveIntervalMinutes = Mathf.Max(0, minutes);
            autoSaveEnabled = autoSaveIntervalMinutes > 0;
        }

        public void EnableAutoSave(bool enable)
        {
            autoSaveEnabled = enable;
            if (enable)
            {
                autoSaveTimer = 0f;
            }
        }

        private void AutoSave()
        {
            if (GameManager.Instance != null)
            {
                GameData autoSaveData = new GameData
                {
                    knowledgePoints = GameManager.Instance.TotalKnowledgePoints,
                    currentEra = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
                };
                SaveGame("auto_save", autoSaveData);
                Debug.Log("Auto save completed");
            }
        }

        public bool SaveExists(string slotId)
        {
            string filePath = saveDirectory + slotId + ".json";
            return File.Exists(filePath);
        }

        public long GetSaveTimestamp(string slotId)
        {
            GameData data = LoadGame(slotId);
            return data?.timestamp ?? 0;
        }

        public int GetMaxSaveSlots()
        {
            return maxSaveSlots;
        }

        public string GetSaveDirectory()
        {
            return saveDirectory;
        }
    }
}
