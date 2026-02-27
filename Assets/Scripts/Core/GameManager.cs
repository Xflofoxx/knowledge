using UnityEngine;
using UnityEngine.SceneManagement;

namespace Knowledge.Core
{
    public enum GameState
    {
        Loading,
        Playing,
        Paused,
        Menu,
        GameOver
    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("Game Settings")]
        [SerializeField] private float minTimeScale = 0.1f;
        [SerializeField] private float maxTimeScale = 2f;

        private GameState currentState = GameState.Loading;
        private float timeScale = 1f;
        private int totalKnowledgePoints;

        public bool IsGamePaused => currentState == GameState.Paused;
        
        public float TimeScale 
        { 
            get => timeScale;
            set => SetTimeScale(value);
        }

        public int TotalKnowledgePoints => totalKnowledgePoints;

        public GameState CurrentState => currentState;

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
            SetGameState(GameState.Playing);
        }

        public void PauseGame()
        {
            SetGameState(GameState.Paused);
        }

        public void ResumeGame()
        {
            SetGameState(GameState.Playing);
        }

        public void AddKnowledgePoints(int amount)
        {
            if (amount > 0)
            {
                totalKnowledgePoints += amount;
            }
        }

        public void SetTimeScale(float scale)
        {
            timeScale = Mathf.Clamp(scale, minTimeScale, maxTimeScale);
            Time.timeScale = timeScale;
        }

        public void SetGameState(GameState newState)
        {
            if (currentState == newState) return;

            currentState = newState;

            if (newState == GameState.Paused)
            {
                Time.timeScale = 0f;
            }
            else if (newState == GameState.Playing)
            {
                Time.timeScale = timeScale;
            }
        }

        public void SaveGame(string path)
        {
            var saveData = new GameSaveData
            {
                knowledgePoints = totalKnowledgePoints,
                currentScene = SceneManager.GetActiveScene().name,
                gameState = currentState,
                timeScale = timeScale
            };

            string json = JsonUtility.ToJson(saveData);
            System.IO.File.WriteAllText(path, json);
        }

        public void LoadGame(string path)
        {
            if (!System.IO.File.Exists(path)) return;

            string json = System.IO.File.ReadAllText(path);
            var saveData = JsonUtility.FromJson<GameSaveData>(json);

            totalKnowledgePoints = saveData.knowledgePoints;
            timeScale = saveData.timeScale;
            Time.timeScale = timeScale;

            if (!string.IsNullOrEmpty(saveData.currentScene))
            {
                SceneManager.LoadScene(saveData.currentScene);
            }

            SetGameState(saveData.gameState);
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void QuitGame()
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }

        private void OnDestroy()
        {
            if (Instance == this)
            {
                Instance = null;
            }
            Time.timeScale = 1f;
        }
    }

    [System.Serializable]
    public class GameSaveData
    {
        public int knowledgePoints;
        public string currentScene;
        public GameState gameState;
        public float timeScale;
    }
}
