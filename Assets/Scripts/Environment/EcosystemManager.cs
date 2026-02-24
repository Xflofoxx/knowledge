using System.Collections.Generic;
using UnityEngine;

namespace Knowledge.Game
{
    public enum AnimalDiet { Herbivore, Carnivore, Omnivore, Detritivore }
    public enum AnimalBehavior { Passive, Aggressive, Territorial, Social, Unknown }

    [System.Serializable]
    public class AnimalSpecies
    {
        public string speciesName = string.Empty;
        public GameObject prefab;
        public AnimalDiet diet;
        public AnimalBehavior behavior;
        public float health = 50f;
        public float speed = 3f;
        public int resourceMeat;
        public int resourceHide;
        public int resourceBones;
        public bool canBeTamed;
        public bool canBeHunted = true;
        public int eraIndex;
    }

    public sealed class EcosystemManager : MonoBehaviour
    {
        public static EcosystemManager Instance { get; private set; }

        [Header("Ecosystem Settings")]
        [SerializeField] private List<AnimalSpecies> allSpecies = new();
        [SerializeField] private int maxAnimalsPerSpecies = 20;
        [SerializeField] private bool ecosystemEnabled = true;

        [Header("Population Control")]
        [SerializeField] private float reproductionRate = 0.01f;
        [SerializeField] private float migrationChance = 0.005f;

        private readonly Dictionary<string, List<AnimalSpecies>> speciesByEra = new();
        private readonly Dictionary<string, int> populationCounts = new();

        public int SpeciesCount => allSpecies.Count;
        public IReadOnlyList<AnimalSpecies> AllSpecies => allSpecies;
        public bool IsEnabled => ecosystemEnabled;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            InitializeSpecies();
        }

        private void InitializeSpecies()
        {
            allSpecies = new List<AnimalSpecies>
            {
                new() { speciesName = "Mammut", diet = AnimalDiet.Herbivore, behavior = AnimalBehavior.Social, health = 200f, speed = 4f, resourceMeat = 50, resourceHide = 20, resourceBones = 15, eraIndex = 0 },
                new() { speciesName = "TigreDentiSciabola", diet = AnimalDiet.Carnivore, behavior = AnimalBehavior.Aggressive, health = 80f, speed = 7f, resourceMeat = 30, resourceHide = 10, resourceBones = 5, canBeTamed = false, eraIndex = 0 },
                new() { speciesName = "Megaceronte", diet = AnimalDiet.Herbivore, behavior = AnimalBehavior.Passive, health = 150f, speed = 3f, resourceMeat = 40, resourceBones = 10, eraIndex = 0 },
                new() { speciesName = "Pterodattilo", diet = AnimalDiet.Carnivore, behavior = AnimalBehavior.Passive, health = 20f, speed = 10f, resourceMeat = 10, resourceBones = 5, eraIndex = 0 },
                new() { speciesName = "OrsoCaverne", diet = AnimalDiet.Omnivore, behavior = AnimalBehavior.Territorial, health = 100f, speed = 5f, resourceMeat = 35, resourceHide = 15, eraIndex = 0 },
                new() { speciesName = "Bisonte", diet = AnimalDiet.Herbivore, behavior = AnimalBehavior.Social, health = 80f, speed = 5f, resourceMeat = 30, resourceHide = 10, eraIndex = 0 },
                
                new() { speciesName = "Lupo", diet = AnimalDiet.Carnivore, behavior = AnimalBehavior.Social, health = 40f, speed = 8f, resourceMeat = 15, resourceHide = 5, canBeTamed = true, eraIndex = 1 },
                new() { speciesName = "Cervo", diet = AnimalDiet.Herbivore, behavior = AnimalBehavior.Passive, health = 50f, speed = 7f, resourceMeat = 20, resourceBones = 5, eraIndex = 1 },
                new() { speciesName = "Cinghiale", diet = AnimalDiet.Omnivore, behavior = AnimalBehavior.Territorial, health = 60f, speed = 6f, resourceMeat = 25, resourceHide = 8, eraIndex = 1 },
                new() { speciesName = "Aquila", diet = AnimalDiet.Carnivore, behavior = AnimalBehavior.Passive, health = 15f, speed = 12f, resourceMeat = 5, resourceBones = 2, eraIndex = 1 },
                new() { speciesName = "Cavallo", diet = AnimalDiet.Herbivore, behavior = AnimalBehavior.Social, health = 70f, speed = 10f, resourceMeat = 25, resourceHide = 8, canBeTamed = true, eraIndex = 1 },
                
                new() { speciesName = "Pecora", diet = AnimalDiet.Herbivore, behavior = AnimalBehavior.Passive, health = 30f, speed = 4f, resourceMeat = 15, resourceHide = 8, canBeTamed = true, eraIndex = 2 },
                new() { speciesName = "Maiale", diet = AnimalDiet.Omnivore, behavior = AnimalBehavior.Passive, health = 40f, speed = 4f, resourceMeat = 20, resourceHide = 5, canBeTamed = true, eraIndex = 2 },
                new() { speciesName = "Bue", diet = AnimalDiet.Herbivore, behavior = AnimalBehavior.Passive, health = 100f, speed = 3f, resourceMeat = 40, resourceHide = 12, canBeTamed = true, eraIndex = 2 },
                new() { speciesName = "Falco", diet = AnimalDiet.Carnivore, behavior = AnimalBehavior.Passive, health = 10f, speed = 15f, resourceMeat = 3, eraIndex = 2 },
                
                new() { speciesName = "Balena", diet = AnimalDiet.Carnivore, behavior = AnimalBehavior.Passive, health = 500f, speed = 5f, resourceMeat = 100, resourceHide = 30, eraIndex = 3 },
                new() { speciesName = "Dromedario", diet = AnimalDiet.Herbivore, behavior = AnimalBehavior.Passive, health = 80f, speed = 6f, resourceMeat = 20, resourceHide = 8, canBeTamed = true, eraIndex = 3 },
                new() { speciesName = "Elefante", diet = AnimalDiet.Herbivore, behavior = AnimalBehavior.Social, health = 200f, speed = 4f, resourceMeat = 80, resourceHide = 25, eraIndex = 3 },
                
                new() { speciesName = "Mucca", diet = AnimalDiet.Herbivore, behavior = AnimalBehavior.Passive, health = 80f, speed = 3f, resourceMeat = 30, resourceHide = 10, canBeTamed = true, eraIndex = 4 },
                new() { speciesName = "Pollo", diet = AnimalDiet.Omnivore, behavior = AnimalBehavior.Passive, health = 5f, speed = 5f, resourceMeat = 5, eraIndex = 4 },
                new() { speciesName = "Topo", diet = AnimalDiet.Detritivore, behavior = AnimalBehavior.Passive, health = 2f, speed = 3f, eraIndex = 4 },
                
                new() { speciesName = "Panda", diet = AnimalDiet.Herbivore, behavior = AnimalBehavior.Passive, health = 50f, speed = 3f, resourceHide = 8, eraIndex = 5 },
                new() { speciesName = "Delfino", diet = AnimalDiet.Carnivore, behavior = AnimalBehavior.Social, health = 60f, speed = 12f, resourceMeat = 20, eraIndex = 5 },
                
                new() { speciesName = "CreaturaAliena", diet = AnimalDiet.Omnivore, behavior = AnimalBehavior.Unknown, health = 100f, speed = 5f, eraIndex = 7 },
            };

            foreach (var species in allSpecies)
            {
                string key = species.eraIndex.ToString();
                if (!speciesByEra.ContainsKey(key))
                    speciesByEra[key] = new List<AnimalSpecies>();
                
                speciesByEra[key].Add(species);
                populationCounts[species.speciesName] = Random.Range(5, maxAnimalsPerSpecies);
            }
        }

        private void Update()
        {
            if (!ecosystemEnabled || GameManager.Instance?.IsPaused == true) return;
            UpdateEcosystem(Time.deltaTime);
        }

        private void UpdateEcosystem(float delta)
        {
            foreach (var species in allSpecies)
            {
                string name = species.speciesName;
                if (!populationCounts.TryGetValue(name, out int currentPop)) continue;

                if (currentPop < maxAnimalsPerSpecies && Random.value < reproductionRate * delta)
                    populationCounts[name] = currentPop + 1;

                if (currentPop > maxAnimalsPerSpecies * 0.8f && Random.value < migrationChance * delta)
                    populationCounts[name] = currentPop - 1;
            }
        }

        public List<AnimalSpecies> GetSpeciesForEra(int eraIndex)
        {
            return speciesByEra.TryGetValue(eraIndex.ToString(), out var species) ? species : new List<AnimalSpecies>();
        }

        public int GetPopulation(string speciesName)
        {
            return populationCounts.TryGetValue(speciesName, out int pop) ? pop : 0;
        }

        public void AnimalHunted(string speciesName)
        {
            if (populationCounts.TryGetValue(speciesName, out int pop) && pop > 0)
                populationCounts[speciesName] = pop - 1;
        }

        public float GetBiodiversityScore(int eraIndex)
        {
            var species = GetSpeciesForEra(eraIndex);
            if (species.Count == 0) return 0;

            int totalPop = species.Sum(s => GetPopulation(s.speciesName));
            return (float)totalPop / species.Count;
        }
    }
}
