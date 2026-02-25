using UnityEngine;

namespace Knowledge.Game
{
    public enum Gender { Male, Female, NonBinary }

    [RequireComponent(typeof(CharacterController))]
    public sealed class PlayerController : MonoBehaviour
    {
        #region Constants
        private const float DefaultMaxHealth = 100f;
        private const float DefaultMaxEnergy = 100f;
        private const float DefaultMaxHunger = 100f;
        private const float DefaultMaxThirst = 100f;
        private const float DefaultMaxHappiness = 100f;
        private const float BaseMoveSpeed = 5f;
        private const float SprintSpeed = 8f;
        private const float RotationSpeed = 10f;
        private const float Gravity = -9.81f;
        private const float HungerDrain = 0.5f;
        private const float ThirstDrain = 0.8f;
        private const float EnergyDrain = 0.2f;
        private const float HealthDrainOnDepletion = 2f;
        private const int MinSocialStatus = 1;
        private const int MaxSocialStatus = 100;
        private const float LevelUpMultiplier = 1.5f;
        #endregion

        [Header("Character Customization")]
        [SerializeField] private Gender gender = Gender.Male;
        [SerializeField] [Range(18, 100)] private int age = 25;
        [SerializeField] [Range(0.5f, 2f)] private float height = 1f;
        [SerializeField] [Range(0.5f, 2f)] private float bodyWidth = 1f;

        [Header("Movement")]
        [SerializeField] private float moveSpeed = BaseMoveSpeed;
        [SerializeField] private float sprintSpeed = SprintSpeed;
        [SerializeField] private float rotationSpeed = RotationSpeed;
        [SerializeField] private float gravity = Gravity;

        [Header("Stats")]
        [SerializeField] private float maxHealth = DefaultMaxHealth;
        [SerializeField] private float maxEnergy = DefaultMaxEnergy;
        [SerializeField] private float maxHunger = DefaultMaxHunger;
        [SerializeField] private float maxThirst = DefaultMaxThirst;
        [SerializeField] private float maxHappiness = DefaultMaxHappiness;

        [Header("Skill Attributes")]
        [SerializeField] private float strength = 10f;
        [SerializeField] private float dexterity = 10f;
        [SerializeField] private float intelligence = 10f;
        [SerializeField] private float charisma = 10f;
        [SerializeField] private float vitality = 10f;

        [Header("Social Status")]
        [SerializeField] [Range(MinSocialStatus, MaxSocialStatus)] private int socialStatus = 1;

        [Header("Experience")]
        [SerializeField] private int level = 1;
        [SerializeField] private float experience = 0f;
        [SerializeField] private float experienceToNextLevel = 100f;

        [Header("Inventory")]
        [SerializeField] private int inventoryCapacity = 50;
        [SerializeField] private float maxCarryWeight = 50f;

        private CharacterController controller;
        private Vector3 velocity;
        private bool isGrounded;
        private float currentHealth;
        private float currentEnergy;
        private float currentHunger;
        private float currentThirst;
        private float currentHappiness;

        #region Properties
        public float Health => currentHealth;
        public float Energy => currentEnergy;
        public float Hunger => currentHunger;
        public float Thirst => currentThirst;
        public float Happiness => currentHappiness;
        public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }
        public float SprintSpeed { get => sprintSpeed; set => sprintSpeed = value; }
        public float MaxHealth { get => maxHealth; set => maxHealth = value; }
        public float MaxEnergy { get => maxEnergy; set => maxEnergy = value; }
        public float MaxHunger { get => maxHunger; set => maxHunger = value; }
        public float MaxThirst { get => maxThirst; set => maxThirst = value; }
        public float MaxHappiness { get => maxHappiness; set => maxHappiness = value; }
        public int Level { get => level; set => level = value; }
        public int SocialStatus { get => socialStatus; set => socialStatus = Mathf.Clamp(value, MinSocialStatus, MaxSocialStatus); }
        public Gender Gender => gender;
        public int Age => age;
        #endregion

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            ResetStats();
            ApplyBodyDimensions();
        }

        public void ApplyBodyDimensions()
        {
            if (controller == null) return;
            
            controller.radius = 0.5f * bodyWidth;
            controller.height = 2f * height;
            transform.localScale = new Vector3(bodyWidth, height, bodyWidth);
        }

        public void UpdateCharacter(Gender newGender, int newAge, float newHeight, float newBodyWidth)
        {
            gender = newGender;
            age = newAge;
            height = newHeight;
            bodyWidth = newBodyWidth;
            ApplyBodyDimensions();
        }

        private void ResetStats()
        {
            currentHealth = maxHealth;
            currentEnergy = maxEnergy;
            currentHunger = maxHunger;
            currentThirst = maxThirst;
            currentHappiness = maxHappiness;
        }

        private void Update()
        {
            if (GameManager.Instance?.IsPaused == true) return;

            isGrounded = controller.isGrounded;
            if (isGrounded && velocity.y < 0)
                velocity.y = -2f;

            HandleMovement();
            HandleStats();
        }

        private void HandleMovement()
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            bool sprint = Input.GetKey(KeyCode.LeftShift);

            Vector3 move = transform.right * x + transform.forward * z;
            float currentSpeed = sprint ? sprintSpeed : moveSpeed;

            controller.Move(move * currentSpeed * Time.deltaTime);

            if (Input.GetKey(KeyCode.Q))
                transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
            if (Input.GetKey(KeyCode.E))
                transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }

        private void HandleStats()
        {
            float delta = Time.deltaTime;

            currentHunger -= delta * HungerDrain;
            currentThirst -= delta * ThirstDrain;
            currentEnergy -= delta * EnergyDrain;

            if (currentHunger <= 0 || currentThirst <= 0)
                currentHealth -= delta * HealthDrainOnDepletion;

            if (currentHealth <= 0)
                HandleDeath();

            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
            currentHunger = Mathf.Clamp(currentHunger, 0, maxHunger);
            currentThirst = Mathf.Clamp(currentThirst, 0, maxThirst);
            currentHappiness = Mathf.Clamp(currentHappiness, 0, maxHappiness);
        }

        private void HandleDeath()
        {
            Debug.Log("Player died! Respawning...");
            currentHealth = maxHealth;
            currentHunger = maxHunger;
            currentThirst = maxThirst;
            transform.position = Vector3.zero;
        }

        public void Eat(float amount) => currentHunger = Mathf.Min(currentHunger + amount, maxHunger);
        public void Drink(float amount) => currentThirst = Mathf.Min(currentThirst + amount, maxThirst);
        public void Rest(float amount) => currentEnergy = Mathf.Min(currentEnergy + amount, maxEnergy);

        public void AddExperience(float amount)
        {
            experience += amount;
            if (experience >= experienceToNextLevel)
                LevelUp();
        }

        private void LevelUp()
        {
            level++;
            experience -= experienceToNextLevel;
            experienceToNextLevel *= LevelUpMultiplier;
            Debug.Log($"Level up! Now level {level}");
        }

        public void ModifySocialStatus(int amount)
        {
            socialStatus = Mathf.Clamp(socialStatus + amount, MinSocialStatus, MaxSocialStatus);
        }
    }
}
