using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Knowledge.Game
{
    [System.Serializable]
    public class EraInfo
    {
        public string eraName;
        public string displayName;
        public string description;
        public int requiredKnowledge;
        public bool isUnlocked;
        public bool isCompleted;
        public Color eraColor = Color.white;
        public Sprite eraIcon;
    }

    public sealed class WorldMapManager : MonoBehaviour
    {
        public static WorldMapManager Instance { get; private set; }

        [Header("Era Data")]
        [SerializeField] private List<EraInfo> eras = new();

        [Header("UI References")]
        [SerializeField] private Transform eraButtonsContainer;
        [SerializeField] private GameObject eraButtonPrefab;
        [SerializeField] private Text eraDetailsText;
        [SerializeField] private Image selectedEraIcon;
        [SerializeField] private Text selectedEraName;
        [SerializeField] private Text selectedEraDescription;

        [Header("Selection")]
        [SerializeField] private int selectedEraIndex;

        public int SelectedEraIndex => selectedEraIndex;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            InitializeEras();
        }

        private void Start()
        {
            UpdateEraUnlockStatus();
            CreateEraButtons();
            SelectEra(0);
        }

        private void InitializeEras()
        {
            eras = new List<EraInfo>
            {
                new() { eraName = "StoneAge", displayName = "Età della Pietra", description = "Impara a sopravvivere nella preistoria. Raccogli risorse, scopri il fuoco e costruisci i tuoi primi strumenti.", requiredKnowledge = 0, isUnlocked = true, eraColor = new Color(0.6f, 0.4f, 0.2f) },
                new() { eraName = "BronzeAge", displayName = "Età del Bronzo", description = "Sviluppa la metallurgia. Scopri come fondere il bronzo e crea armi e attrezzi più avanzati.", requiredKnowledge = 50, eraColor = new Color(0.8f, 0.5f, 0.2f) },
                new() { eraName = "IronAge", displayName = "Età del Ferro", description = "L'era dei grandi regni. Costruisci città, commercia con altre civiltà e espandi il tuo impero.", requiredKnowledge = 150, eraColor = new Color(0.5f, 0.5f, 0.55f) },
                new() { eraName = "Medieval", displayName = "Medioevo", description = "L'epoca dei cavalieri e dei castelli. Costruisci fortezze, alleva bestiame e sviluppa nuove tecnologie.", requiredKnowledge = 300, eraColor = new Color(0.4f, 0.3f, 0.3f) },
                new() { eraName = "Renaissance", displayName = "Rinascimento", description = "L'era dell'arte e della scienza. Esplora il mondo, studia la natura e scopri nuove scoperte.", requiredKnowledge = 500, eraColor = new Color(0.3f, 0.4f, 0.6f) },
                new() { eraName = "Industrial", displayName = "Era Industriale", description = "La rivoluzione delle macchine. Costruisci fabbriche, treni e trasforma il mondo con la tecnologia.", requiredKnowledge = 800, eraColor = new Color(0.4f, 0.35f, 0.3f) },
                new() { eraName = "Modern", displayName = "Era Moderna", description = "L'età dell'elettricità e dell'elettronica. Sviluppa computer, internet e prepara l'umanità allo spazio.", requiredKnowledge = 1200, eraColor = new Color(0.5f, 0.6f, 0.7f) },
                new() { eraName = "Space", displayName = "Era Spaziale", description = "L'ultima frontiera. Costruisci razzi, colonizza altri pianeti e porta l'umanità tra le stelle.", requiredKnowledge = 2000, eraColor = new Color(0.2f, 0.2f, 0.4f) }
            };
        }

        private void UpdateEraUnlockStatus()
        {
            int currentKnowledge = GameManager.Instance?.TotalKnowledgePoints ?? 0;

            for (int i = 0; i < eras.Count; i++)
            {
                eras[i].isUnlocked = currentKnowledge >= eras[i].requiredKnowledge;

                if (i > 0)
                    eras[i].isUnlocked = eras[i].isUnlocked && eras[i - 1].isUnlocked;
            }
        }

        private void CreateEraButtons()
        {
            if (eraButtonsContainer == null || eraButtonPrefab == null) return;

            for (int i = 0; i < eras.Count; i++)
            {
                var era = eras[i];
                var button = Instantiate(eraButtonPrefab, eraButtonsContainer);
                button.name = $"EraButton_{i}";

                var buttonText = button.GetComponentInChildren<Text>();
                if (buttonText != null)
                    buttonText.text = era.displayName;

                var buttonImage = button.GetComponent<Image>();
                if (buttonImage != null)
                    buttonImage.color = era.eraColor;

                int index = i;
                button.GetComponent<Button>().onClick.AddListener(() => SelectEra(index));
            }
        }

        public void SelectEra(int index)
        {
            if (index < 0 || index >= eras.Count) return;

            selectedEraIndex = index;
            var era = eras[index];

            if (selectedEraName != null)
                selectedEraName.text = era.displayName;

            if (selectedEraDescription != null)
            {
                string status = era.isUnlocked ? (era.isCompleted ? "✓ Completata" : "Sbloccata") : $"Richiede {era.requiredKnowledge} KP";
                selectedEraDescription.text = $"{era.description}\n\nStato: {status}";
            }

            if (selectedEraIcon != null && era.eraIcon != null)
                selectedEraIcon.sprite = era.eraIcon;
        }

        public void TravelToSelectedEra()
        {
            var era = eras[selectedEraIndex];

            if (!era.isUnlocked)
            {
                Debug.LogWarning($"Era {era.displayName} is locked! Need {era.requiredKnowledge} KP");
                return;
            }

            var loader = FindObjectOfType<SceneLoader>();
            if (loader != null)
            {
                if (era.eraName == "StoneAge")
                    loader.LoadStoneAge();
                else
                    Debug.Log($"Loading {era.eraName}...");
            }
        }

        public bool IsEraUnlocked(int index)
        {
            if (index < 0 || index >= eras.Count) return false;
            return eras[index].isUnlocked;
        }

        public EraInfo GetEraInfo(int index)
        {
            if (index < 0 || index >= eras.Count) return null;
            return eras[index];
        }

        public void RefreshMap()
        {
            UpdateEraUnlockStatus();
        }
    }
}
