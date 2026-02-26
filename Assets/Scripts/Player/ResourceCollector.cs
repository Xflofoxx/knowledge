using UnityEngine;

namespace Knowledge.Game
{
    public enum ResourceType
    {
        Wood,
        Stone,
        Herb,
        Berry,
        Water,
        Fish,
        Meat,
        Bone,
        Hide,
        MetalOre,
        Coal,
        Fiber,
        Mushroom,
        Fruit
    }

    public class InteractableObject : MonoBehaviour
    {
        [Header("Interaction Settings")]
        [SerializeField] private string objectName = "Unknown";
        [SerializeField] private string description = "An object";
        [SerializeField] private ResourceType resourceType;
        [SerializeField] private int resourceAmount = 1;
        [SerializeField] private float respawnTime = 30f;
        [SerializeField] private bool infinite = true;

        [Header("Visual")]
        [SerializeField] private Color highlightColor = Color.yellow;
        [SerializeField] private float highlightIntensity = 0.3f;

        private bool isDepleted;
        private float depletionTimer;
        private Renderer objectRenderer;
        private Color originalColor;

        public string ObjectName => objectName;
        public string Description => description;
        public ResourceType ResourceType => resourceType;
        public int ResourceAmount => resourceAmount;
        public bool IsDepleted => isDepleted;

        private void Start()
        {
            objectRenderer = GetComponent<Renderer>();
            if (objectRenderer != null)
                originalColor = objectRenderer.material.color;
        }

        private void Update()
        {
            if (isDepleted && !infinite)
            {
                depletionTimer += Time.deltaTime;
                if (depletionTimer >= respawnTime)
                {
                    Respawn();
                }
            }
        }

        public bool CanInteract()
        {
            return !isDepleted;
        }

        public void Interact()
        {
            if (!CanInteract()) return;

            if (!infinite)
            {
                isDepleted = true;
                depletionTimer = 0f;
                UpdateVisuals();
            }

            Debug.Log($"Collected {resourceAmount} {resourceType}");
            GameManager.Instance?.AddKnowledgePoints(5);
        }

        private void Respawn()
        {
            isDepleted = false;
            UpdateVisuals();
            Debug.Log($"{objectName} respawned");
        }

        private void UpdateVisuals()
        {
            if (objectRenderer == null) return;

            if (isDepleted)
                objectRenderer.material.color = Color.gray;
            else
                objectRenderer.material.color = originalColor;
        }

        public void Highlight(bool active)
        {
            if (objectRenderer == null) return;

            if (active)
                objectRenderer.material.color = originalColor + (highlightColor * highlightIntensity);
            else
                objectRenderer.material.color = originalColor;
        }
    }

    public sealed class ResourceCollector : MonoBehaviour
    {
        public static ResourceCollector Instance { get; private set; }

        [Header("Settings")]
        [SerializeField] private float interactionRange = 2.5f;
        [SerializeField] private LayerMask interactableLayer;
        [SerializeField] private KeyCode interactKey = KeyCode.E;
        [SerializeField] private KeyCode pickupKey = KeyCode.F;

        [Header("Gathering")]
        [SerializeField] private float gatherTime = 1f;
        [SerializeField] private bool autoPickup = true;

        private Camera playerCamera;
        private InteractableObject currentTarget;

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
            playerCamera = Camera.main;
        }

        private void Update()
        {
            if (GameManager.Instance?.IsPaused == true) return;

            FindTarget();

            if (Input.GetKeyDown(interactKey) || Input.GetKeyDown(pickupKey))
            {
                TryInteract();
            }
        }

        private void FindTarget()
        {
            if (playerCamera == null) return;

            Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, interactionRange, interactableLayer))
            {
                var interactable = hit.collider.GetComponent<InteractableObject>();
                if (interactable != null)
                {
                    if (currentTarget != interactable)
                    {
                        if (currentTarget != null)
                            currentTarget.Highlight(false);

                        currentTarget = interactable;
                        currentTarget.Highlight(true);
                        ShowInteractionPrompt();
                    }
                    return;
                }
            }

            ClearTarget();
        }

        private void ClearTarget()
        {
            if (currentTarget != null)
            {
                currentTarget.Highlight(false);
                currentTarget = null;
                HideInteractionPrompt();
            }
        }

        private void TryInteract()
        {
            if (currentTarget == null || !currentTarget.CanInteract()) return;

            currentTarget.Interact();

            var item = ConvertResourceToItem(currentTarget.ResourceType);
            if (item != null)
            {
                InventorySystem.Instance?.AddItem(item, currentTarget.ResourceAmount);
            }
        }

        private ItemData ConvertResourceToItem(ResourceType type)
        {
            return type switch
            {
                ResourceType.Wood => InventorySystem.CreateResource("Legno", 1),
                ResourceType.Stone => InventorySystem.CreateResource("Pietra", 1),
                ResourceType.Herb => InventorySystem.CreateResource("Erba", 2),
                ResourceType.Berry => InventorySystem.CreateResource("Bacche", 3),
                ResourceType.Water => InventorySystem.CreateResource("Acqua", 2),
                ResourceType.Fish => InventorySystem.CreateResource("Pesce", 5),
                ResourceType.Meat => InventorySystem.CreateResource("Carne", 5),
                ResourceType.Bone => InventorySystem.CreateResource("Osso", 3),
                ResourceType.Hide => InventorySystem.CreateResource("Pelle", 4),
                ResourceType.MetalOre => InventorySystem.CreateResource("Minerale", 6),
                ResourceType.Coal => InventorySystem.CreateResource("Carbone", 4),
                ResourceType.Fiber => InventorySystem.CreateResource("Fibra", 2),
                ResourceType.Mushroom => InventorySystem.CreateResource("Fungo", 3),
                ResourceType.Fruit => InventorySystem.CreateResource("Frutta", 3),
                _ => null
            };
        }

        private void ShowInteractionPrompt()
        {
            if (currentTarget != null)
            {
                Debug.Log($"[E] Raccogli {currentTarget.ObjectName}");
            }
        }

        private void HideInteractionPrompt()
        {
        }
    }
}
