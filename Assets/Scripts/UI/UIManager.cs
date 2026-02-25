using UnityEngine;
using UnityEngine.UI;

namespace Knowledge.Game
{
    public sealed class UIManager : MonoBehaviour
    {
        #region Constants
        private static readonly string[] Eras = { "Pietra", "Bronzo", "Ferro", "Medioevo", "Rinascimento", "Industriale", "Moderna", "Spazio" };
        private const int HoursPerDay = 24;
        private const int MinutesPerHour = 60;
        private const int SecondsPerHour = 3600;
        private const int SecondsPerMinute = 60;
        #endregion

        public static UIManager Instance { get; private set; }

        [Header("HUD Elements")]
        [SerializeField] private Slider healthBar;
        [SerializeField] private Slider energyBar;
        [SerializeField] private Slider hungerBar;
        [SerializeField] private Slider thirstBar;
        [SerializeField] private Text knowledgePointsText;
        [SerializeField] private Text levelText;
        [SerializeField] private Text eraText;
        public Text KnowledgePointsText => knowledgePointsText;

        [Header("Panels")]
        [SerializeField] private GameObject inventoryPanel;
        [SerializeField] private GameObject craftingPanel;
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject dialogPanel;
        [SerializeField] private GameObject mapPanel;
        public GameObject InventoryPanel => inventoryPanel;
        public GameObject CraftingPanel => craftingPanel;
        public GameObject PauseMenu => pauseMenu;
        public GameObject DialogPanel => dialogPanel;
        public GameObject MapPanel => mapPanel;

        [Header("Info Display")]
        [SerializeField] private Text weatherText;
        [SerializeField] private Text locationText;
        [SerializeField] private Text timeText;

        private bool inventoryOpen;
        private bool craftingOpen;
        private bool mapOpen;

        public bool IsInventoryOpen => inventoryOpen;
        public bool IsCraftingOpen => craftingOpen;
        public bool IsMapOpen => mapOpen;
        public bool IsPauseMenuOpen => pauseMenu?.activeSelf ?? false;

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
            UpdateAllDisplay();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
                ToggleInventory();
            else if (Input.GetKeyDown(KeyCode.C))
                ToggleCrafting();
            else if (Input.GetKeyDown(KeyCode.M))
                ToggleMap();
            else if (Input.GetKeyDown(KeyCode.Escape))
                TogglePause();

            UpdateHUD();
        }

        public void UpdateHUD()
        {
            var player = GameManager.Instance?.Player;
            if (player == null) return;

            UpdateBars(player);
            UpdateTexts(player);
        }

        private void UpdateBars(PlayerController player)
        {
            if (healthBar) healthBar.value = player.Health / player.MaxHealth;
            if (energyBar) energyBar.value = player.Energy / player.MaxEnergy;
            if (hungerBar) hungerBar.value = player.Hunger / player.MaxHunger;
            if (thirstBar) thirstBar.value = player.Thirst / player.MaxThirst;
        }

        private void UpdateTexts(PlayerController player)
        {
            if (knowledgePointsText)
                knowledgePointsText.text = $"KP: {GameManager.Instance?.TotalKnowledgePoints ?? 0}";
            
            if (levelText)
                levelText.text = $"LVL: {player.Level}";

            if (eraText && GameManager.Instance != null)
            {
                int eraIndex = GameManager.Instance.CurrentEra;
                eraIndex = Mathf.Clamp(eraIndex, 0, Eras.Length - 1);
                eraText.text = $"Era: {Eras[eraIndex]}";
            }

            if (weatherText)
                weatherText.text = WeatherSystem.Instance?.GetWeatherDescription() ?? string.Empty;

            if (timeText && GameManager.Instance != null)
            {
                float totalSeconds = GameManager.Instance.GameTime;
                int hours = (int)(totalSeconds / SecondsPerHour) % HoursPerDay;
                int minutes = (int)(totalSeconds / SecondsPerMinute) % MinutesPerHour;
                timeText.text = $"{hours:D2}:{minutes:D2}";
            }
        }

        public void UpdateKnowledgeDisplay(int kp)
        {
            if (knowledgePointsText)
                knowledgePointsText.text = $"KP: {kp}";
        }

        public void UpdateAllDisplay() => UpdateHUD();

        public void ToggleInventory()
        {
            inventoryOpen = !inventoryOpen;
            if (inventoryPanel) inventoryPanel.SetActive(inventoryOpen);
        }

        public void ToggleCrafting()
        {
            craftingOpen = !craftingOpen;
            if (craftingPanel) craftingPanel.SetActive(craftingOpen);
        }

        public void ToggleMap()
        {
            mapOpen = !mapOpen;
            if (mapPanel) mapPanel.SetActive(mapOpen);
        }

        public void TogglePause()
        {
            if (pauseMenu)
            {
                bool isActive = !pauseMenu.activeSelf;
                pauseMenu.SetActive(isActive);
                GameManager.Instance.SetPaused(isActive);
            }
        }

        public void ShowDialog(string npcName, string[] lines)
        {
            if (dialogPanel)
                dialogPanel.SetActive(true);
        }

        public void HideDialog()
        {
            if (dialogPanel)
                dialogPanel.SetActive(false);
        }

        public void ShowMessage(string message, float duration = 3f)
        {
            Debug.Log(message);
        }
    }
}
