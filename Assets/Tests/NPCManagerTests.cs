using NUnit.Framework;
using UnityEngine;

namespace Knowledge.Game.Tests
{
    [TestFixture]
    public class NPCManagerTests
    {
        private NPCManager _npcManager;
        private GameObject _go;

        [SetUp]
        public void Setup()
        {
            _go = new GameObject();
            _npcManager = _go.AddComponent<NPCManager>();
        }

        [TearDown]
        public void Teardown()
        {
            Object.DestroyImmediate(_go);
        }

        [Test]
        public void Instance_Set_SingleInstance()
        {
            Assert.AreEqual(_npcManager, NPCManager.Instance);
        }

        [Test]
        public void ModifyReputation_ValidNPC_ChangesReputation()
        {
            _npcManager.ModifyReputation("Giocatore", 20);
            Assert.AreEqual(20, _npcManager.GetReputation("Giocatore"));
        }

        [Test]
        public void ModifyReputation_NegativeValue_DecreasesReputation()
        {
            _npcManager.ModifyReputation("Giocatore", -30);
            Assert.AreEqual(-30, _npcManager.GetReputation("Giocatore"));
        }

        [Test]
        public void ModifyReputation_MultipleTimes_Accumulates()
        {
            _npcManager.ModifyReputation("Giocatore", 10);
            _npcManager.ModifyReputation("Giocatore", 20);
            _npcManager.ModifyReputation("Giocatore", -5);

            Assert.AreEqual(25, _npcManager.GetReputation("Giocatore"));
        }

        [Test]
        public void ModifyReputation_Above100_ClampsTo100()
        {
            _npcManager.ModifyReputation("Giocatore", 150);
            Assert.AreEqual(100, _npcManager.GetReputation("Giocatore"));
        }

        [Test]
        public void ModifyReputation_BelowMinus100_ClampsToMinus100()
        {
            _npcManager.ModifyReputation("Giocatore", -150);
            Assert.AreEqual(-100, _npcManager.GetReputation("Giocatore"));
        }

        [Test]
        public void IsFriend_ReputationAbove50_ReturnsTrue()
        {
            _npcManager.ModifyReputation("Amico", 60);
            Assert.IsTrue(_npcManager.IsFriend("Amico"));
        }

        [Test]
        public void IsFriend_ReputationBelow50_ReturnsFalse()
        {
            _npcManager.ModifyReputation("Nemico", 40);
            Assert.IsFalse(_npcManager.IsFriend("Nemico"));
        }

        [Test]
        public void GetReputation_UnknownNPC_ReturnsZero()
        {
            Assert.AreEqual(0, _npcManager.GetReputation("Sconosciuto"));
        }

        [Test]
        public void ModifyFactionReputation_ChangesFactionReputation()
        {
            _npcManager.ModifyFactionReputation(NPCFaction.Village, 30);
            Assert.AreEqual(80, _npcManager.GetFactionReputation(NPCFaction.Village));
        }

        [Test]
        public void GetFactionReputation_UnknownFaction_Returns50()
        {
            Assert.AreEqual(50, _npcManager.GetFactionReputation(NPCFaction.Tribe));
        }

        [Test]
        public void GetFactionReputation_Above100_Clamps()
        {
            _npcManager.ModifyFactionReputation(NPCFaction.Kingdom, 100);
            Assert.AreEqual(100, _npcManager.GetFactionReputation(NPCFaction.Kingdom));
        }

        [Test]
        public void AddNPC_AddsToRegistry()
        {
            var npc = new NPCData
            {
                name = "TestNPC",
                position = Vector3.zero,
                faction = NPCFaction.Village
            };

            _npcManager.AddNPC(npc);
            Assert.AreEqual(1, _npcManager.NPCs.Count);
        }

        [Test]
        public void Factions_AllDefined()
        {
            var factions = System.Enum.GetValues(typeof(NPCFaction));
            Assert.GreaterOrEqual(factions.Length, 5);
        }
    }
}
