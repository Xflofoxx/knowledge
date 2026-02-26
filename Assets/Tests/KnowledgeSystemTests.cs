using NUnit.Framework;
using UnityEngine;

namespace Knowledge.Game.Tests
{
    [TestFixture]
    public class KnowledgeSystemTests
    {
        private KnowledgeSystem _knowledge;
        private GameObject _go;
        private GameManager _gm;

        [SetUp]
        public void Setup()
        {
            _go = new GameObject();
            _gm = _go.AddComponent<GameManager>();
            _knowledge = _go.AddComponent<KnowledgeSystem>();
        }

        [TearDown]
        public void Teardown()
        {
            Object.DestroyImmediate(_go);
        }

        [Test]
        public void Instance_Set_SingleInstance()
        {
            Assert.AreEqual(_knowledge, KnowledgeSystem.Instance);
        }

        [Test]
        public void TotalKnowledge_InitiallyZero()
        {
            Assert.AreEqual(0, _knowledge.TotalKnowledge);
        }

        [Test]
        public void GainNatureKnowledge_AddsToNatureAndTotal()
        {
            _knowledge.GainNatureKnowledge(10);

            Assert.AreEqual(10, _knowledge.NatureKnowledgeField);
            Assert.AreEqual(10, _knowledge.TotalKnowledge);
        }

        [Test]
        public void GainTechnologyKnowledge_AddsToTechAndTotal()
        {
            _knowledge.GainTechnologyKnowledge(15);

            Assert.AreEqual(15, _knowledge.TechnologyKnowledgeField);
            Assert.AreEqual(15, _knowledge.TotalKnowledge);
        }

        [Test]
        public void GainSocialKnowledge_AddsToSocialAndTotal()
        {
            _knowledge.GainSocialKnowledge(20);

            Assert.AreEqual(20, _knowledge.SocialKnowledgeField);
            Assert.AreEqual(20, _knowledge.TotalKnowledge);
        }

        [Test]
        public void GainCombatKnowledge_AddsToCombatAndTotal()
        {
            _knowledge.GainCombatKnowledge(25);

            Assert.AreEqual(25, _knowledge.CombatKnowledgeField);
            Assert.AreEqual(25, _knowledge.TotalKnowledge);
        }

        [Test]
        public void HasEnoughKnowledge_Enough_ReturnsTrue()
        {
            _knowledge.GainNatureKnowledge(100);
            Assert.IsTrue(_knowledge.HasEnoughKnowledge(50));
        }

        [Test]
        public void HasEnoughKnowledge_NotEnough_ReturnsFalse()
        {
            _knowledge.GainNatureKnowledge(10);
            Assert.IsFalse(_knowledge.HasEnoughKnowledge(50));
        }

        [Test]
        public void StudyAnimal_FirstTime_AddsObservation()
        {
            _knowledge.StudyAnimal("Lupo", 5f);

            Assert.Greater(_knowledge.TotalKnowledge, 0);
        }

        [Test]
        public void DiscoverPhenomenon_CallsGainKnowledge()
        {
            _knowledge.DiscoverPhenomenon("Fulmine");

            Assert.Greater(_knowledge.NatureKnowledgeField, 0);
        }

        [Test]
        public void GetKnowledgeBreakdown_ReturnsCorrectString()
        {
            _knowledge.GainNatureKnowledge(10);
            _knowledge.GainTechnologyKnowledge(20);

            string breakdown = _knowledge.GetKnowledgeBreakdown();

            StringAssert.Contains("Nature: 10", breakdown);
            StringAssert.Contains("Tech: 20", breakdown);
        }

        [Test]
        public void MultipleCategories_TotalIsSum()
        {
            _knowledge.GainNatureKnowledge(10);
            _knowledge.GainTechnologyKnowledge(20);
            _knowledge.GainSocialKnowledge(30);
            _knowledge.GainCombatKnowledge(40);

            Assert.AreEqual(100, _knowledge.TotalKnowledge);
        }
    }
}
