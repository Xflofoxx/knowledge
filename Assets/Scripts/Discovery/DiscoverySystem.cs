using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Knowledge.Game
{
    public class DiscoverySystem : MonoBehaviour
    {
        public static DiscoverySystem Instance { get; private set; }

        [Header("Discovery Data")]
        public List<DiscoveryRecipe> allRecipes = new();
        public HashSet<string> discoveredItems = new();
        public HashSet<string> unlockedTechnologies = new();

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
            InitializeRecipes();
        }

        private void InitializeRecipes()
        {
            allRecipes = new List<DiscoveryRecipe>
            {
                new DiscoveryRecipe { inputs = new[] { "Legno", "Pietra" }, output = "Ascia", requiredKnowledge = 10 },
                new DiscoveryRecipe { inputs = new[] { "Legno", "Osso" }, output = "Lancia", requiredKnowledge = 15 },
                new DiscoveryRecipe { inputs = new[] { "Pietra", "Pietra" }, output = "Affilatore", requiredKnowledge = 5 },
                new DiscoveryRecipe { inputs = new[] { "Legno" }, output = "Fuoco", requiredKnowledge = 20 },
                new DiscoveryRecipe { inputs = new[] { "Fuoco", "Carne" }, output = "Carne Cotta", requiredKnowledge = 25 },
                new DiscoveryRecipe { inputs = new[] { "Legno", "Legno" }, output = "Rifugio", requiredKnowledge = 30 },
                new DiscoveryRecipe { inputs = new[] { "Rame", "Stagno" }, output = "Bronzo", requiredKnowledge = 50 },
                new DiscoveryRecipe { inputs = new[] { "Bronzo", "Legno" }, output = "Spada Bronzo", requiredKnowledge = 60 },
                new DiscoveryRecipe { inputs = new[] { "Ferro", "Carbone" }, output = "Acciaio", requiredKnowledge = 100 },
                new DiscoveryRecipe { inputs = new[] { "Acciaio", "Legno" }, output = "Armatura", requiredKnowledge = 120 },
                new DiscoveryRecipe { inputs = new[] { "Vetro", "Rame" }, output = "Lente", requiredKnowledge = 150 },
                new DiscoveryRecipe { inputs = new[] { "Acciaio", "Vapore" }, output = "Motore Vapore", requiredKnowledge = 200 },
                new DiscoveryRecipe { inputs = new[] { "Motore Vapore", "Metallo" }, output = "Treno", requiredKnowledge = 220 },
                new DiscoveryRecipe { inputs = new[] { "Elettronica", "Plastica" }, output = "Computer", requiredKnowledge = 300 },
                new DiscoveryRecipe { inputs = new[] { "Computer", "Metallo" }, output = "Robot", requiredKnowledge = 350 },
                new DiscoveryRecipe { inputs = new[] { "Isotopi", "Metallo" }, output = "Razzo", requiredKnowledge = 400 },
                new DiscoveryRecipe { inputs = new[] { "Razzo", "Isotopi" }, output = "Satellite", requiredKnowledge = 450 },
                new DiscoveryRecipe { inputs = new[] { "Satellite", "Computer" }, output = "Stazione Spaziale", requiredKnowledge = 500 },
            };
        }

        public DiscoveryResult TryCombine(string[] inputs)
        {
            var sortedInputs = inputs.OrderBy(i => i).ToArray();

            foreach (var recipe in allRecipes)
            {
                var recipeInputs = recipe.inputs.OrderBy(i => i).ToArray();

                if (sortedInputs.SequenceEqual(recipeInputs))
                {
                    if (!discoveredItems.Contains(recipe.output))
                    {
                        if (GameManager.Instance.TotalKnowledgePoints >= recipe.requiredKnowledge)
                        {
                            discoveredItems.Add(recipe.output);
                            GameManager.Instance.AddKnowledgePoints(recipe.requiredKnowledge);
                            GameManager.Instance.DiscoveredItems.Add(recipe.output);
                            return new DiscoveryResult { success = true, itemDiscovered = recipe.output };
                        }
                        return new DiscoveryResult { success = false, message = $"Not enough knowledge. Need {recipe.requiredKnowledge} KP." };
                    }
                    return new DiscoveryResult { success = false, message = $"{recipe.output} already discovered!" };
                }
            }

            return new DiscoveryResult { success = false, message = "No valid combination found." };
        }

        public bool IsItemDiscovered(string itemName)
        {
            return discoveredItems.Contains(itemName);
        }

        public List<DiscoveryRecipe> GetAvailableRecipes()
        {
            return allRecipes.Where(r => !discoveredItems.Contains(r.output) && 
                GameManager.Instance.TotalKnowledgePoints >= r.requiredKnowledge).ToList();
        }

        public int GetTotalRecipesCount() => allRecipes.Count;
        public int GetDiscoveredCount() => discoveredItems.Count;
    }

    [System.Serializable]
    public class DiscoveryRecipe
    {
        public string[] inputs;
        public string output;
        public int requiredKnowledge;
        public string description;
    }

    public struct DiscoveryResult
    {
        public bool success;
        public string itemDiscovered;
        public string message;
    }
}
