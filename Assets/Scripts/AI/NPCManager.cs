using System.Collections.Generic;
using UnityEngine;

namespace Knowledge.Game
{
    public enum NPCFaction { Tribe, Village, City, Kingdom, Empire }

    [System.Serializable]
    public class NPCRelationship
    {
        public string npcName;
        public int reputation = 0;
        public bool isFriend = false;
        public bool isEnemy = false;
    }

    public class NPCManager : MonoBehaviour
    {
        public static NPCManager Instance { get; private set; }

        [Header("NPC Registry")]
        public List<NPCData> allNPCs = new();
        public Dictionary<NPCFaction, int> factionReputations = new();

        [Header("Interaction Settings")]
        public float interactionRange = 3f;
        public LayerMask npcLayer;

        private Dictionary<string, NPCRelationship> relationships = new();

        private void Awake()
        {
            if (Instance != null) return;
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
            allNPCs.Add(npc);
        }

        public void InteractWithNPC(string npcName)
        {
            if (!relationships.ContainsKey(npcName))
            {
                relationships[npcName] = new NPCRelationship { npcName = npcName };
            }

            var npc = allNPCs.Find(n => n.name == npcName);
            if (npc != null)
            {
                npc.OnInteract?.Invoke();
            }
        }

        public void ModifyReputation(string npcName, int amount)
        {
            if (!relationships.ContainsKey(npcName))
            {
                relationships[npcName] = new NPCRelationship { npcName = npcName };
            }

            relationships[npcName].reputation = Mathf.Clamp(
                relationships[npcName].reputation + amount, -100, 100);

            var rel = relationships[npcName];
            rel.isFriend = rel.reputation > 50;
            rel.isEnemy = rel.reputation < -50;

            Debug.Log($"{npcName} reputation: {relationships[npcName].reputation}");
        }

        public void ModifyFactionReputation(NPCFaction faction, int amount)
        {
            factionReputations[faction] = Mathf.Clamp(
                factionReputations[faction] + amount, 0, 100);
        }

        public int GetReputation(string npcName)
        {
            return relationships.ContainsKey(npcName) ? relationships[npcName].reputation : 0;
        }

        public int GetFactionReputation(NPCFaction faction)
        {
            return factionReputations.ContainsKey(faction) ? factionReputations[faction] : 50;
        }

        public bool IsFriend(string npcName)
        {
            return relationships.ContainsKey(npcName) && relationships[npcName].isFriend;
        }

        public List<NPCData> GetNPCsInRange(Vector3 position)
        {
            var npcsInRange = new List<NPCData>();
            foreach (var npc in allNPCs)
            {
                if (Vector3.Distance(position, npc.position) <= interactionRange)
                {
                    npcsInRange.Add(npc);
                }
            }
            return npcsInRange;
        }

        public void CompleteQuest(string questName)
        {
            ModifySocialStatus(5);
            GameManager.Instance.AddKnowledgePoints(20);
        }

        private void ModifySocialStatus(int amount)
        {
            var player = GameManager.Instance.Player;
            if (player != null)
            {
                player.ModifySocialStatus(amount);
            }
        }
    }

    [System.Serializable]
    public class NPCData
    {
        public string name;
        public Vector3 position;
        public NPCFaction faction;
        public string dialogueTree;
        public bool hasQuest;
        public string questName;
        public UnityEngine.Events.UnityAction OnInteract;
    }
}
