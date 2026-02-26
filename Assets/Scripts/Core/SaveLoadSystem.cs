using System;
using System.IO;
using UnityEngine;

namespace Knowledge.Game
{
    [Serializable]
    public class GameSaveData
    {
        public string saveName = "New Game";
        public DateTime savedAt;
        public int totalPlayTime;

        public PlayerSaveData playerData;
        public DiscoverySaveData discoveryData;
        public ProgressionSaveData progressionData;
        public SettingsSaveData settingsData;
    }

    [Serializable]
    public class PlayerSaveData
    {
        public string characterName;
        public int level;
        public float experience;
        public float health;
        public float energy;
        public float hunger;
        public float thirst;
        public float happiness;
        public int socialStatus;
        public Vector3SaveData position;
    }

    [Serializable]
    public class DiscoverySaveData
    {
        public int totalKnowledgePoints;
        public ListStringSaveData discoveredItems;
        public ListStringSaveData unlockedTechnologies;
    }

    [Serializable]
    public class ProgressionSaveData
    {
        public IntBoolDictSaveData unlockedEras;
        public int currentEra;
    }

    [Serializable]
    public class SettingsSaveData
    {
        public float masterVolume = 1f;
        public float musicVolume = 1f;
        public float sfxVolume = 1f;
        public float mouseSensitivity = 1f;
        public bool vsyncEnabled = true;
        public int qualityLevel = 2;
    }

    [Serializable]
    public class Vector3SaveData { public float x, y, z; public Vector3 ToVector3() => new(x, y, z); public static Vector3SaveData From(Vector3 v) => new() { x = v.x, y = v.y, z = v.z }; }
    [Serializable] public class ListStringSaveData { public string[] items = Array.Empty<string>(); }
    [Serializable] public class IntBoolDictSaveData { public IntBoolPair[] pairs = Array.Empty<IntBoolPair>(); }
    [Serializable] public class IntBoolPair { public int key; public bool value; }

    public sealed class SaveLoadSystem : MonoBehaviour
    {
        public static SaveLoadSystem Instance { get; private set; }

        [Header("Save Settings")]
        [SerializeField] private string saveFolderName = "Saves";
        [SerializeField] private int maxSaveSlots = 5;
        [SerializeField] private bool autoSaveEnabled = true;
        [SerializeField] private float autoSaveInterval = 300f;

        private string SaveFolderPath => Path.Combine(Application.persistentDataPath, saveFolderName);
        private float autoSaveTimer;
        private GameSaveData currentSave;

        public bool HasActiveSave => currentSave != null;
        public GameSaveData CurrentSave => currentSave;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (!Directory.Exists(SaveFolderPath))
                Directory.CreateDirectory(SaveFolderPath);
        }

        private void Update()
        {
            if (!autoSaveEnabled || GameManager.Instance?.IsPaused == true) return;

            autoSaveTimer += Time.deltaTime;
            if (autoSaveTimer >= autoSaveInterval)
            {
                autoSaveTimer = 0f;
                if (currentSave != null)
                {
                    SaveGame();
                    Debug.Log("Auto-saved!");
                }
            }
        }

        public void SaveGame(string saveName = "quicksave")
        {
            var saveData = CreateSaveData(saveName);
            string filePath = GetSaveFilePath(saveName);

            try
            {
                string json = JsonUtility.ToJson(saveData, true);
                File.WriteAllText(filePath, json);
                currentSave = saveData;
                Debug.Log($"Game saved: {saveName}");
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to save: {e.Message}");
            }
        }

        public bool LoadGame(string saveName = "quicksave")
        {
            string filePath = GetSaveFilePath(saveName);

            if (!File.Exists(filePath))
            {
                Debug.LogWarning($"Save file not found: {saveName}");
                return false;
            }

            try
            {
                string json = File.ReadAllText(filePath);
                var saveData = JsonUtility.FromJson<GameSaveData>(json);
                ApplySaveData(saveData);
                currentSave = saveData;
                Debug.Log($"Game loaded: {saveName}");
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load: {e.Message}");
                return false;
            }
        }

        public bool DeleteSave(string saveName)
        {
            string filePath = GetSaveFilePath(saveName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                Debug.Log($"Save deleted: {saveName}");
                return true;
            }
            return false;
        }

        public string[] GetAllSaves()
        {
            if (!Directory.Exists(SaveFolderPath))
                return Array.Empty<string>();

            return Directory.GetFiles(SaveFolderPath, "*.json")
                .Select(Path.GetFileNameWithoutExtension)
                .ToArray();
        }

        public bool SaveExists(string saveName = "quicksave")
        {
            return File.Exists(GetSaveFilePath(saveName));
        }

        private GameSaveData CreateSaveData(string saveName)
        {
            var save = new GameSaveData
            {
                saveName = saveName,
                savedAt = DateTime.Now,
                playerData = CreatePlayerData(),
                discoveryData = CreateDiscoveryData(),
                progressionData = CreateProgressionData(),
                settingsData = CreateSettingsData()
            };

            return save;
        }

        private PlayerSaveData CreatePlayerData()
        {
            var player = GameManager.Instance?.Player;
            return new PlayerSaveData
            {
                characterName = "Hero",
                level = player?.Level ?? 1,
                experience = 0,
                health = player?.Health ?? 100,
                energy = player?.Energy ?? 100,
                hunger = player?.Hunger ?? 100,
                thirst = player?.Thirst ?? 100,
                happiness = player?.Happiness ?? 100,
                socialStatus = player?.SocialStatus ?? 1,
                position = Vector3SaveData.From(player?.transform.position ?? Vector3.zero)
            };
        }

        private DiscoverySaveData CreateDiscoveryData()
        {
            var gm = GameManager.Instance;
            return new DiscoverySaveData
            {
                totalKnowledgePoints = gm?.TotalKnowledgePoints ?? 0,
                discoveredItems = new ListStringSaveData { items = gm?.DiscoveredItems?.ToArray() ?? Array.Empty<string>() },
                unlockedTechnologies = new ListStringSaveData { items = Array.Empty<string>() }
            };
        }

        private ProgressionSaveData CreateProgressionData()
        {
            var gm = GameManager.Instance;
            var dict = new IntBoolDictSaveData();

            if (gm?.UnlockedEras != null)
            {
                dict.pairs = gm.UnlockedEras.Select(kvp => new IntBoolPair { key = kvp.Key, value = kvp.Value }).ToArray();
            }

            return new ProgressionSaveData
            {
                unlockedEras = dict,
                currentEra = gm?.CurrentEra ?? 0
            };
        }

        private SettingsSaveData CreateSettingsData()
        {
            return new SettingsSaveData();
        }

        private void ApplySaveData(GameSaveData save)
        {
            var player = GameManager.Instance?.Player;
            if (player != null && save.playerData != null)
            {
                player.Level = save.playerData.level;
            }
        }

        private string GetSaveFilePath(string saveName)
        {
            return Path.Combine(SaveFolderPath, $"{saveName}.json");
        }

        public void NewGame()
        {
            currentSave = null;
            GameManager.Instance?.AddKnowledgePoints(0);
        }
    }
}
