using NUnit.Framework;
using UnityEngine;

namespace Knowledge.Game.Tests
{
    [TestFixture]
    public class WeatherSystemTests
    {
        private WeatherSystem _weather;
        private GameObject _go;

        [SetUp]
        public void Setup()
        {
            _go = new GameObject();
            _weather = _go.AddComponent<WeatherSystem>();
        }

        [TearDown]
        public void Teardown()
        {
            Object.DestroyImmediate(_go);
        }

        [Test]
        public void Instance_Set_SingleInstance()
        {
            Assert.AreEqual(_weather, WeatherSystem.Instance);
        }

        [Test]
        public void GetCurrentWeatherEffect_Default_ReturnsClear()
        {
            var effect = _weather.GetCurrentWeatherEffect();
            Assert.AreEqual(WeatherType.Clear, effect.Type);
        }

        [Test]
        public void GetWeatherDescription_Default_ReturnsClear()
        {
            string desc = _weather.GetWeatherDescription();
            StringAssert.Contains("Clear", desc);
        }

        [Test]
        public void IsDisasterActive_Default_ReturnsFalse()
        {
            Assert.IsFalse(_weather.IsDisasterActive);
        }

        [Test]
        public void WeatherEffect_Clear_NoSpeedModification()
        {
            var effect = _weather.GetCurrentWeatherEffect();
            Assert.AreEqual(1f, effect.MovementSpeedMod);
        }

        [Test]
        public void WeatherEffect_Rain_ReducesSpeed()
        {
            _weather.CurrentWeatherField = WeatherType.Rain;
            var effect = _weather.GetCurrentWeatherEffect();
            Assert.Less(effect.MovementSpeedMod, 1f);
        }

        [Test]
        public void WeatherEffect_Snow_ReducesSpeed()
        {
            _weather.CurrentWeatherField = WeatherType.Snow;
            var effect = _weather.GetCurrentWeatherEffect();
            Assert.Less(effect.MovementSpeedMod, 1f);
        }

        [Test]
        public void WeatherEffect_Storm_MaxReduction()
        {
            _weather.CurrentWeatherField = WeatherType.Storm;
            var effect = _weather.GetCurrentWeatherEffect();
            Assert.Less(effect.MovementSpeedMod, 0.5f);
        }

        [Test]
        public void WeatherEffect_Fog_ReducesVisibility()
        {
            _weather.CurrentWeatherField = WeatherType.Fog;
            var effect = _weather.GetCurrentWeatherEffect();
            Assert.Less(effect.VisibilityMod, 0.5f);
        }

        [Test]
        public void WeatherEffect_Heat_DrainsHealth()
        {
            _weather.CurrentWeatherField = WeatherType.Heat;
            var effect = _weather.GetCurrentWeatherEffect();
            Assert.Greater(effect.HealthDrain, 0);
        }

        [Test]
        public void WeatherEffect_Storm_DrainsHealthAndEnergy()
        {
            _weather.CurrentWeatherField = WeatherType.Storm;
            var effect = _weather.GetCurrentWeatherEffect();
            Assert.Greater(effect.HealthDrain, 0);
            Assert.Greater(effect.EnergyDrain, 0);
        }

        [Test]
        public void GetWeatherDescription_Storm_ReturnsStorm()
        {
            _weather.CurrentWeatherField = WeatherType.Storm;
            string desc = _weather.GetWeatherDescription();
            StringAssert.Contains("Storm", desc);
        }

        [Test]
        public void WeatherTypes_AllDefined()
        {
            var types = System.Enum.GetValues(typeof(WeatherType));
            Assert.Greater(types.Length, 0);
        }

        [Test]
        public void DisasterTypes_AllDefined()
        {
            var types = System.Enum.GetValues(typeof(DisasterType));
            Assert.Greater(types.Length, 0);
        }
    }
}
