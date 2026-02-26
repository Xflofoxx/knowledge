using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Knowledge.Game
{
    public sealed class SceneLoader : MonoBehaviour
    {
        public static SceneLoader Instance { get; private set; }

        public static readonly string BootScene = "Boot";
        public static readonly string MainMenuScene = "MainMenu";
        public static readonly string WorldMapScene = "WorldMap";
        public static readonly string StoneAgeScene = "StoneAge";
        public static readonly string CharacterCreationScene = "CharacterCreation";

        [Header("Loading")]
        [SerializeField] private bool showLoadingScreen = true;
        [SerializeField] private float minimumLoadTime = 1f;

        [Header("Scene References")]
        [SerializeField] private string initialScene = BootScene;

        public string CurrentScene => UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        public bool IsLoading { get; private set; }

        public event Action<string> OnSceneLoaded;
        public event Action<string> OnSceneChanging;

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
            if (!string.IsNullOrEmpty(initialScene))
                LoadScene(initialScene);
        }

        public void LoadScene(string sceneName)
        {
            if (IsLoading || string.IsNullOrEmpty(sceneName)) return;

            StartCoroutine(LoadSceneRoutine(sceneName));
        }

        private IEnumerator LoadSceneRoutine(string sceneName)
        {
            IsLoading = true;
            OnSceneChanging?.Invoke(sceneName);

            float loadStartTime = Time.time;

            AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            float elapsedTime = Time.time - loadStartTime;
            if (elapsedTime < minimumLoadTime)
            {
                yield return new WaitForSeconds(minimumLoadTime - elapsedTime);
            }

            IsLoading = false;
            OnSceneLoaded?.Invoke(sceneName);

            Debug.Log($"Scene loaded: {sceneName}");
        }

        public void LoadMainMenu() => LoadScene(MainMenuScene);
        public void LoadWorldMap() => LoadScene(WorldMapScene);
        public void LoadStoneAge() => LoadScene(StoneAgeScene);
        public void LoadCharacterCreation() => LoadScene(CharacterCreationScene);

        public void ReloadCurrentScene()
        {
            LoadScene(CurrentScene);
        }

        public void QuitGame()
        {
            Debug.Log("Quitting game...");
            Application.Quit();

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
    }
}
