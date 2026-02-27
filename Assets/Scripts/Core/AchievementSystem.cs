using UnityEngine;
using System;
using System.Collections.Generic;

namespace Knowledge.Core
{
    [Serializable]
    public class Achievement
    {
        public string id;
        public string name;
        public string description;
        public int knowledgeReward;
        public int targetValue = 1;
        public AchievementType type;
        public bool isHidden;
    }

    public enum AchievementType
    {
        Discovery,
        Crafting,
        Combat,
        Exploration,
        Social,
        Collection,
        Progression,
        Special
    }

    [Serializable]
    public class AchievementProgress
    {
        public string achievementId;
        public int currentValue;
        public int targetValue;
        public bool isUnlocked;
    }

    public class AchievementSystem : MonoBehaviour
    {
        public static AchievementSystem Instance { get; private set; }

        [Header("Achievements")]
        [SerializeField] private List<Achievement> achievements = new List<Achievement>();
        [SerializeField] private bool loadOnStart = true;

        private Dictionary<string, Achievement> achievementMap = new Dictionary<string, Achievement>();
        private HashSet<string> unlockedAchievements = new HashSet<string>();
        private Dictionary<string, AchievementProgress> progressMap = new Dictionary<string, AchievementProgress>();

        public event Action<Achievement> OnAchievementUnlocked;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            InitializeAchievements();
        }

        private void Start()
        {
            if (loadOnStart)
            {
                LoadProgress();
            }
        }

        private void InitializeAchievements()
        {
            achievementMap.Clear();
            foreach (var achievement in achievements)
            {
                if (!string.IsNullOrEmpty(achievement.id))
                {
                    achievementMap[achievement.id] = achievement;
                    if (!progressMap.ContainsKey(achievement.id))
                    {
                        progressMap[achievement.id] = new AchievementProgress
                        {
                            achievementId = achievement.id,
                            currentValue = 0,
                            targetValue = achievement.targetValue,
                            isUnlocked = false
                        };
                    }
                }
            }
        }

        public void RegisterAchievement(Achievement achievement)
        {
            if (string.IsNullOrEmpty(achievement.id))
            {
                Debug.LogError("AchievementSystem: Cannot register achievement with empty ID");
                return;
            }

            if (achievementMap.ContainsKey(achievement.id))
            {
                Debug.LogWarning($"AchievementSystem: Achievement '{achievement.id}' already registered");
                return;
            }

            achievementMap[achievement.id] = achievement;

            if (!progressMap.ContainsKey(achievement.id))
            {
                progressMap[achievement.id] = new AchievementProgress
                {
                    achievementId = achievement.id,
                    currentValue = 0,
                    targetValue = achievement.targetValue,
                    isUnlocked = false
                };
            }

            if (!achievements.Contains(achievement))
            {
                achievements.Add(achievement);
            }
        }

        public bool UnlockAchievement(string achievementId)
        {
            if (!achievementMap.ContainsKey(achievementId))
            {
                Debug.LogWarning($"AchievementSystem: Achievement '{achievementId}' not found");
                return false;
            }

            if (unlockedAchievements.Contains(achievementId))
            {
                return false;
            }

            var achievement = achievementMap[achievementId];
            unlockedAchievements.Add(achievementId);

            if (progressMap.ContainsKey(achievementId))
            {
                progressMap[achievementId].isUnlocked = true;
                progressMap[achievementId].currentValue = progressMap[achievementId].targetValue;
            }

            if (GameManager.Instance != null && achievement.knowledgeReward > 0)
            {
                GameManager.Instance.AddKnowledgePoints(achievement.knowledgeReward);
            }

            EventSystem eventSystem = EventSystem.Instance;
            if (eventSystem != null)
            {
                eventSystem.Publish(GameEvents.AchievementUnlocked, achievement);
            }

            OnAchievementUnlocked?.Invoke(achievement);

            Debug.Log($"Achievement Unlocked: {achievement.name} (+{achievement.knowledgeReward} KP)");

            SaveProgress();

            return true;
        }

        public void UpdateProgress(string achievementId, int newValue)
        {
            if (!achievementMap.ContainsKey(achievementId))
            {
                return;
            }

            var achievement = achievementMap[achievementId];

            if (!progressMap.ContainsKey(achievementId))
            {
                progressMap[achievementId] = new AchievementProgress
                {
                    achievementId = achievementId,
                    currentValue = 0,
                    targetValue = achievement.targetValue,
                    isUnlocked = false
                };
            }

            var progress = progressMap[achievementId];
            progress.currentValue = Mathf.Min(newValue, progress.targetValue);

            if (progress.currentValue >= progress.targetValue && !progress.isUnlocked)
            {
                UnlockAchievement(achievementId);
            }

            SaveProgress();
        }

        public bool IsUnlocked(string achievementId)
        {
            return unlockedAchievements.Contains(achievementId);
        }

        public bool HasAchievement(string achievementId)
        {
            return achievementMap.ContainsKey(achievementId);
        }

        public int GetUnlockedCount()
        {
            return unlockedAchievements.Count;
        }

        public int GetTotalAchievementCount()
        {
            return achievementMap.Count;
        }

        public float GetCompletionPercentage()
        {
            int total = achievementMap.Count;
            if (total == 0) return 0f;
            return (float)unlockedAchievements.Count / total * 100f;
        }

        public int GetTotalKnowledgeFromAchievements()
        {
            int total = 0;
            foreach (var achievementId in unlockedAchievements)
            {
                if (achievementMap.TryGetValue(achievementId, out var achievement))
                {
                    total += achievement.knowledgeReward;
                }
            }
            return total;
        }

        public AchievementProgress GetProgress(string achievementId)
        {
            if (progressMap.TryGetValue(achievementId, out var progress))
            {
                return progress;
            }
            return null;
        }

        public List<Achievement> GetAllAchievements()
        {
            return new List<Achievement>(achievementMap.Values);
        }

        public List<Achievement> GetUnlockedAchievements()
        {
            List<Achievement> unlocked = new List<Achievement>();
            foreach (var achievementId in unlockedAchievements)
            {
                if (achievementMap.TryGetValue(achievementId, out var achievement))
                {
                    unlocked.Add(achievement);
                }
            }
            return unlocked;
        }

        public void ResetAllProgress()
        {
            unlockedAchievements.Clear();
            progressMap.Clear();
            foreach (var achievement in achievementMap.Values)
            {
                progressMap[achievement.id] = new AchievementProgress
                {
                    achievementId = achievement.id,
                    currentValue = 0,
                    targetValue = achievement.targetValue,
                    isUnlocked = false
                };
            }
            SaveProgress();
        }

        private void SaveProgress()
        {
            string key = "Knowledge_Achievements";
            string json = JsonUtility.ToJson(new AchievementSaveData
            {
                unlockedIds = new List<string>(unlockedAchievements),
                progress = new Dictionary<string, AchievementProgress>(progressMap)
            });
            PlayerPrefs.SetString(key, json);
            PlayerPrefs.Save();
        }

        private void LoadProgress()
        {
            string key = "Knowledge_Achievements";
            if (PlayerPrefs.HasKey(key))
            {
                try
                {
                    string json = PlayerPrefs.GetString(key);
                    var saveData = JsonUtility.FromJson<AchievementSaveData>(json);
                    if (saveData != null)
                    {
                        unlockedAchievements = new HashSet<string>(saveData.unlockedIds);
                        progressMap = saveData.progress ?? new Dictionary<string, AchievementProgress>();
                    }
                }
                catch (Exception e)
                {
                    Debug.LogError($"AchievementSystem: Failed to load progress - {e.Message}");
                }
            }
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
        }
    }

    [Serializable]
    public class AchievementSaveData
    {
        public List<string> unlockedIds;
        public Dictionary<string, AchievementProgress> progress;
    }
}
