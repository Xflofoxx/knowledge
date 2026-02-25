using System.Collections.Generic;
using UnityEngine;

namespace Knowledge.Game
{
    public sealed class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        private const int MaxEras = 8;

        [Header("Game Settings")]
        [SerializeField] private bool gamePaused = false;
        [SerializeField] private float gameTime = 0f;
        [SerializeField] private int currentEra = 0;
        public bool IsGamePaused => gamePaused;

        [Header("Managers")]
        [SerializeField] private PlayerController player;
        [SerializeField] private DiscoverySystem discoverySystem;
        [SerializeField] private KnowledgeSystem knowledgeSystem;
        [SerializeField] private EcosystemManager ecosystemManager;
        [SerializeField] private WeatherSystem weatherSystem;
        [SerializeField] private NPCManager npcManager;
        [SerializeField] private UIManager uiManager;

        [Header("Game State")]
        public int TotalKnowledgePoints { get; private set; }
        public List<string> DiscoveredItems { get; } = new();
        public Dictionary<int, bool> UnlockedEras { get; } = new();

        public PlayerController Player => player;
        public DiscoverySystem DiscoverySystemRef => discoverySystem;
        public KnowledgeSystem KnowledgeSystemRef => knowledgeSystem;
        public EcosystemManager EcosystemManagerRef => ecosystemManager;
        public WeatherSystem WeatherSystemRef => weatherSystem;
        public NPCManager NPCManagerRef => npcManager;
        public UIManager UIManagerRef => uiManager;
        public bool IsPaused => gamePaused;
        public float GameTime => gameTime;
        public int CurrentEra => currentEra;

        private void Awake()
        {
            if (Instance != null)
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
            for (int i = 1; i < MaxEras; i++)
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
            if (amount <= 0) return;
            
            TotalKnowledgePoints += amount;
            knowledgeSystem?.OnKnowledgeGained(amount);
            uiManager?.UpdateKnowledgeDisplay(TotalKnowledgePoints);
        }

        public void UnlockEra(int eraIndex)
        {
            if (eraIndex is < 0 or >= MaxEras) return;
            
            UnlockedEras[eraIndex] = true;
            Debug.Log($"Era {eraIndex} unlocked!");
        }

        public bool CanAccessEra(int eraIndex)
        {
            return eraIndex >= 0 && eraIndex < MaxEras && 
                   UnlockedEras.TryGetValue(eraIndex, out bool unlocked) && unlocked;
        }

        public void PauseGame() => SetPaused(true);
        public void ResumeGame() => SetPaused(false);
        public void SetPaused(bool paused)
        {
            gamePaused = paused;
            Time.timeScale = paused ? 0f : 1f;
        }
    }
}
