using NUnit.Framework;
using UnityEngine;

namespace Knowledge.Game.Tests
{
    [TestFixture]
    public class EcosystemManagerTests
    {
        private EcosystemManager _ecosystem;
        private GameObject _go;

        [SetUp]
        public void Setup()
        {
            _go = new GameObject();
            _ecosystem = _go.AddComponent<EcosystemManager>();
        }

        [TearDown]
        public void Teardown()
        {
            Object.DestroyImmediate(_go);
        }

        [Test]
        public void Instance_Set_SingleInstance()
        {
            Assert.AreEqual(_ecosystem, EcosystemManager.Instance);
        }

        [Test]
        public void GetSpeciesForEra_StoneAge_ReturnsMammut()
        {
            var species = _ecosystem.GetSpeciesForEra(0);
            Assert.IsNotEmpty(species);
            Assert.IsTrue(species.Exists(s => s.speciesName == "Mammut"));
        }

        [Test]
        public void GetSpeciesForEra_BronzeAge_ReturnsLupo()
        {
            var species = _ecosystem.GetSpeciesForEra(1);
            Assert.IsNotEmpty(species);
            Assert.IsTrue(species.Exists(s => s.speciesName == "Lupo"));
        }

        [Test]
        public void GetSpeciesForEra_IronAge_ReturnsPecora()
        {
            var species = _ecosystem.GetSpeciesForEra(2);
            Assert.IsNotEmpty(species);
            Assert.IsTrue(species.Exists(s => s.speciesName == "Pecora"));
        }

        [Test]
        public void GetSpeciesForEra_UnknownEra_ReturnsEmpty()
        {
            var species = _ecosystem.GetSpeciesForEra(99);
            Assert.IsEmpty(species);
        }

        [Test]
        public void GetPopulation_Mammut_ReturnsPositiveValue()
        {
            int population = _ecosystem.GetPopulation("Mammut");
            Assert.Greater(population, 0);
        }

        [Test]
        public void GetPopulation_UnknownSpecies_ReturnsZero()
        {
            int population = _ecosystem.GetPopulation("Unknown");
            Assert.AreEqual(0, population);
        }

        [Test]
        public void AnimalHunted_DecreasesPopulation()
        {
            int initialPop = _ecosystem.GetPopulation("Mammut");
            _ecosystem.AnimalHunted("Mammut");

            Assert.Less(_ecosystem.GetPopulation("Mammut"), initialPop);
        }

        [Test]
        public void AnimalHunted_ZeroPopulation_DoesNotGoNegative()
        {
            int initialPop = _ecosystem.GetPopulation("Pterodattilo");
            for (int i = 0; i < 100; i++)
                _ecosystem.AnimalHunted("Pterodattilo");

            Assert.GreaterOrEqual(_ecosystem.GetPopulation("Pterodattilo"), 0);
        }

        [Test]
        public void GetBiodiversityScore_ValidEra_ReturnsPositive()
        {
            float score = _ecosystem.GetBiodiversityScore(0);
            Assert.Greater(score, 0);
        }

        [Test]
        public void GetBiodiversityScore_UnknownEra_ReturnsZero()
        {
            float score = _ecosystem.GetBiodiversityScore(99);
            Assert.AreEqual(0, score);
        }

        [Test]
        public void AllSpecies_HaveValidDiet()
        {
            foreach (var species in _ecosystem.AllSpecies)
            {
                Assert.IsNotNull(species.diet);
            }
        }

        [Test]
        public void AllSpecies_HaveValidBehavior()
        {
            foreach (var species in _ecosystem.AllSpecies)
            {
                Assert.IsNotNull(species.behavior);
            }
        }

        [Test]
        public void SpeciesCount_MoreThan30()
        {
            Assert.Greater(_ecosystem.AllSpecies.Count, 30);
        }
    }
}
