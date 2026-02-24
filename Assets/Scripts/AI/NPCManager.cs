using System.Collections.Generic;
using UnityEngine;

namespace Knowledge.Game
{
    public enum NPCFaction { Tribe, Village, City, Kingdom, Empire }

    [System.Serializable]
    public class NPCRelationship
    {
        public string npcName = string.Empty;
        public int reputation;
        public bool isFriend;
        public bool isEnemy;
    }

    public sealed class NPCManager : MonoBehaviour
    {
        public static NPCManager Instance { get; private set; }

        [Header("NPC Registry")]
        [SerializeField] private List<NPCData> allNPCs = new();
        [SerializeField] private Dictionary<NPCFaction, int> factionReputations = new();

        [Header("Interaction Settings")]
        [SerializeField] private float interactionRange = 3f;
        [SerializeField] private LayerMask npcLayer;

        private readonly Dictionary<string, NPCRelationship> relationships = new();

        public IReadOnlyList<NPCData> NPCs => allNPCs;
        public float InteractionRange => interactionRange;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            InitializeFactions();
        }

        private void InitializeFactions()
        {
            foreach (NPCFaction faction in System.Enum.GetValues(typeof(NPCFaction)))
            {
                factionReputations[faction] = 50;
            }
        }

        public void AddNPC(NPCData npc)
        {
            if (npc != null && !string.IsNullOrEmpty(npc.name))
                allNPCs.Add(npc);
        }

        public void InteractWithNPC(string npcName)
        {
            if (string.IsNullOrEmpty(npcName)) return;

            if (!relationships.ContainsKey(npcName))
                relationships[npcName] = new NPCRelationship { npcName = npcName };

            var npc = allNPCs.Find(n => n.name == npcName);
            npc?.OnInteract?.Invoke();
        }

        public void ModifyReputation(string npcName, int amount)
        {
            if (string.IsNullOrEmpty(npcName)) return;

            if (!relationships.ContainsKey(npcName))
                relationships[npcName] = new NPCRelationship { npcName = npcName };

            var rel = relationships[npcName];
            rel.reputation = Mathf.Clamp(rel.reputation + amount, -100, 100);
            rel.isFriend = rel.reputation > 50;
            rel.isEnemy = rel.reputation < -50;

            Debug.Log($"{npcName} reputation: {rel.reputation}");
        }

        public void ModifyFactionReputation(NPCFaction faction, int amount)
        {
            if (factionReputations.TryGetValue(faction, out int current))
                factionReputations[faction] = Mathf.Clamp(current + amount, 0, 100);
        }

        public int GetReputation(string npcName)
        {
            return relationships.TryGetValue(npcName, out var rel) ? rel.reputation : 0;
        }

        public int GetFactionReputation(NPCFaction faction)
        {
            return factionReputations.TryGetValue(faction, out int rep) ? rep : 50;
        }

        public bool IsFriend(string npcName)
        {
            return relationships.TryGetValue(npcName, out var rel) && rel.isFriend;
        }

        public List<NPCData> GetNPCsInRange(Vector3 position)
        {
            var npcsInRange = new List<NPCData>();
            foreach (var npc in allNPCs)
            {
                if (Vector3.Distance(position, npc.position) <= interactionRange)
                    npcsInRange.Add(npc);
            }
            return npcsInRange;
        }

        public void CompleteQuest(string questName)
        {
            GameManager.Instance?.Player?.ModifySocialStatus(5);
            GameManager.Instance?.AddKnowledgePoints(20);
        }
    }

    [System.Serializable]
    public class NPCData
    {
        public string name = string.Empty;
        public Vector3 position;
        public NPCFaction faction;
        public string dialogueTree = string.Empty;
        public bool hasQuest;
        public string questName = string.Empty;
        public UnityEngine.Events.UnityAction OnInteract;
    }
}
