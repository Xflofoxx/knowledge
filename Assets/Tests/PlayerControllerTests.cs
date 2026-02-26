using NUnit.Framework;
using UnityEngine;

namespace Knowledge.Game.Tests
{
    [TestFixture]
    public class PlayerControllerTests
    {
        private PlayerController _player;
        private GameObject _go;
        private CharacterController _cc;

        [SetUp]
        public void Setup()
        {
            _go = new GameObject();
            _cc = _go.AddComponent<CharacterController>();
            _player = _go.AddComponent<PlayerController>();
            _player.MoveSpeed = 5f;
            _player.SprintSpeed = 8f;
            _player.MaxHealth = 100f;
            _player.MaxEnergy = 100f;
            _player.MaxHunger = 100f;
            _player.MaxThirst = 100f;
            _player.MaxHappiness = 100f;
        }

        [TearDown]
        public void Teardown()
        {
            Object.DestroyImmediate(_go);
        }

        [Test]
        public void Health_InitialValue_MaxHealth()
        {
            Assert.AreEqual(_player.MaxHealth, _player.Health);
        }

        [Test]
        public void Energy_InitialValue_MaxEnergy()
        {
            Assert.AreEqual(_player.MaxEnergy, _player.Energy);
        }

        [Test]
        public void Hunger_InitialValue_MaxHunger()
        {
            Assert.AreEqual(_player.MaxHunger, _player.Hunger);
        }

        [Test]
        public void Thirst_InitialValue_MaxThirst()
        {
            Assert.AreEqual(_player.MaxThirst, _player.Thirst);
        }

        [Test]
        public void Eat_ValidAmount_IncreasesHunger()
        {
            float initialHunger = _player.Hunger;
            _player.Eat(20f);

            Assert.Greater(_player.Hunger, initialHunger);
        }

        [Test]
        public void Eat_ExceedsMax_ClampsToMax()
        {
            _player.Eat(200f);
            Assert.AreEqual(_player.MaxHunger, _player.Hunger);
        }

        [Test]
        public void Drink_ValidAmount_IncreasesThirst()
        {
            float initialThirst = _player.Thirst;
            _player.Drink(20f);

            Assert.Greater(_player.Thirst, initialThirst);
        }

        [Test]
        public void Rest_ValidAmount_IncreasesEnergy()
        {
            float initialEnergy = _player.Energy;
            _player.Rest(20f);

            Assert.Greater(_player.Energy, initialEnergy);
        }

        [Test]
        public void AddExperience_BelowThreshold_DoesNotLevelUp()
        {
            int initialLevel = _player.Level;
            _player.AddExperience(50f);

            Assert.AreEqual(initialLevel, _player.Level);
        }

        [Test]
        public void AddExperience_ExceedsThreshold_LevelsUp()
        {
            _player.AddExperience(150f);

            Assert.Greater(_player.Level, 1);
        }

        [Test]
        public void ModifySocialStatus_ValidAmount_ChangesStatus()
        {
            int initialStatus = _player.SocialStatus;
            _player.ModifySocialStatus(10);

            Assert.AreEqual(initialStatus + 10, _player.SocialStatus);
        }

        [Test]
        public void ModifySocialStatus_ExceedsMax_ClampsTo100()
        {
            _player.ModifySocialStatus(200);
            Assert.AreEqual(100, _player.SocialStatus);
        }

        [Test]
        public void ModifySocialStatus_BelowMin_ClampsTo1()
        {
            _player.ModifySocialStatus(-200);
            Assert.AreEqual(1, _player.SocialStatus);
        }
    }
}
