using System.Collections.Generic;
using UnityEngine;

namespace Knowledge.Game
{
    public enum AnimalDiet { Herbivore, Carnivore, Omnivore, Detritivore }
    public enum AnimalBehavior { Passive, Aggressive, Territorial, Social }

    [System.Serializable]
    public class AnimalSpecies
    {
        public string speciesName;
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

    public class EcosystemManager : MonoBehaviour
    {
        public static EcosystemManager Instance { get; private set; }

        [Header("Ecosystem Settings")]
        public List<AnimalSpecies> allSpecies = new();
        public int maxAnimalsPerSpecies = 20;
        public bool ecosystemEnabled = true;

        [Header("Population Control")]
        public float reproductionRate = 0.01f;
        public float migrationChance = 0.005f;

        private Dictionary<string, List<AnimalSpecies>> speciesByEra = new();
        private Dictionary<string, int> populationCounts = new();

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
            InitializeSpecies();
        }

        private void InitializeSpecies()
        {
            allSpecies = new List<AnimalSpecies>
            {
                new AnimalSpecies { speciesName = "Mammut", diet = AnimalDiet.Herbivore, behavior = AnimalBehavior.Social, health = 200f, speed = 4f, resourceMeat = 50, resourceHide = 20, resourceBones = 15, eraIndex = 0 },
                new AnimalSpecies { speciesName = "TigreDentiSciabola", diet = AnimalDiet.Carnivore, behavior = AnimalBehavior.Aggressive, health = 80f, speed = 7f, resourceMeat = 30, resourceHide = 10, resourceBones = 5, canBeTamed = false, eraIndex = 0 },
                new AnimalSpecies { speciesName = "Megaceronte", diet = AnimalDiet.Herbivore, behavior = AnimalBehavior.Passive, health = 150f, speed = 3f, resourceMeat = 40, resourceBones = 10, eraIndex = 0 },
                new AnimalSpecies { speciesName = "Pterodattilo", diet = AnimalDiet.Carnivore, behavior = AnimalBehavior.Passive, health = 20f, speed = 10f, resourceMeat = 10, resourceBones = 5, eraIndex = 0 },
                new AnimalSpecies { speciesName = "OrsoCaverne", diet = AnimalDiet.Omnivore, behavior = AnimalBehavior.Territorial, health = 100f, speed = 5f, resourceMeat = 35, resourceHide = 15, eraIndex = 0 },
                new AnimalSpecies { speciesName = "Bisonte", diet = AnimalDiet.Herbivore, behavior = AnimalBehavior.Social, health = 80f, speed = 5f, resourceMeat = 30, resourceHide = 10, eraIndex = 0 },
                
                new AnimalSpecies { speciesName = "Lupo", diet = AnimalDiet.Carnivore, behavior = AnimalBehavior.Social, health = 40f, speed = 8f, resourceMeat = 15, resourceHide = 5, canBeTamed = true, eraIndex = 1 },
                new AnimalSpecies { speciesName = "Cervo", diet = AnimalDiet.Herbivore, behavior = AnimalBehavior.Passive, health = 50f, speed = 7f, resourceMeat = 20, resourceBones = 5, eraIndex = 1 },
                new AnimalSpecies { speciesName = "Cinghiale", diet = AnimalDiet.Omnivore, behavior = AnimalBehavior.Territorial, health = 60f, speed = 6f, resourceMeat = 25, resourceHide = 8, eraIndex = 1 },
                new AnimalSpecies { speciesName = "Aquila", diet = AnimalDiet.Carnivore, behavior = AnimalBehavior.Passive, health = 15f, speed = 12f, resourceMeat = 5, resourceBones = 2, eraIndex = 1 },
                new AnimalSpecies { speciesName = "Cavallo", diet = AnimalDiet.Herbivore, behavior = AnimalBehavior.Social, health = 70f, speed = 10f, resourceMeat = 25, resourceHide = 8, canBeTamed = true, eraIndex = 1 },
                
                new AnimalSpecies { speciesName = "Pecora", diet = AnimalDiet.Herbivore, behavior = AnimalBehavior.Passive, health = 30f, speed = 4f, resourceMeat = 15, resourceHide = 8, canBeTamed = true, eraIndex = 2 },
                new AnimalSpecies { speciesName = "Maiale", diet = AnimalDiet.Omnivore, behavior = AnimalBehavior.Passive, health = 40f, speed = 4f, resourceMeat = 20, resourceHide = 5, canBeTamed = true, eraIndex = 2 },
                new AnimalSpecies { speciesName = "Bue", diet = AnimalDiet.Herbivore, behavior = AnimalBehavior.Passive, health = 100f, speed = 3f, resourceMeat = 40, resourceHide = 12, canBeTamed = true, eraIndex = 2 },
                new AnimalSpecies { speciesName = "Falco", diet = AnimalDiet.Carnivore, behavior = AnimalBehavior.Passive, health = 10f, speed = 15f, resourceMeat = 3, eraIndex = 2 },
                
                new AnimalSpecies { speciesName = "Balena", diet = AnimalDiet.Carnivore, behavior = AnimalBehavior.Passive, health = 500f, speed = 5f, resourceMeat = 100, resourceHide = 30, eraIndex = 3 },
                new AnimalSpecies { speciesName = "Dromedario", diet = AnimalDiet.Herbivore, behavior = AnimalBehavior.Passive, health = 80f, speed = 6f, resourceMeat = 20, resourceHide = 8, canBeTamed = true, eraIndex = 3 },
                new AnimalSpecies { speciesName = "Elefante", diet = AnimalDiet.Herbivore, behavior = AnimalBehavior.Social, health = 200f, speed = 4f, resourceMeat = 80, resourceHide = 25, eraIndex = 3 },
                
                new AnimalSpecies { speciesName = "Mucca", diet = AnimalDiet.Herbivore, behavior = AnimalBehavior.Passive, health = 80f, speed = 3f, resourceMeat = 30, resourceHide = 10, canBeTamed = true, eraIndex = 4 },
                new AnimalSpecies { speciesName = "Pollo", diet = AnimalDiet.Omnivore, behavior = AnimalBehavior.Passive, health = 5f, speed = 5f, resourceMeat = 5, eraIndex = 4 },
                new AnimalSpecies { speciesName = "Topo", diet = AnimalDiet.Detritivore, behavior = AnimalBehavior.Passive, health = 2f, speed = 3f, eraIndex = 4 },
                
                new AnimalSpecies { speciesName = "Panda", diet = AnimalDiet.Herbivore, behavior = AnimalBehavior.Passive, health = 50f, speed = 3f, resourceHide = 8, eraIndex = 5 },
                new AnimalSpecies { speciesName = "Delfino", diet = AnimalDiet.Carnivore, behavior = AnimalBehavior.Social, health = 60f, speed = 12f, resourceMeat = 20, eraIndex = 5 },
                
                new AnimalSpecies { speciesName = "CreaturaAliena", diet = AnimalDiet.Omnivore, behavior = AnimalBehavior.Unknown, health = 100f, speed = 5f, eraIndex = 7 },
            };

            foreach (var species in allSpecies)
            {
                speciesByEra[species.eraIndex.ToString()] = speciesByEra.ContainsKey(species.eraIndex.ToString()) 
                    ? speciesByEra[species.eraIndex.ToString()] : new List<AnimalSpecies>();
                speciesByEra[species.eraIndex.ToString()].Add(species);
                populationCounts[species.speciesName] = Random.Range(5, maxAnimalsPerSpecies);
            }
        }

        private void Update()
        {
            if (!ecosystemEnabled || GameManager.Instance.gamePaused) return;
            UpdateEcosystem(Time.deltaTime);
        }

        private void UpdateEcosystem(float delta)
        {
            foreach (var species in allSpecies)
            {
                if (populationCounts[species.speciesName] < maxAnimalsPerSpecies)
                {
                    if (Random.value < reproductionRate * delta)
                        populationCounts[species.speciesName]++;
                }

                if (populationCounts[species.speciesName] > maxAnimalsPerSpecies * 0.8f)
                {
                    if (Random.value < migrationChance * delta)
                        populationCounts[species.speciesName]--;
                }
            }
        }

        public List<AnimalSpecies> GetSpeciesForEra(int eraIndex)
        {
            return speciesByEra.ContainsKey(eraIndex.ToString()) ? speciesByEra[eraIndex.ToString()] : new List<AnimalSpecies>();
        }

        public int GetPopulation(string speciesName)
        {
            return populationCounts.ContainsKey(speciesName) ? populationCounts[speciesName] : 0;
        }

        public void AnimalHunted(string speciesName)
        {
            if (populationCounts.ContainsKey(speciesName) && populationCounts[speciesName] > 0)
                populationCounts[speciesName]--;
        }

        public float GetBiodiversityScore(int eraIndex)
        {
            var species = GetSpeciesForEra(eraIndex);
            int totalPop = 0;
            foreach (var s in species)
                totalPop += populationCounts[s.speciesName];
            return species.Count > 0 ? (float)totalPop / species.Count : 0;
        }
    }
}
