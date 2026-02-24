using UnityEngine;
using UnityEngine.UI;

namespace Knowledge.Game
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement")]
        public float moveSpeed = 5f;
        public float sprintSpeed = 8f;
        public float rotationSpeed = 10f;
        public float gravity = -9.81f;

        [Header("Stats")]
        public float maxHealth = 100f;
        public float maxEnergy = 100f;
        public float maxHunger = 100f;
        public float maxThirst = 100f;
        public float maxHappiness = 100f;

        [Header("Skill Attributes")]
        public float strength = 10f;
        public float dexterity = 10f;
        public float intelligence = 10f;
        public float charisma = 10f;
        public float vitality = 10f;

        [Header("Social Status")]
        [Range(1, 100)] public int socialStatus = 1;

        [Header("Experience")]
        public int level = 1;
        public float experience = 0f;
        public float experienceToNextLevel = 100f;

        [Header("Inventory")]
        public int inventoryCapacity = 50;
        public float maxCarryWeight = 50f;

        private CharacterController controller;
        private Vector3 velocity;
        private bool isGrounded;
        private float currentHealth;
        private float currentEnergy;
        private float currentHunger;
        private float currentThirst;
        private float currentHappiness;

        public float Health => currentHealth;
        public float Energy => currentEnergy;
        public float Hunger => currentHunger;
        public float Thirst => currentThirst;
        public float Happiness => currentHappiness;

        private void Awake()
        {
            controller = GetComponent<CharacterController>();
            ResetStats();
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
            if (GameManager.Instance.gamePaused) return;

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
            float speed = sprint ? sprintSpeed : moveSpeed;

            controller.Move(move * speed * Time.deltaTime);

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

            currentHunger -= delta * 0.5f;
            currentThirst -= delta * 0.8f;
            currentEnergy -= delta * 0.2f;

            if (currentHunger <= 0 || currentThirst <= 0)
                currentHealth -= delta * 2f;

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

        public void Eat(float amount)
        {
            currentHunger = Mathf.Min(currentHunger + amount, maxHunger);
        }

        public void Drink(float amount)
        {
            currentThirst = Mathf.Min(currentThirst + amount, maxThirst);
        }

        public void Rest(float amount)
        {
            currentEnergy = Mathf.Min(currentEnergy + amount, maxEnergy);
        }

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
            experienceToNextLevel *= 1.5f;
            Debug.Log($"Level up! Now level {level}");
        }

        public void ModifySocialStatus(int amount)
        {
            socialStatus = Mathf.Clamp(socialStatus + amount, 1, 100);
        }
    }
}
