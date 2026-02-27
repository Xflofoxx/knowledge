using NUnit.Framework;
using UnityEngine;

namespace Knowledge.Tests.Core
{
    [TestFixture]
    public class GameManagerTests
    {
        private GameManager gameManager;

        [SetUp]
        public void Setup()
        {
            var gameObject = new GameObject("GameManager");
            gameManager = gameObject.AddComponent<GameManager>();
        }

        [TearDown]
        public void Teardown()
        {
            Object.DestroyImmediate(gameManager.gameObject);
        }

        [Test]
        public void AddKnowledgePoints_ValidAmount_IncreasesKP()
        {
            int initialKP = gameManager.TotalKnowledgePoints;
            gameManager.AddKnowledgePoints(10);
            Assert.AreEqual(initialKP + 10, gameManager.TotalKnowledgePoints);
        }

        [Test]
        public void AddKnowledgePoints_NegativeAmount_DoesNotDecreaseKP()
        {
            gameManager.AddKnowledgePoints(10);
            gameManager.AddKnowledgePoints(-5);
            Assert.GreaterOrEqual(gameManager.TotalKnowledgePoints, 10);
        }

        [Test]
        public void PauseGame_SetsIsPaused()
        {
            Assert.IsFalse(gameManager.IsGamePaused);
            gameManager.PauseGame();
            Assert.IsTrue(gameManager.IsGamePaused);
        }

        [Test]
        public void ResumeGame_SetsIsNotPaused()
        {
            gameManager.PauseGame();
            gameManager.ResumeGame();
            Assert.IsFalse(gameManager.IsGamePaused);
        }

        [Test]
        public void SetTimeScale_ValidScale_ChangesTimeScale()
        {
            gameManager.SetTimeScale(0.5f);
            Assert.AreEqual(0.5f, gameManager.TimeScale);
        }

        [Test]
        public void SetTimeScale_OutOfRange_ClampsToValidRange()
        {
            gameManager.SetTimeScale(5f);
            Assert.AreEqual(2f, gameManager.TimeScale);

            gameManager.SetTimeScale(0f);
            Assert.AreEqual(0.1f, gameManager.TimeScale);
        }

        [Test]
        public void GetCurrentState_ReturnsCorrectState()
        {
            Assert.AreEqual(GameState.Playing, gameManager.CurrentState);
            
            gameManager.PauseGame();
            Assert.AreEqual(GameState.Paused, gameManager.CurrentState);
        }
    }
}
