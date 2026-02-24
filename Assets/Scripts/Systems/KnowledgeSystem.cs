using UnityEngine;
using System.Collections.Generic;

namespace Knowledge.Game
{
    public class KnowledgeSystem : MonoBehaviour
    {
        public static KnowledgeSystem Instance { get; private set; }

        [Header("Knowledge Settings")]
        public int baseKnowledgeGain = 10;
        public float knowledgeMultiplier = 1f;

        [Header("Knowledge Categories")]
        public int natureKnowledge = 0;
        public int technologyKnowledge = 0;
        public int socialKnowledge = 0;
        public int combatKnowledge = 0;

        public int TotalKnowledge => natureKnowledge + technologyKnowledge + socialKnowledge + combatKnowledge;

        private Dictionary<string, float> observationKnowledge = new();

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
        }

        public void OnKnowledgeGained(int amount)
        {
            int categorizedAmount = Mathf.RoundToInt(amount * knowledgeMultiplier);
            GameManager.Instance.AddKnowledgePoints(categorizedAmount);
        }

        public void GainNatureKnowledge(int amount)
        {
            natureKnowledge += amount;
            OnKnowledgeGained(amount);
        }

        public void GainTechnologyKnowledge(int amount)
        {
            technologyKnowledge += amount;
            OnKnowledgeGained(amount);
        }

        public void GainSocialKnowledge(int amount)
        {
            socialKnowledge += amount;
            OnKnowledgeGained(amount);
        }

        public void GainCombatKnowledge(int amount)
        {
            combatKnowledge += amount;
            OnKnowledgeGained(amount);
        }

        public void StudyAnimal(string animalName, float duration)
        {
            if (!observationKnowledge.ContainsKey(animalName))
            {
                observationKnowledge[animalName] = 0;
            }

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
            GainNatureKnowledge(10);
            Debug.Log($"Discovered: {phenomenonName}! +10 Nature Knowledge");
        }

        public bool HasEnoughKnowledge(int amount)
        {
            return TotalKnowledge >= amount;
        }

        public string GetKnowledgeBreakdown()
        {
            return $"Nature: {natureKnowledge} | Tech: {technologyKnowledge} | Social: {socialKnowledge} | Combat: {combatKnowledge}";
        }
    }
}
