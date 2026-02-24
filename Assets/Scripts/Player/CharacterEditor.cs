using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Knowledge.Game
{
    public class CharacterEditor : MonoBehaviour
    {
        public static CharacterEditor Instance { get; private set; }

        [Header("Character Data")]
        public CharacterData characterData = new CharacterData();

        [Header("UI References")]
        public GameObject editorPanel;
        public TMP_InputField nameInput;
        public TMP_Dropdown sexDropdown;
        public TMP_Dropdown backgroundDropdown;
        public TMP_Dropdown bodyTypeDropdown;
        public TMP_Dropdown primaryTraitDropdown;
        public TMP_Dropdown secondaryTraitDropdown;

        [Header("Age Slider")]
        public Slider ageSlider;
        public TextMeshProUGUI ageValueText;

        [Header("Size Sliders")]
        public Slider heightSlider;
        public TextMeshProUGUI heightValueText;
        public Slider widthSlider;
        public TextMeshProUGUI widthValueText;
        public Slider massSlider;
        public TextMeshProUGUI massValueText;

        [Header("Color Pickers")]
        public Slider hairHueSlider;
        public Slider skinHueSlider;
        public Slider eyeHueSlider;
        public Image hairColorPreview;
        public Image skinColorPreview;
        public Image eyeColorPreview;

        [Header("Preview")]
        public TextMeshProUGUI previewNameText;
        public TextMeshProUGUI previewInfoText;
        public TextMeshProUGUI previewStatsText;
        public TextMeshProUGUI previewPersonalityText;

        [Header("Buttons")]
        public Button confirmButton;
        public Button cancelButton;
        public Button openEditorButton;
        public Button randomButton;
        public Button resetButton;

        [Header("Tab Navigation")]
        public GameObject basicTab;
        public GameObject bodyTab;
        public GameObject appearanceTab;
        public GameObject personalityTab;

        private bool isEditorOpen = false;

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
        }

        private void Start()
        {
            SetupDropdowns();
            SetupUI();
            SetupListeners();
            if (editorPanel != null)
                editorPanel.SetActive(false);
        }

        private void SetupDropdowns()
        {
            if (sexDropdown != null)
            {
                sexDropdown.ClearOptions();
                sexDropdown.AddOptions(new System.Collections.Generic.List<string> { "Male", "Female", "NonBinary" });
            }

            if (backgroundDropdown != null)
            {
                backgroundDropdown.ClearOptions();
                backgroundDropdown.AddOptions(new System.Collections.Generic.List<string>
                {
                    "None", "Warrior", "Scholar", "Merchant", "Farmer", "Hunter", "Noble", "Outcast"
                });
            }

            if (bodyTypeDropdown != null)
            {
                bodyTypeDropdown.ClearOptions();
                bodyTypeDropdown.AddOptions(new System.Collections.Generic.List<string>
                {
                    "Slim", "Average", "Athletic", "Heavy", "Muscular"
                });
            }

            if (primaryTraitDropdown != null)
            {
                primaryTraitDropdown.ClearOptions();
                primaryTraitDropdown.AddOptions(new System.Collections.Generic.List<string>
                {
                    "Brave", "Cautious", "Curious", "Greedy", "Kind", "Aggressive", "Peaceful", "Ambitious"
                });
            }

            if (secondaryTraitDropdown != null)
            {
                secondaryTraitDropdown.ClearOptions();
                secondaryTraitDropdown.AddOptions(new System.Collections.Generic.List<string>
                {
                    "Brave", "Cautious", "Curious", "Greedy", "Kind", "Aggressive", "Peaceful", "Ambitious"
                });
            }
        }

        private void SetupUI()
        {
            UpdateUIValues();
            UpdateColorPreviews();
            UpdatePreview();
        }

        private void SetupListeners()
        {
            if (confirmButton != null)
                confirmButton.onClick.AddListener(OnConfirm);
            if (cancelButton != null)
                cancelButton.onClick.AddListener(OnCancel);
            if (openEditorButton != null)
                openEditorButton.onClick.AddListener(ToggleEditor);
            if (randomButton != null)
                randomButton.onClick.AddListener(RandomizeCharacter);
            if (resetButton != null)
                resetButton.onClick.AddListener(ResetToDefaults);

            if (nameInput != null)
                nameInput.onValueChanged.AddListener(OnNameChanged);
            if (sexDropdown != null)
                sexDropdown.onValueChanged.AddListener(OnSexChanged);
            if (backgroundDropdown != null)
                backgroundDropdown.onValueChanged.AddListener(OnBackgroundChanged);
            if (bodyTypeDropdown != null)
                bodyTypeDropdown.onValueChanged.AddListener(OnBodyTypeChanged);
            if (primaryTraitDropdown != null)
                primaryTraitDropdown.onValueChanged.AddListener(OnPrimaryTraitChanged);
            if (secondaryTraitDropdown != null)
                secondaryTraitDropdown.onValueChanged.AddListener(OnSecondaryTraitChanged);

            if (ageSlider != null)
                ageSlider.onValueChanged.AddListener(OnAgeChanged);
            if (heightSlider != null)
                heightSlider.onValueChanged.AddListener(OnHeightChanged);
            if (widthSlider != null)
                widthSlider.onValueChanged.AddListener(OnWidthChanged);
            if (massSlider != null)
                massSlider.onValueChanged.AddListener(OnMassChanged);

            if (hairHueSlider != null)
                hairHueSlider.onValueChanged.AddListener(OnHairColorChanged);
            if (skinHueSlider != null)
                skinHueSlider.onValueChanged.AddListener(OnSkinColorChanged);
            if (eyeHueSlider != null)
                eyeHueSlider.onValueChanged.AddListener(OnEyeColorChanged);
        }

        public void ToggleEditor()
        {
            isEditorOpen = !isEditorOpen;
            if (editorPanel != null)
                editorPanel.SetActive(isEditorOpen);
            if (isEditorOpen)
            {
                UpdateUIValues();
                UpdateColorPreviews();
                UpdatePreview();
            }
        }

        public void ShowTab(int tabIndex)
        {
            if (basicTab) basicTab.SetActive(tabIndex == 0);
            if (bodyTab) bodyTab.SetActive(tabIndex == 1);
            if (appearanceTab) appearanceTab.SetActive(tabIndex == 2);
            if (personalityTab) personalityTab.SetActive(tabIndex == 3);
        }

        private void UpdateUIValues()
        {
            if (nameInput != null)
                nameInput.text = characterData.characterName;
            if (sexDropdown != null)
                sexDropdown.value = (int)characterData.sex;
            if (backgroundDropdown != null)
                backgroundDropdown.value = (int)characterData.background;
            if (bodyTypeDropdown != null)
                bodyTypeDropdown.value = (int)characterData.bodyType;
            if (primaryTraitDropdown != null)
                primaryTraitDropdown.value = (int)characterData.primaryTrait;
            if (secondaryTraitDropdown != null)
                secondaryTraitDropdown.value = (int)characterData.secondaryTrait;

            if (ageSlider != null)
            {
                ageSlider.minValue = 18;
                ageSlider.maxValue = 80;
                ageSlider.value = characterData.startingAge;
            }
            if (heightSlider != null)
            {
                heightSlider.minValue = 0.5f;
                heightSlider.maxValue = 2f;
                heightSlider.value = characterData.heightScale;
            }
            if (widthSlider != null)
            {
                widthSlider.minValue = 0.5f;
                widthSlider.maxValue = 2f;
                widthSlider.value = characterData.widthScale;
            }
            if (massSlider != null)
            {
                massSlider.minValue = 0.5f;
                massSlider.maxValue = 2f;
                massSlider.value = characterData.massScale;
            }

            if (hairHueSlider != null)
                hairHueSlider.value = characterData.hairColor.r;
            if (skinHueSlider != null)
                skinHueSlider.value = characterData.skinColor.r;
            if (eyeHueSlider != null)
                eyeHueSlider.value = characterData.eyeColor.r;

            UpdateValueTexts();
        }

        private void UpdateValueTexts()
        {
            if (ageValueText != null)
                ageValueText.text = $"{characterData.startingAge} years";
            if (heightValueText != null)
                heightValueText.text = $"{characterData.GetDisplayHeight():F2}m";
            if (widthValueText != null)
                widthValueText.text = $"{characterData.GetDisplayWidth():F2}m";
            if (massValueText != null)
                massValueText.text = $"{characterData.GetDisplayMass():F1f}kg";
        }

        private void UpdateColorPreviews()
        {
            if (hairColorPreview != null)
                hairColorPreview.color = characterData.hairColor;
            if (skinColorPreview != null)
                skinColorPreview.color = characterData.skinColor;
            if (eyeColorPreview != null)
                eyeColorPreview.color = characterData.eyeColor;
        }

        private void UpdatePreview()
        {
            if (previewNameText != null)
                previewNameText.text = characterData.characterName;

            if (previewInfoText != null)
            {
                previewInfoText.text = $"{characterData.sex}\n" +
                    $"Age: {characterData.startingAge}\n" +
                    $"Height: {characterData.GetDisplayHeight():F2}m\n" +
                    $"Weight: {characterData.GetDisplayMass():F1f}kg";
            }

            if (previewStatsText != null)
            {
                float bonus = characterData.GetStartingStatBonus();
                string bonusStr = bonus > 1f ? $"+{Mathf.RoundToInt((bonus - 1f) * 100)}%" : "Normal";
                previewStatsText.text = $"Background: {characterData.background}\n" +
                    $"Body Type: {characterData.bodyType}\n" +
                    $"Stat Bonus: {bonusStr}";
            }

            if (previewPersonalityText != null)
            {
                previewPersonalityText.text = $"Primary: {characterData.primaryTrait}\n" +
                    $"Secondary: {characterData.secondaryTrait}";
            }
        }

        private void OnNameChanged(string value)
        {
            characterData.characterName = value;
            UpdatePreview();
        }

        private void OnSexChanged(int value)
        {
            characterData.sex = (CharacterSex)value;
            UpdateValueTexts();
            UpdatePreview();
        }

        private void OnBackgroundChanged(int value)
        {
            characterData.background = (CharacterBackground)value;
            UpdatePreview();
        }

        private void OnBodyTypeChanged(int value)
        {
            characterData.bodyType = (BodyType)value;
            UpdateValueTexts();
            UpdatePreview();
        }

        private void OnPrimaryTraitChanged(int value)
        {
            characterData.primaryTrait = (PersonalityTrait)value;
            UpdatePreview();
        }

        private void OnSecondaryTraitChanged(int value)
        {
            characterData.secondaryTrait = (PersonalityTrait)value;
            UpdatePreview();
        }

        private void OnAgeChanged(float value)
        {
            characterData.startingAge = Mathf.RoundToInt(value);
            UpdateValueTexts();
            UpdatePreview();
        }

        private void OnHeightChanged(float value)
        {
            characterData.heightScale = value;
            UpdateValueTexts();
            UpdatePreview();
        }

        private void OnWidthChanged(float value)
        {
            characterData.widthScale = value;
            UpdateValueTexts();
            UpdatePreview();
        }

        private void OnMassChanged(float value)
        {
            characterData.massScale = value;
            UpdateValueTexts();
            UpdatePreview();
        }

        private void OnHairColorChanged(float value)
        {
            characterData.hairColor = Color.HSVToRGB(value, 0.6f, 0.4f);
            UpdateColorPreviews();
        }

        private void OnSkinColorChanged(float value)
        {
            characterData.skinColor = Color.HSVToRGB(value, 0.4f, 0.9f);
            UpdateColorPreviews();
        }

        private void OnEyeColorChanged(float value)
        {
            characterData.eyeColor = Color.HSVToRGB(value, 0.7f, 0.8f);
            UpdateColorPreviews();
        }

        public void RandomizeCharacter()
        {
            characterData.sex = (CharacterSex)Random.Range(0, 3);
            characterData.background = (CharacterBackground)Random.Range(0, 8);
            characterData.bodyType = (BodyType)Random.Range(0, 5);
            characterData.primaryTrait = (PersonalityTrait)Random.Range(0, 8);
            characterData.secondaryTrait = (PersonalityTrait)Random.Range(0, 8);
            characterData.startingAge = Random.Range(18, 65);
            characterData.heightScale = Random.Range(0.8f, 1.2f);
            characterData.widthScale = Random.Range(0.8f, 1.2f);
            characterData.massScale = Random.Range(0.8f, 1.2f);
            characterData.hairColor = Color.HSVToRGB(Random.value, 0.6f, 0.4f);
            characterData.skinColor = Color.HSVToRGB(Random.Range(0f, 0.15f), 0.4f, 0.9f);
            characterData.eyeColor = Color.HSVToRGB(Random.value, 0.7f, 0.8f);

            string[] maleNames = { "Marco", "Luca", "Giovanni", "Francesco", "Antonio", "Andrea" };
            string[] femaleNames = { "Maria", "Anna", "Giulia", "Sofia", "Francesca", "Elena" };
            string[] neutralNames = { "Alex", "Jordan", "Casey", "Riley", "Morgan", "Taylor" };

            if (characterData.sex == CharacterSex.Male)
                characterData.characterName = maleNames[Random.Range(0, maleNames.Length)];
            else if (characterData.sex == CharacterSex.Female)
                characterData.characterName = femaleNames[Random.Range(0, femaleNames.Length)];
            else
                characterData.characterName = neutralNames[Random.Range(0, neutralNames.Length)];

            UpdateUIValues();
            UpdateColorPreviews();
            UpdatePreview();
        }

        public void ResetToDefaults()
        {
            characterData = new CharacterData();
            UpdateUIValues();
            UpdateColorPreviews();
            UpdatePreview();
        }

        private void OnConfirm()
        {
            ApplyToPlayer();
            Debug.Log($"Character created: {characterData.characterName}");
            Debug.Log($"Sex: {characterData.sex}, Age: {characterData.startingAge}, Background: {characterData.background}");
            Debug.Log($"Body: {characterData.bodyType}, Height: {characterData.GetDisplayHeight():F2}m, Mass: {characterData.GetDisplayMass():F1f}kg");
            Debug.Log($"Personality: {characterData.primaryTrait}, {characterData.secondaryTrait}");
            ToggleEditor();
        }

        private void OnCancel()
        {
            UpdateUIValues();
            UpdateColorPreviews();
            UpdatePreview();
            ToggleEditor();
        }

        public void ApplyToPlayer()
        {
            var player = GameManager.Instance?.Player;
            if (player == null) return;

            Transform playerTransform = player.transform;
            playerTransform.localScale = new Vector3(
                characterData.GetDisplayWidth(),
                characterData.GetDisplayHeight(),
                characterData.GetDisplayWidth()
            );
        }

        public CharacterData GetCharacterData() => characterData;

        public void SetCharacterData(CharacterData data)
        {
            characterData = data;
            UpdateUIValues();
            UpdateColorPreviews();
            UpdatePreview();
        }
    }
}
