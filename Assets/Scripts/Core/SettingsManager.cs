using UnityEngine;
using System;
using System.Collections.Generic;

namespace Knowledge.Core
{
    public enum Difficulty
    {
        Easy,
        Normal,
        Hard,
        Permadeath
    }

    [Serializable]
    public class SettingsData
    {
        public float masterVolume = 1f;
        public float musicVolume = 0.8f;
        public float sfxVolume = 0.8f;
        public float dialogueVolume = 1f;
        public int qualityLevel = 2;
        public bool vsyncEnabled = false;
        public int resolutionWidth = 1920;
        public int resolutionHeight = 1080;
        public bool fullscreen = true;
        public float fov = 60f;
        public Difficulty difficulty = Difficulty.Normal;
        public float uiScale = 1f;
        public bool showTutorials = true;
        public Dictionary<string, KeyCode> keyBindings = new Dictionary<string, KeyCode>();
    }

    public class SettingsManager : MonoBehaviour
    {
        public static SettingsManager Instance { get; private set; }

        private const string SETTINGS_KEY = "Knowledge_Settings";

        [Header("Default Settings")]
        [SerializeField] private float defaultMasterVolume = 1f;
        [SerializeField] private float defaultMusicVolume = 0.8f;
        [SerializeField] private float defaultSfxVolume = 0.8f;
        [SerializeField] private int defaultQualityLevel = 2;
        [SerializeField] private Difficulty defaultDifficulty = Difficulty.Normal;

        private SettingsData settings;

        public float MasterVolume
        {
            get => settings.masterVolume;
            set
            {
                settings.masterVolume = Mathf.Clamp01(value);
                ApplyAudioSettings();
            }
        }

        public float MusicVolume
        {
            get => settings.musicVolume;
            set => settings.musicVolume = Mathf.Clamp01(value);
        }

        public float SfxVolume
        {
            get => settings.sfxVolume;
            set => settings.sfxVolume = Mathf.Clamp01(value);
        }

        public float DialogueVolume
        {
            get => settings.dialogueVolume;
            set => settings.dialogueVolume = Mathf.Clamp01(value);
        }

        public int QualityLevel
        {
            get => settings.qualityLevel;
            set
            {
                settings.qualityLevel = Mathf.Clamp(value, 0, QualitySettings.names.Length - 1);
                ApplyGraphicsSettings();
            }
        }

        public bool IsVsyncEnabled
        {
            get => settings.vsyncEnabled;
            set
            {
                settings.vsyncEnabled = value;
                QualitySettings.vSyncCount = value ? 1 : 0;
            }
        }

        public Vector2Int Resolution
        {
            get => new Vector2Int(settings.resolutionWidth, settings.resolutionHeight);
            set
            {
                settings.resolutionWidth = Mathf.Max(1, value.x);
                settings.resolutionHeight = Mathf.Max(1, value.y);
                ApplyResolutionSettings();
            }
        }

        public bool Fullscreen
        {
            get => settings.fullscreen;
            set
            {
                settings.fullscreen = value;
                Screen.fullScreen = value;
            }
        }

        public float FOV
        {
            get => settings.fov;
            set => settings.fov = Mathf.Clamp(value, 30f, 120f);
        }

        public Difficulty Difficulty
        {
            get => settings.difficulty;
            set => settings.difficulty = value;
        }

        public float UIScale
        {
            get => settings.uiScale;
            set => settings.uiScale = Mathf.Clamp(value, 0.5f, 2f);
        }

        public bool ShowTutorials
        {
            get => settings.showTutorials;
            set => settings.showTutorials = value;
        }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            LoadSettings();
            InitializeDefaultKeyBindings();
        }

        private void InitializeDefaultKeyBindings()
        {
            if (settings.keyBindings == null || settings.keyBindings.Count == 0)
            {
                settings.keyBindings = new Dictionary<string, KeyCode>
                {
                    { "MoveForward", KeyCode.W },
                    { "MoveBackward", KeyCode.S },
                    { "MoveLeft", KeyCode.A },
                    { "MoveRight", KeyCode.D },
                    { "Jump", KeyCode.Space },
                    { "Sprint", KeyCode.LeftShift },
                    { "Crouch", KeyCode.LeftControl },
                    { "Interact", KeyCode.E },
                    { "Inventory", KeyCode.I },
                    { "Crafting", KeyCode.C },
                    { "Pause", KeyCode.Escape },
                    { "Attack", KeyCode.Mouse0 },
                    { "Defend", KeyCode.Mouse1 }
                };
            }
        }

        public KeyCode GetKeyBinding(string action)
        {
            if (settings.keyBindings.TryGetValue(action, out KeyCode key))
            {
                return key;
            }
            return KeyCode.None;
        }

        public void SetKeyBinding(string action, KeyCode key)
        {
            settings.keyBindings[action] = key;
        }

        public Dictionary<string, KeyCode> GetAllKeyBindings()
        {
            return new Dictionary<string, KeyCode>(settings.keyBindings);
        }

        public void ResetKeyBindings()
        {
            settings.keyBindings.Clear();
            InitializeDefaultKeyBindings();
        }

        public void SaveSettings()
        {
            try
            {
                string json = JsonUtility.ToJson(settings);
                PlayerPrefs.SetString(SETTINGS_KEY, json);
                PlayerPrefs.Save();
                Debug.Log("Settings saved");
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to save settings: {e.Message}");
            }
        }

        public void LoadSettings()
        {
            try
            {
                string json = PlayerPrefs.GetString(SETTINGS_KEY);
                if (!string.IsNullOrEmpty(json))
                {
                    settings = JsonUtility.FromJson<SettingsData>(json);
                }
                else
                {
                    settings = new SettingsData();
                }
            }
            catch
            {
                settings = new SettingsData();
            }

            ApplyAllSettings();
        }

        public void ResetToDefaults()
        {
            settings = new SettingsData
            {
                masterVolume = defaultMasterVolume,
                musicVolume = defaultMusicVolume,
                sfxVolume = defaultSfxVolume,
                qualityLevel = defaultQualityLevel,
                difficulty = defaultDifficulty
            };
            InitializeDefaultKeyBindings();
            ApplyAllSettings();
            SaveSettings();
        }

        private void ApplyAllSettings()
        {
            ApplyAudioSettings();
            ApplyGraphicsSettings();
            ApplyResolutionSettings();
        }

        private void ApplyAudioSettings()
        {
            AudioListener.volume = settings.masterVolume;
        }

        private void ApplyGraphicsSettings()
        {
            QualitySettings.SetQualityLevel(settings.qualityLevel);
            QualitySettings.vSyncCount = settings.vsyncEnabled ? 1 : 0;
        }

        private void ApplyResolutionSettings()
        {
            Screen.SetResolution(settings.resolutionWidth, settings.resolutionHeight, settings.fullscreen);
        }

        public SettingsData GetSettingsData()
        {
            return settings;
        }

        public void ApplySettingsData(SettingsData data)
        {
            settings = data;
            ApplyAllSettings();
            SaveSettings();
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
    }
}
