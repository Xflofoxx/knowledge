using NUnit.Framework;
using System.Linq;
using UnityEngine;

namespace Knowledge.Game.Tests
{
    [TestFixture]
    public class DiscoverySystemTests
    {
        private DiscoverySystem _discovery;
        private GameObject _go;
        private GameManager _gm;

        [SetUp]
        public void Setup()
        {
            _go = new GameObject();
            _gm = _go.AddComponent<GameManager>();
            _discovery = _go.AddComponent<DiscoverySystem>();
        }

        [TearDown]
        public void Teardown()
        {
            Object.DestroyImmediate(_go);
        }

        [Test]
        public void Instance_Set_SingleInstance()
        {
            Assert.AreEqual(_discovery, DiscoverySystem.Instance);
        }

        [Test]
        public void TryCombine_LegnoPietra_ReturnsAxe()
        {
            var result = _discovery.TryCombine(new[] { "Legno", "Pietra" });

            Assert.IsTrue(result.success);
            Assert.AreEqual("Ascia", result.itemDiscovered);
        }

        [Test]
        public void TryCombine_LegnoOsso_ReturnsLancia()
        {
            var result = _discovery.TryCombine(new[] { "Legno", "Osso" });

            Assert.IsTrue(result.success);
            Assert.AreEqual("Lancia", result.itemDiscovered);
        }

        [Test]
        public void TryCombine_InvalidCombination_ReturnsFalse()
        {
            var result = _discovery.TryCombine(new[] { "Oro", "Plastica" });

            Assert.IsFalse(result.success);
            Assert.IsNull(result.itemDiscovered);
        }

        [Test]
        public void TryCombine_AlreadyDiscovered_ReturnsFalse()
        {
            _discovery.TryCombine(new[] { "Legno", "Pietra" });
            var result = _discovery.TryCombine(new[] { "Legno", "Pietra" });

            Assert.IsFalse(result.success);
            StringAssert.Contains("already discovered", result.message);
        }

        [Test]
        public void IsItemDiscovered_UndiscoveredItem_ReturnsFalse()
        {
            Assert.IsFalse(_discovery.IsItemDiscovered("Ascia"));
        }

        [Test]
        public void IsItemDiscovered_DiscoveredItem_ReturnsTrue()
        {
            _discovery.TryCombine(new[] { "Legno", "Pietra" });
            Assert.IsTrue(_discovery.IsItemDiscovered("Ascia"));
        }

        [Test]
        public void GetTotalRecipesCount_ReturnsCorrectNumber()
        {
            Assert.Greater(_discovery.GetTotalRecipesCount(), 0);
        }

        [Test]
        public void GetDiscoveredCount_InitiallyZero()
        {
            Assert.AreEqual(0, _discovery.GetDiscoveredCount());
        }

        [Test]
        public void GetDiscoveredCount_AfterDiscovery_Increases()
        {
            _discovery.TryCombine(new[] { "Legno", "Pietra" });
            Assert.AreEqual(1, _discovery.GetDiscoveredCount());
        }

        [Test]
        public void TryCombine_NotEnoughKnowledge_ReturnsFalse()
        {
            _gm.AddKnowledgePoints(5);
            var result = _discovery.TryCombine(new[] { "Legno", "Pietra" });

            Assert.IsFalse(result.success);
            StringAssert.Contains("Not enough knowledge", result.message);
        }

        [Test]
        public void TryCombine_RameStagno_ReturnsBronzo()
        {
            _gm.AddKnowledgePoints(50);
            var result = _discovery.TryCombine(new[] { "Rame", "Stagno" });

            Assert.IsTrue(result.success);
            Assert.AreEqual("Bronzo", result.itemDiscovered);
        }

        [Test]
        public void TryCombine_FuocoCarne_ReturnsCarneCotta()
        {
            _gm.AddKnowledgePoints(25);
            var result = _discovery.TryCombine(new[] { "Fuoco", "Carne" });

            Assert.IsTrue(result.success);
            Assert.AreEqual("Carne Cotta", result.itemDiscovered);
        }
    }
}
