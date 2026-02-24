using UnityEngine;
using UnityEngine.UI;

namespace Knowledge.Game
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        [Header("HUD Elements")]
        public Slider healthBar;
        public Slider energyBar;
        public Slider hungerBar;
        public Slider thirstBar;
        public Text knowledgePointsText;
        public Text levelText;
        public Text eraText;

        [Header("Panels")]
        public GameObject inventoryPanel;
        public GameObject craftingPanel;
        public GameObject pauseMenu;
        public GameObject dialogPanel;
        public GameObject mapPanel;

        [Header("Info Display")]
        public Text weatherText;
        public Text locationText;
        public Text timeText;

        private bool inventoryOpen = false;
        private bool craftingOpen = false;
        private bool mapOpen = false;

        private void Awake()
        {
            if (Instance != null) return;
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
            if (Input.GetKeyDown(KeyCode.C))
                ToggleCrafting();
            if (Input.GetKeyDown(KeyCode.M))
                ToggleMap();
            if (Input.GetKeyDown(KeyCode.Escape))
                TogglePause();

            UpdateHUD();
        }

        public void UpdateHUD()
        {
            var player = GameManager.Instance.Player;
            if (player == null) return;

            if (healthBar) healthBar.value = player.Health / player.maxHealth;
            if (energyBar) energyBar.value = player.Energy / player.maxEnergy;
            if (hungerBar) hungerBar.value = player.Hunger / player.maxHunger;
            if (thirstBar) thirstBar.value = player.Thirst / player.maxThirst;

            if (knowledgePointsText)
                knowledgePointsText.text = $"KP: {GameManager.Instance.TotalKnowledgePoints}";
            
            if (levelText)
                levelText.text = $"LVL: {player.level}";

            if (eraText)
            {
                string[] eras = { "Pietra", "Bronzo", "Ferro", "Medioevo", "Rinascimento", "Industriale", "Moderna", "Spazio" };
                eraText.text = $"Era: {eras[GameManager.Instance.currentEra]}";
            }

            if (weatherText && WeatherSystem.Instance != null)
                weatherText.text = WeatherSystem.Instance.GetWeatherDescription();

            if (timeText)
            {
                float totalSeconds = GameManager.Instance.gameTime;
                int hours = (int)(totalSeconds / 3600) % 24;
                int minutes = (int)(totalSeconds / 60) % 60;
                timeText.text = $"{hours:D2}:{minutes:D2}";
            }
        }

        public void UpdateKnowledgeDisplay(int kp)
        {
            if (knowledgePointsText)
                knowledgePointsText.text = $"KP: {kp}";
        }

        public void UpdateAllDisplay()
        {
            UpdateHUD();
        }

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
                pauseMenu.SetActive(!pauseMenu.activeSelf);
                GameManager.Instance.gamePaused = pauseMenu.activeSelf;
            }
        }

        public void ShowDialog(string npcName, string[] lines)
        {
            if (dialogPanel)
            {
                dialogPanel.SetActive(true);
            }
        }

        public void HideDialog()
        {
            if (dialogPanel)
            {
                dialogPanel.SetActive(false);
            }
        }

        public void ShowMessage(string message, float duration = 3f)
        {
            Debug.Log(message);
        }
    }
}
