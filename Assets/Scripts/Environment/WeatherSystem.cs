using System.Collections.Generic;
using UnityEngine;

namespace Knowledge.Game
{
    public enum WeatherType { Clear, Rain, Snow, Wind, Fog, Heat, Storm }
    public enum DisasterType { None, Lightning, Landslide, Flood, Fire, Earthquake, VolcanicEruption }

    [System.Serializable]
    public readonly struct WeatherEffect
    {
        public WeatherType Type { get; }
        public float MovementSpeedMod { get; }
        public float VisibilityMod { get; }
        public float HealthDrain { get; }
        public float EnergyDrain { get; }

        public WeatherEffect(WeatherType type, float moveSpeed = 1f, float visibility = 1f, float healthDrain = 0f, float energyDrain = 0f)
        {
            Type = type;
            MovementSpeedMod = moveSpeed;
            VisibilityMod = visibility;
            HealthDrain = healthDrain;
            EnergyDrain = energyDrain;
        }

        public static WeatherEffect Clear => new(WeatherType.Clear);
        public static WeatherEffect Rain => new(WeatherType.Rain, 0.8f, 0.7f, 0f, 0.5f);
        public static WeatherEffect Snow => new(WeatherType.Snow, 0.6f, 0.6f, 1f, 0f);
        public static WeatherEffect Wind => new(WeatherType.Wind, 0.7f, 0.8f);
        public static WeatherEffect Fog => new(WeatherType.Fog, 0.9f, 0.3f);
        public static WeatherEffect Heat => new(WeatherType.Heat, 0.9f, 0.9f, 2f);
        public static WeatherEffect Storm => new(WeatherType.Storm, 0.4f, 0.2f, 3f, 2f);
    }

    public sealed class WeatherSystem : MonoBehaviour
    {
        public static WeatherSystem Instance { get; private set; }

        [Header("Weather Settings")]
        [SerializeField] private WeatherType currentWeather = WeatherType.Clear;
        [SerializeField] private DisasterType currentDisaster = DisasterType.None;
        
        [Header("Weather Effects")]
        [SerializeField] private float weatherChangeInterval = 60f;
        [SerializeField] private float disasterChance = 0.001f;
        
        [Header("Intensity")]
        [SerializeField] [Range(0, 1)] private float weatherIntensity = 0.5f;
        [SerializeField] [Range(0, 1)] private float disasterIntensity = 0.3f;

        [Header("Visual")]
        [SerializeField] private ParticleSystem rainParticles;
        [SerializeField] private ParticleSystem snowParticles;
        [SerializeField] private Light directionalLight;

        private float weatherTimer;
        private float disasterTimer;
        private readonly Dictionary<WeatherType, WeatherEffect> weatherEffects;

        public WeatherType CurrentWeather => currentWeather;
        public DisasterType CurrentDisaster => currentDisaster;
        public bool IsDisasterActive => currentDisaster != DisasterType.None;

        private WeatherSystem()
        {
            weatherEffects = new Dictionary<WeatherType, WeatherEffect>
            {
                { WeatherType.Clear, WeatherEffect.Clear },
                { WeatherType.Rain, WeatherEffect.Rain },
                { WeatherType.Snow, WeatherEffect.Snow },
                { WeatherType.Wind, WeatherEffect.Wind },
                { WeatherType.Fog, WeatherEffect.Fog },
                { WeatherType.Heat, WeatherEffect.Heat },
                { WeatherType.Storm, WeatherEffect.Storm }
            };
        }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
        }

        private void Update()
        {
            if (GameManager.Instance?.IsPaused == true) return;

            weatherTimer += Time.deltaTime;
            if (weatherTimer >= weatherChangeInterval)
            {
                weatherTimer = 0;
                ChangeWeather();
            }

            disasterTimer += Time.deltaTime;
            if (disasterTimer > 30f)
            {
                disasterTimer = 0;
                TryTriggerDisaster();
            }

            ApplyWeatherEffects();
        }

        private void ChangeWeather()
        {
            var weatherTypes = (WeatherType[])System.Enum.GetValues(typeof(WeatherType));
            currentWeather = weatherTypes[Random.Range(0, weatherTypes.Length)];
            UpdateWeatherVisuals();
            Debug.Log($"Weather changed to: {currentWeather}");
        }

        private void TryTriggerDisaster()
        {
            if (Random.value < disasterChance * disasterIntensity)
            {
                var disasters = new[] { DisasterType.Lightning, DisasterType.Landslide, DisasterType.Flood, DisasterType.Fire };
                currentDisaster = disasters[Random.Range(0, disasters.Length)];
                TriggerDisaster(currentDisaster);
            }
            else
            {
                currentDisaster = DisasterType.None;
            }
        }

        private void TriggerDisaster(DisasterType disaster)
        {
            Debug.LogWarning($"DISASTER: {disaster}!");
            GameManager.Instance?.Player?.ModifySocialStatus(-5);

            switch (disaster)
            {
                case DisasterType.Lightning:
                    HandleLightningStrike();
                    break;
                case DisasterType.Landslide:
                    Debug.LogWarning("Landslide! Seek higher ground!");
                    break;
                case DisasterType.Flood:
                    Debug.LogWarning("Flood! Move to higher ground!");
                    break;
                case DisasterType.Fire:
                    Debug.LogWarning("Fire spreading! Find water or evacuate!");
                    break;
            }
        }

        private void HandleLightningStrike()
        {
            if (Random.value < 0.1f)
                Debug.LogWarning("You were struck by lightning!");
        }

        private void ApplyWeatherEffects()
        {
            if (GameManager.Instance?.Player == null) return;
            
            var effect = GetCurrentWeatherEffect();
            
            if (effect.HealthDrain > 0)
                Debug.Log($"Weather draining health: {effect.HealthDrain}");
        }

        private void UpdateWeatherVisuals()
        {
            if (rainParticles != null)
                rainParticles.gameObject.SetActive(currentWeather is WeatherType.Rain or WeatherType.Storm);
            
            if (snowParticles != null)
                snowParticles.gameObject.SetActive(currentWeather == WeatherType.Snow);
        }

        public WeatherEffect GetCurrentWeatherEffect()
        {
            return weatherEffects.TryGetValue(currentWeather, out var effect) ? effect : WeatherEffect.Clear;
        }

        public string GetWeatherDescription() => $"{currentWeather} {currentDisaster}";
    }
}
