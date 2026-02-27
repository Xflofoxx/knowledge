using NUnit.Framework;
using UnityEngine;
using System.IO;

namespace Knowledge.Tests.Core
{
    [TestFixture]
    public class SaveLoadSystemTests
    {
        private SaveLoadSystem saveLoadSystem;
        private string testSavePath;

        [SetUp]
        public void Setup()
        {
            var gameObject = new GameObject("SaveLoadSystem");
            saveLoadSystem = gameObject.AddComponent<SaveLoadSystem>();
            testSavePath = Application.persistentDataPath + "/test_saves/";
        }

        [TearDown]
        public void Teardown()
        {
            if (Directory.Exists(testSavePath))
            {
                Directory.Delete(testSavePath, true);
            }
            Object.DestroyImmediate(saveLoadSystem.gameObject);
        }

        [Test]
        public void SaveGame_ValidSlot_SavesFile()
        {
            GameData data = new GameData
            {
                playerName = "TestPlayer",
                knowledgePoints = 100,
                currentEra = "StoneAge"
            };

            bool result = saveLoadSystem.SaveGame("test_slot", data);
            Assert.IsTrue(result);
            Assert.IsTrue(File.Exists(testSavePath + "test_slot.json"));
        }

        [Test]
        public void LoadGame_ExistingSlot_ReturnsData()
        {
            GameData data = new GameData
            {
                playerName = "TestPlayer",
                knowledgePoints = 100,
                currentEra = "StoneAge"
            };

            saveLoadSystem.SaveGame("test_slot", data);
            GameData loadedData = saveLoadSystem.LoadGame("test_slot");

            Assert.IsNotNull(loadedData);
            Assert.AreEqual("TestPlayer", loadedData.playerName);
            Assert.AreEqual(100, loadedData.knowledgePoints);
        }

        [Test]
        public void LoadGame_NonExistingSlot_ReturnsNull()
        {
            GameData loadedData = saveLoadSystem.LoadGame("non_existing");
            Assert.IsNull(loadedData);
        }

        [Test]
        public void DeleteSave_ExistingSlot_DeletesFile()
        {
            GameData data = new GameData { playerName = "Test" };
            saveLoadSystem.SaveGame("to_delete", data);
            
            bool result = saveLoadSystem.DeleteSave("to_delete");
            
            Assert.IsTrue(result);
            Assert.IsFalse(File.Exists(testSavePath + "to_delete.json"));
        }

        [Test]
        public void GetSaveSlots_ReturnsAllSlots()
        {
            GameData data = new GameData { playerName = "Test1" };
            saveLoadSystem.SaveGame("slot1", data);
            saveLoadSystem.SaveGame("slot2", data);
            saveLoadSystem.SaveGame("slot3", data);

            string[] slots = saveLoadSystem.GetSaveSlots();

            Assert.AreEqual(3, slots.Length);
            Assert.Contains("slot1", slots);
            Assert.Contains("slot2", slots);
            Assert.Contains("slot3", slots);
        }
    }
}
