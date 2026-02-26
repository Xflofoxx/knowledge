using NUnit.Framework;
using UnityEngine;

namespace Knowledge.Game.Tests
{
    [TestFixture]
    public class GameManagerTests
    {
        private GameManager _gameManager;
        private GameObject _go;

        [SetUp]
        public void Setup()
        {
            _go = new GameObject();
            _gameManager = _go.AddComponent<GameManager>();
        }

        [TearDown]
        public void Teardown()
        {
            Object.DestroyImmediate(_go);
        }

        [Test]
        public void Instance_Set_SingleInstance()
        {
            var go1 = new GameObject();
            var go2 = new GameObject();
            var gm1 = go1.AddComponent<GameManager>();
            var gm2 = go2.AddComponent<GameManager>();

            Assert.AreEqual(gm1, GameManager.Instance);
            Object.DestroyImmediate(go1);
            Object.DestroyImmediate(go2);
        }

        [Test]
        public void AddKnowledgePoints_ValidAmount_IncreasesKP()
        {
            _gameManager.AddKnowledgePoints(50);
            Assert.AreEqual(50, _gameManager.TotalKnowledgePoints);
        }

        [Test]
        public void AddKnowledgePoints_MultipleCalls_AccumulatesKP()
        {
            _gameManager.AddKnowledgePoints(10);
            _gameManager.AddKnowledgePoints(20);
            _gameManager.AddKnowledgePoints(30);

            Assert.AreEqual(60, _gameManager.TotalKnowledgePoints);
        }

        [Test]
        public void UnlockEra_ValidIndex_UnlocksEra()
        {
            _gameManager.UnlockEra(3);
            Assert.IsTrue(_gameManager.CanAccessEra(3));
        }

        [Test]
        public void UnlockEra_InvalidIndex_DoesNotCrash()
        {
            _gameManager.UnlockEra(10);
            Assert.IsFalse(_gameManager.CanAccessEra(10));
        }

        [Test]
        public void CanAccessEra_FirstEra_AlwaysAccessible()
        {
            Assert.IsTrue(_gameManager.CanAccessEra(0));
        }

        [Test]
        public void CanAccessEra_LockedEra_ReturnsFalse()
        {
            Assert.IsFalse(_gameManager.CanAccessEra(5));
        }

        [Test]
        public void PauseGame_SetsGamePaused_True()
        {
            _gameManager.PauseGame();
            Assert.IsTrue(_gameManager.IsGamePaused);
        }

        [Test]
        public void ResumeGame_SetsGamePaused_False()
        {
            _gameManager.PauseGame();
            _gameManager.ResumeGame();
            Assert.IsFalse(_gameManager.IsGamePaused);
        }
    }
}
