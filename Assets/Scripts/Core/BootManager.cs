using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Knowledge.Game
{
    public sealed class BootManager : MonoBehaviour
    {
        public static BootManager Instance { get; private set; }

        [Header("Boot Settings")]
        [SerializeField] private float minimumBootTime = 2f;
        [SerializeField] private bool skipBootOnInput = true;

        [Header("References")]
        [SerializeField] private Image logoImage;
        [SerializeField] private Image progressBar;
        [SerializeField] private Text statusText;

        private float bootTimer;
        private bool bootComplete;
        private string currentStatus = "Initializing...";

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            StartCoroutine(BootSequence());
        }

        private void Update()
        {
            if (skipBootOnInput && !bootComplete && Input.anyKeyDown)
            {
                bootTimer = minimumBootTime;
            }

            UpdateUI();
        }

        private IEnumerator BootSequence()
        {
            bootTimer = 0f;
            currentStatus = "Loading core systems...";
            yield return new WaitForSeconds(0.5f);

            currentStatus = "Initializing game manager...";
            yield return new WaitForSeconds(0.3f);

            currentStatus = "Loading player data...";
            yield return new WaitForSeconds(0.3f);

            currentStatus = "Preparing world...";
            yield return new WaitForSeconds(0.3f);

            while (bootTimer < minimumBootTime)
            {
                bootTimer += Time.deltaTime;
                yield return null;
            }

            currentStatus = "Ready!";
            bootComplete = true;

            yield return new WaitForSeconds(0.5f);

            LoadMainMenu();
        }

        private void UpdateUI()
        {
            if (progressBar != null)
            {
                float progress = Mathf.Clamp01(bootTimer / minimumBootTime);
                progressBar.fillAmount = progress;
            }

            if (statusText != null)
            {
                statusText.text = currentStatus;
            }
        }

        private void LoadMainMenu()
        {
            var loader = FindObjectOfType<SceneLoader>();
            if (loader != null)
            {
                loader.LoadMainMenu();
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
            }
        }

        public void SkipBoot()
        {
            bootTimer = minimumBootTime;
        }
    }
}
