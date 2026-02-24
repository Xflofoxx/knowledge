using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Knowledge.Game
{
    public sealed class DiscoverySystem : MonoBehaviour
    {
        public static DiscoverySystem Instance { get; private set; }

        [Header("Discovery Data")]
        [SerializeField] private List<DiscoveryRecipe> allRecipes = new();
        [SerializeField] private HashSet<string> discoveredItems = new();
        [SerializeField] private HashSet<string> unlockedTechnologies = new();

        public IReadOnlyCollection<string> DiscoveredItems => discoveredItems;
        public IReadOnlyCollection<string> UnlockedTechnologies => unlockedTechnologies;
        public int TotalRecipes => allRecipes.Count;
        public int DiscoveredCount => discoveredItems.Count;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            InitializeRecipes();
        }

        private void InitializeRecipes()
        {
            allRecipes = new List<DiscoveryRecipe>
            {
                new() { inputs = new[] { "Legno", "Pietra" }, output = "Ascia", requiredKnowledge = 10 },
                new() { inputs = new[] { "Legno", "Osso" }, output = "Lancia", requiredKnowledge = 15 },
                new() { inputs = new[] { "Pietra", "Pietra" }, output = "Affilatore", requiredKnowledge = 5 },
                new() { inputs = new[] { "Legno" }, output = "Fuoco", requiredKnowledge = 20 },
                new() { inputs = new[] { "Fuoco", "Carne" }, output = "Carne Cotta", requiredKnowledge = 25 },
                new() { inputs = new[] { "Legno", "Legno" }, output = "Rifugio", requiredKnowledge = 30 },
                new() { inputs = new[] { "Rame", "Stagno" }, output = "Bronzo", requiredKnowledge = 50 },
                new() { inputs = new[] { "Bronzo", "Legno" }, output = "Spada Bronzo", requiredKnowledge = 60 },
                new() { inputs = new[] { "Ferro", "Carbone" }, output = "Acciaio", requiredKnowledge = 100 },
                new() { inputs = new[] { "Acciaio", "Legno" }, output = "Armatura", requiredKnowledge = 120 },
                new() { inputs = new[] { "Vetro", "Rame" }, output = "Lente", requiredKnowledge = 150 },
                new() { inputs = new[] { "Acciaio", "Vapore" }, output = "Motore Vapore", requiredKnowledge = 200 },
                new() { inputs = new[] { "Motore Vapore", "Metallo" }, output = "Treno", requiredKnowledge = 220 },
                new() { inputs = new[] { "Elettronica", "Plastica" }, output = "Computer", requiredKnowledge = 300 },
                new() { inputs = new[] { "Computer", "Metallo" }, output = "Robot", requiredKnowledge = 350 },
                new() { inputs = new[] { "Isotopi", "Metallo" }, output = "Razzo", requiredKnowledge = 400 },
                new() { inputs = new[] { "Razzo", "Isotopi" }, output = "Satellite", requiredKnowledge = 450 },
                new() { inputs = new[] { "Satellite", "Computer" }, output = "Stazione Spaziale", requiredKnowledge = 500 },
            };
        }

        public DiscoveryResult TryCombine(string[] inputs)
        {
            if (inputs == null || inputs.Length == 0)
                return new DiscoveryResult { success = false, message = "No inputs provided." };

            var sortedInputs = inputs.OrderBy(i => i).ToArray();

            foreach (var recipe in allRecipes)
            {
                var recipeInputs = recipe.inputs.OrderBy(i => i).ToArray();

                if (sortedInputs.SequenceEqual(recipeInputs))
                {
                    if (discoveredItems.Contains(recipe.output))
                        return new DiscoveryResult { success = false, message = $"{recipe.output} already discovered!" };

                    int currentKnowledge = GameManager.Instance?.TotalKnowledgePoints ?? 0;
                    if (currentKnowledge < recipe.requiredKnowledge)
                        return new DiscoveryResult { success = false, message = $"Not enough knowledge. Need {recipe.requiredKnowledge} KP." };

                    discoveredItems.Add(recipe.output);
                    GameManager.Instance?.AddKnowledgePoints(recipe.requiredKnowledge);
                    GameManager.Instance?.DiscoveredItems.Add(recipe.output);
                    
                    return new DiscoveryResult { success = true, itemDiscovered = recipe.output };
                }
            }

            return new DiscoveryResult { success = false, message = "No valid combination found." };
        }

        public bool IsItemDiscovered(string itemName) => 
            !string.IsNullOrEmpty(itemName) && discoveredItems.Contains(itemName);

        public List<DiscoveryRecipe> GetAvailableRecipes()
        {
            int currentKnowledge = GameManager.Instance?.TotalKnowledgePoints ?? 0;
            return allRecipes
                .Where(r => !discoveredItems.Contains(r.output) && currentKnowledge >= r.requiredKnowledge)
                .ToList();
        }

        public void UnlockTechnology(string technology)
        {
            if (!string.IsNullOrEmpty(technology))
                unlockedTechnologies.Add(technology);
        }

        public bool IsTechnologyUnlocked(string technology) =>
            !string.IsNullOrEmpty(technology) && unlockedTechnologies.Contains(technology);

        public int GetTotalRecipesCount() => TotalRecipes;
        public int GetDiscoveredCount() => DiscoveredCount;
    }

    [System.Serializable]
    public class DiscoveryRecipe
    {
        public string[] inputs = Array.Empty<string>();
        public string output = string.Empty;
        public int requiredKnowledge;
        public string description = string.Empty;
    }

    public readonly struct DiscoveryResult
    {
        public bool Success => success;
        public string ItemDiscovered => itemDiscovered;
        public string Message => message;

        public readonly bool success;
        public readonly string itemDiscovered;
        public readonly string message;

        public DiscoveryResult(bool success, string itemDiscovered = null, string message = null)
        {
            this.success = success;
            this.itemDiscovered = itemDiscovered ?? string.Empty;
            this.message = message ?? string.Empty;
        }
    }
}
