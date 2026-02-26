using System;
using System.Collections.Generic;
using UnityEngine;

namespace Knowledge.Game
{
    [Serializable]
    public enum ItemCategory
    {
        Resource,
        Tool,
        Weapon,
        Armor,
        Consumable,
        Material,
        QuestItem,
        KeyItem
    }

    [Serializable]
    public class ItemData
    {
        public string id;
        public string displayName;
        public string description;
        public ItemCategory category;
        public int maxStack = 99;
        public float weight = 1f;
        public int value = 1;
        public Sprite icon;
        public bool is consumable;
        public float healthRestore;
        public float energyRestore;
        public float hungerRestore;
        public float thirstRestore;

        public ItemData()
        {
            id = Guid.NewGuid().ToString();
        }

        public ItemData(string name, ItemCategory cat, int stack = 99) : this()
        {
            displayName = name;
            category = cat;
            maxStack = stack;
        }
    }

    [Serializable]
    public class InventorySlot
    {
        public ItemData item;
        public int quantity;

        public bool IsEmpty => item == null || quantity <= 0;
        public bool CanAdd(int amount) => item == null || quantity + amount <= item.maxStack;

        public void Add(int amount)
        {
            quantity += amount;
        }

        public bool Remove(int amount)
        {
            if (quantity < amount) return false;
            quantity -= amount;
            if (quantity <= 0)
            {
                item = null;
                quantity = 0;
            }
            return true;
        }

        public void Clear()
        {
            item = null;
            quantity = 0;
        }
    }

    public sealed class InventorySystem : MonoBehaviour
    {
        public static InventorySystem Instance { get; private set; }

        [Header("Inventory Settings")]
        [SerializeField] private int inventorySize = 50;
        [SerializeField] private float maxWeight = 100f;

        [Header("Starting Items")]
        [SerializeField] private List<ItemData> startingItems = new();

        private InventorySlot[] slots;
        private float currentWeight;

        public int Size => inventorySize;
        public float CurrentWeight => currentWeight;
        public float MaxWeight => maxWeight;
        public InventorySlot[] Slots => slots;

        public event Action OnInventoryChanged;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            InitializeInventory();
        }

        private void InitializeInventory()
        {
            slots = new InventorySlot[inventorySize];
            for (int i = 0; i < inventorySize; i++)
            {
                slots[i] = new InventorySlot();
            }

            foreach (var item in startingItems)
            {
                AddItem(item, 1);
            }
        }

        public bool AddItem(ItemData item, int amount = 1)
        {
            if (item == null || amount <= 0) return false;

            if (currentWeight + (item.weight * amount) > maxWeight)
            {
                Debug.Log("Inventory full!");
                return false;
            }

            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null && slots[i].item.id == item.id && slots[i].CanAdd(amount))
                {
                    slots[i].Add(amount);
                    currentWeight += item.weight * amount;
                    OnInventoryChanged?.Invoke();
                    return true;
                }
            }

            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].IsEmpty)
                {
                    slots[i].item = item;
                    slots[i].Add(amount);
                    currentWeight += item.weight * amount;
                    OnInventoryChanged?.Invoke();
                    return true;
                }
            }

            Debug.Log("Inventory full!");
            return false;
        }

        public bool RemoveItem(ItemData item, int amount = 1)
        {
            if (item == null || amount <= 0) return false;

            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null && slots[i].item.id == item.id)
                {
                    if (slots[i].Remove(amount))
                    {
                        currentWeight -= item.weight * amount;
                        currentWeight = Mathf.Max(0, currentWeight);
                        OnInventoryChanged?.Invoke();
                        return true;
                    }
                }
            }

            return false;
        }

        public bool HasItem(string itemId, int amount = 1)
        {
            int total = 0;
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null && slots[i].item.id == itemId)
                {
                    total += slots[i].quantity;
                }
            }
            return total >= amount;
        }

        public InventorySlot FindSlot(ItemData item)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].item != null && slots[i].item.id == item.id)
                {
                    return slots[i];
                }
            }
            return null;
        }

        public List<InventorySlot> GetAllItems()
        {
            var items = new List<InventorySlot>();
            foreach (var slot in slots)
            {
                if (!slot.IsEmpty)
                    items.Add(slot);
            }
            return items;
        }

        public void Clear()
        {
            foreach (var slot in slots)
            {
                slot.Clear();
            }
            currentWeight = 0;
            OnInventoryChanged?.Invoke();
        }

        public bool UseItem(int slotIndex)
        {
            if (slotIndex < 0 || slotIndex >= slots.Length) return false;

            var slot = slots[slotIndex];
            if (slot.IsEmpty || !slot.item.isConsumable) return false;

            var player = GameManager.Instance?.Player;
            if (player == null) return false;

            if (slot.item.healthRestore > 0)
                player?.ModifyHealth(slot.item.healthRestore);
            if (slot.item.energyRestore > 0)
                player?.ModifyEnergy(slot.item.energyRestore);
            if (slot.item.hungerRestore > 0)
                player?.ModifyHunger(slot.item.hungerRestore);
            if (slot.item.thirstRestore > 0)
                player?.ModifyThirst(slot.item.thirstRestore);

            RemoveItem(slot.item, 1);
            return true;
        }

        public static ItemData CreateResource(string name, int value = 1)
        {
            return new ItemData(name, ItemCategory.Resource) { weight = 1f, value = value };
        }
    }
}
