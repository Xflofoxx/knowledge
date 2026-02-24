using System.Collections.Generic;
using UnityEngine;

namespace Knowledge.Game
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("Game Settings")]
        public bool gamePaused = false;
        public float gameTime = 0f;
        public int currentEra = 0;

        [Header("Managers")]
        public PlayerController Player;
        public DiscoverySystem DiscoverySystem;
        public KnowledgeSystem KnowledgeSystem;
        public EcosystemManager EcosystemManager;
        public WeatherSystem WeatherSystem;
        public NPCManager NPCManager;
        public UIManager UIManager;

        [Header("Game State")]
        public int TotalKnowledgePoints { get; private set; }
        public List<string> DiscoveredItems { get; } = new();
        public Dictionary<int, bool> UnlockedEras { get; } = new();

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

        private void Start()
        {
            InitializeGame();
        }

        private void InitializeGame()
        {
            UnlockedEras[0] = true;
            for (int i = 1; i < 8; i++)
                UnlockedEras[i] = false;

            Debug.Log("Knowledge - Game Initialized");
        }

        private void Update()
        {
            if (gamePaused) return;
            gameTime += Time.deltaTime;
        }

        public void AddKnowledgePoints(int amount)
        {
            TotalKnowledgePoints += amount;
            KnowledgeSystem?.OnKnowledgeGained(amount);
            UIManager?.UpdateKnowledgeDisplay(TotalKnowledgePoints);
        }

        public void UnlockEra(int eraIndex)
        {
            if (eraIndex >= 0 && eraIndex < 8)
            {
                UnlockedEras[eraIndex] = true;
                Debug.Log($"Era {eraIndex} unlocked!");
            }
        }

        public bool CanAccessEra(int eraIndex)
        {
            return UnlockedEras.ContainsKey(eraIndex) && UnlockedEras[eraIndex];
        }

        public void PauseGame() => gamePaused = true;
        public void ResumeGame() => gamePaused = false;
    }
}
