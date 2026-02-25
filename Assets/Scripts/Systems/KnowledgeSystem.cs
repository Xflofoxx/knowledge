using System.Collections.Generic;
using UnityEngine;

namespace Knowledge.Game
{
    public sealed class KnowledgeSystem : MonoBehaviour
    {
        public static KnowledgeSystem Instance { get; private set; }

        [Header("Knowledge Settings")]
        [SerializeField] private int baseKnowledgeGain = 10;
        [SerializeField] private float knowledgeMultiplier = 1f;

        [Header("Knowledge Categories")]
        [SerializeField] private int natureKnowledge;
        [SerializeField] private int technologyKnowledge;
        [SerializeField] private int socialKnowledge;
        [SerializeField] private int combatKnowledge;
        public int NatureKnowledgeField { get => natureKnowledge; set => natureKnowledge = value; }
        public int TechnologyKnowledgeField { get => technologyKnowledge; set => technologyKnowledge = value; }
        public int SocialKnowledgeField { get => socialKnowledge; set => socialKnowledge = value; }
        public int CombatKnowledgeField { get => combatKnowledge; set => combatKnowledge = value; }

        public int TotalKnowledge => natureKnowledge + technologyKnowledge + socialKnowledge + combatKnowledge;
        public int NatureKnowledge => natureKnowledge;
        public int TechnologyKnowledge => technologyKnowledge;
        public int SocialKnowledge => socialKnowledge;
        public int CombatKnowledge => combatKnowledge;

        private readonly Dictionary<string, float> observationKnowledge = new();

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
        }

        public void OnKnowledgeGained(int amount)
        {
            if (amount <= 0) return;
            
            int categorizedAmount = Mathf.RoundToInt(amount * knowledgeMultiplier);
            GameManager.Instance?.AddKnowledgePoints(categorizedAmount);
        }

        public void GainNatureKnowledge(int amount)
        {
            if (amount <= 0) return;
            natureKnowledge += amount;
            OnKnowledgeGained(amount);
        }

        public void GainTechnologyKnowledge(int amount)
        {
            if (amount <= 0) return;
            technologyKnowledge += amount;
            OnKnowledgeGained(amount);
        }

        public void GainSocialKnowledge(int amount)
        {
            if (amount <= 0) return;
            socialKnowledge += amount;
            OnKnowledgeGained(amount);
        }

        public void GainCombatKnowledge(int amount)
        {
            if (amount <= 0) return;
            combatKnowledge += amount;
            OnKnowledgeGained(amount);
        }

        public void StudyAnimal(string animalName, float duration)
        {
            if (string.IsNullOrEmpty(animalName) || duration <= 0) return;

            if (!observationKnowledge.ContainsKey(animalName))
                observationKnowledge[animalName] = 0;

            observationKnowledge[animalName] += duration;

            if (observationKnowledge[animalName] >= 10f)
            {
                GainNatureKnowledge(5);
                observationKnowledge[animalName] = 0;
                Debug.Log($"Studied {animalName}! +5 Nature Knowledge");
            }
        }

        public void DiscoverPhenomenon(string phenomenonName)
        {
            if (string.IsNullOrEmpty(phenomenonName)) return;
            
            GainNatureKnowledge(10);
            Debug.Log($"Discovered: {phenomenonName}! +10 Nature Knowledge");
        }

        public bool HasEnoughKnowledge(int amount) => amount <= 0 || TotalKnowledge >= amount;

        public string GetKnowledgeBreakdown() => 
            $"Nature: {natureKnowledge} | Tech: {technologyKnowledge} | Social: {socialKnowledge} | Combat: {combatKnowledge}";
    }
}
