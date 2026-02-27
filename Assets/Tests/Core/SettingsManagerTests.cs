using NUnit.Framework;
using UnityEngine;

namespace Knowledge.Tests.Core
{
    [TestFixture]
    public class SettingsManagerTests
    {
        private SettingsManager settingsManager;

        [SetUp]
        public void Setup()
        {
            var gameObject = new GameObject("SettingsManager");
            settingsManager = gameObject.AddComponent<SettingsManager>();
        }

        [TearDown]
        public void Teardown()
        {
            Object.DestroyImmediate(settingsManager.gameObject);
        }

        [Test]
        public void GetMasterVolume_Default_ReturnsValidValue()
        {
            Assert.GreaterOrEqual(settingsManager.MasterVolume, 0f);
            Assert.LessOrEqual(settingsManager.MasterVolume, 1f);
        }

        [Test]
        public void SetMasterVolume_ValidValue_ChangesVolume()
        {
            settingsManager.MasterVolume = 0.5f;
            Assert.AreEqual(0.5f, settingsManager.MasterVolume);
        }

        [Test]
        public void SetMasterVolume_OutOfRange_ClampsToValidRange()
        {
            settingsManager.MasterVolume = 1.5f;
            Assert.AreEqual(1f, settingsManager.MasterVolume);

            settingsManager.MasterVolume = -0.5f;
            Assert.AreEqual(0f, settingsManager.MasterVolume);
        }

        [Test]
        public void GetMusicVolume_Default_ReturnsValidValue()
        {
            Assert.GreaterOrEqual(settingsManager.MusicVolume, 0f);
            Assert.LessOrEqual(settingsManager.MusicVolume, 1f);
        }

        [Test]
        public void SetSfxVolume_ValidValue_ChangesVolume()
        {
            settingsManager.SfxVolume = 0.8f;
            Assert.AreEqual(0.8f, settingsManager.SfxVolume);
        }

        [Test]
        public void GetQualityLevel_Default_ReturnsValidValue()
        {
            int qualityLevel = settingsManager.QualityLevel;
            Assert.GreaterOrEqual(qualityLevel, 0);
            Assert.Less(qualityLevel, QualitySettings.names.Length);
        }

        [Test]
        public void SetQualityLevel_ValidValue_ChangesQuality()
        {
            settingsManager.QualityLevel = 2;
            Assert.AreEqual(2, settingsManager.QualityLevel);
        }

        [Test]
        public void IsVsyncEnabled_Default_ReturnsFalse()
        {
            Assert.IsFalse(settingsManager.IsVsyncEnabled);
        }

        [Test]
        public void SetVsyncEnabled_True_EnablesVsync()
        {
            settingsManager.IsVsyncEnabled = true;
            Assert.IsTrue(settingsManager.IsVsyncEnabled);
        }

        [Test]
        public void GetResolution_Default_ReturnsValidResolution()
        {
            Vector2Int resolution = settingsManager.Resolution;
            Assert.Greater(resolution.x, 0);
            Assert.Greater(resolution.y, 0);
        }

        [Test]
        public void SetResolution_ValidValue_ChangesResolution()
        {
            settingsManager.Resolution = new Vector2Int(1920, 1080);
            Assert.AreEqual(new Vector2Int(1920, 1080), settingsManager.Resolution);
        }

        [Test]
        public void GetDifficulty_Default_ReturnsNormal()
        {
            Assert.AreEqual(Difficulty.Normal, settingsManager.Difficulty);
        }

        [Test]
        public void SetDifficulty_Hard_SetsHardDifficulty()
        {
            settingsManager.Difficulty = Difficulty.Hard;
            Assert.AreEqual(Difficulty.Hard, settingsManager.Difficulty);
        }

        [Test]
        public void GetKeyBinding_Default_ReturnsValidKey()
        {
            KeyCode jumpKey = settingsManager.GetKeyBinding("Jump");
            Assert.AreNotEqual(KeyCode.None, jumpKey);
        }

        [Test]
        public void SetKeyBinding_ValidKey_ChangesBinding()
        {
            settingsManager.SetKeyBinding("Jump", KeyCode.Space);
            Assert.AreEqual(KeyCode.Space, settingsManager.GetKeyBinding("Jump"));
        }

        [Test]
        public void ResetToDefaults_ResetsAllSettings()
        {
            settingsManager.MasterVolume = 0.9f;
            settingsManager.QualityLevel = 0;
            settingsManager.Difficulty = Difficulty.Hard;

            settingsManager.ResetToDefaults();

            Assert.AreEqual(1f, settingsManager.MasterVolume);
            Assert.AreEqual(2, settingsManager.QualityLevel);
            Assert.AreEqual(Difficulty.Normal, settingsManager.Difficulty);
        }
    }
}
