using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

namespace Knowledge.Tests.Core
{
    [TestFixture]
    public class AchievementSystemTests
    {
        private AchievementSystem achievementSystem;

        [SetUp]
        public void Setup()
        {
            var gameObject = new GameObject("AchievementSystem");
            achievementSystem = gameObject.AddComponent<AchievementSystem>();
        }

        [TearDown]
        public void Teardown()
        {
            Object.DestroyImmediate(achievementSystem.gameObject);
        }

        [Test]
        public void RegisterAchievement_ValidAchievement_AddsToList()
        {
            var achievement = new Achievement
            {
                id = "test_achievement",
                name = "Test Achievement",
                description = "Test Description",
                knowledgeReward = 10
            };

            achievementSystem.RegisterAchievement(achievement);
            Assert.IsTrue(achievementSystem.HasAchievement("test_achievement"));
        }

        [Test]
        public void UnlockAchievement_UnlockedAchievement_ReturnsTrue()
        {
            var achievement = new Achievement
            {
                id = "unlock_test",
                name = "Unlock Test",
                knowledgeReward = 5
            };
            achievementSystem.RegisterAchievement(achievement);

            bool result = achievementSystem.UnlockAchievement("unlock_test");
            Assert.IsTrue(result);
            Assert.IsTrue(achievementSystem.IsUnlocked("unlock_test"));
        }

        [Test]
        public void UnlockAchievement_AlreadyUnlocked_ReturnsFalse()
        {
            var achievement = new Achievement { id = "double_unlock" };
            achievementSystem.RegisterAchievement(achievement);
            achievementSystem.UnlockAchievement("double_unlock");

            bool result = achievementSystem.UnlockAchievement("double_unlock");
            Assert.IsFalse(result);
        }

        [Test]
        public void GetUnlockedCount_ReturnsCorrectCount()
        {
            achievementSystem.RegisterAchievement(new Achievement { id = "ach1" });
            achievementSystem.RegisterAchievement(new Achievement { id = "ach2" });
            achievementSystem.RegisterAchievement(new Achievement { id = "ach3" });

            achievementSystem.UnlockAchievement("ach1");
            achievementSystem.UnlockAchievement("ach3");

            Assert.AreEqual(2, achievementSystem.GetUnlockedCount());
        }

        [Test]
        public void GetTotalKnowledgeFromAchievements_ReturnsCorrectSum()
        {
            achievementSystem.RegisterAchievement(new Achievement { id = "ach1", knowledgeReward = 10 });
            achievementSystem.RegisterAchievement(new Achievement { id = "ach2", knowledgeReward = 20 });
            achievementSystem.RegisterAchievement(new Achievement { id = "ach3", knowledgeReward = 30 });

            achievementSystem.UnlockAchievement("ach1");
            achievementSystem.UnlockAchievement("ach2");

            Assert.AreEqual(30, achievementSystem.GetTotalKnowledgeFromAchievements());
        }

        [Test]
        public void GetProgress_ValidAchievement_ReturnsProgress()
        {
            achievementSystem.RegisterAchievement(new Achievement
            {
                id = "progress_ach",
                targetValue = 100
            });

            achievementSystem.UpdateProgress("progress_ach", 50);
            var progress = achievementSystem.GetProgress("progress_ach");

            Assert.AreEqual(50, progress.currentValue);
            Assert.AreEqual(100, progress.targetValue);
        }

        [Test]
        public void IsUnlocked_NonExistent_ReturnsFalse()
        {
            Assert.IsFalse(achievementSystem.IsUnlocked("non_existent"));
        }
    }
}
