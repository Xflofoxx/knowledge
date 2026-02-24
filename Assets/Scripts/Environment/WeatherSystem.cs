using System.Collections.Generic;
using UnityEngine;

namespace Knowledge.Game
{
    public enum WeatherType { Clear, Rain, Snow, Wind, Fog, Heat, Storm }
    public enum DisasterType { None, Lightning, Landslide, Flood, Fire, Earthquake, VolcanicEruption }

    [System.Serializable]
    public struct WeatherEffect
    {
        public WeatherType type;
        public float movementSpeedMod = 1f;
        public float visibilityMod = 1f;
        public float healthDrain;
        public float energyDrain;
    }

    public class WeatherSystem : MonoBehaviour
    {
        public static WeatherSystem Instance { get; private set; }

        [Header("Weather Settings")]
        public WeatherType currentWeather = WeatherType.Clear;
        public DisasterType currentDisaster = DisasterType.None;
        
        [Header("Weather Effects")]
        public float weatherChangeInterval = 60f;
        public float disasterChance = 0.001f;
        
        [Header("Intensity")]
        [Range(0, 1)] public float weatherIntensity = 0.5f;
        [Range(0, 1)] public float disasterIntensity = 0.3f;

        [Header("Visual")]
        public ParticleSystem rainParticles;
        public ParticleSystem snowParticles;
        public Light directionalLight;

        private float weatherTimer = 0f;
        private float disasterTimer = 0f;
        private Dictionary<WeatherType, WeatherEffect> weatherEffects;

        private void Awake()
        {
            if (Instance != null) return;
            Instance = this;
            InitializeWeatherEffects();
        }

        private void InitializeWeatherEffects()
        {
            weatherEffects = new Dictionary<WeatherType, WeatherEffect>
            {
                { WeatherType.Clear, new WeatherEffect { type = WeatherType.Clear, movementSpeedMod = 1f, visibilityMod = 1f } },
                { WeatherType.Rain, new WeatherEffect { type = WeatherType.Rain, movementSpeedMod = 0.8f, visibilityMod = 0.7f, energyDrain = 0.5f } },
                { WeatherType.Snow, new WeatherEffect { type = WeatherType.Snow, movementSpeedMod = 0.6f, visibilityMod = 0.6f, healthDrain = 1f } },
                { WeatherType.Wind, new WeatherEffect { type = WeatherType.Wind, movementSpeedMod = 0.7f, visibilityMod = 0.8f } },
                { WeatherType.Fog, new WeatherEffect { type = WeatherType.Fog, movementSpeedMod = 0.9f, visibilityMod = 0.3f } },
                { WeatherType.Heat, new WeatherEffect { type = WeatherType.Heat, movementSpeedMod = 0.9f, visibilityMod = 0.9f, healthDrain = 2f } },
                { WeatherType.Storm, new WeatherEffect { type = WeatherType.Storm, movementSpeedMod = 0.4f, visibilityMod = 0.2f, healthDrain = 3f, energyDrain = 2f } }
            };
        }

        private void Update()
        {
            if (GameManager.Instance.gamePaused) return;

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
            var weatherTypes = System.Enum.GetValues(typeof(WeatherType));
            currentWeather = (WeatherType)weatherTypes.GetValue(Random.Range(0, weatherTypes.Length));
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
            GameManager.Instance.Player?.ModifySocialStatus(-5);

            switch (disaster)
            {
                case DisasterType.Lightning:
                    HandleLightningStrike();
                    break;
                case DisasterType.Landslide:
                    HandleLandslide();
                    break;
                case DisasterType.Flood:
                    HandleFlood();
                    break;
                case DisasterType.Fire:
                    HandleFire();
                    break;
            }
        }

        private void HandleLightningStrike()
        {
            if (Random.value < 0.1f)
            {
                var player = GameManager.Instance.Player;
                if (player != null)
                {
                    Debug.LogWarning("You were struck by lightning!");
                }
            }
        }

        private void HandleLandslide()
        {
            Debug.LogWarning("Landslide! Seek higher ground!");
        }

        private void HandleFlood()
        {
            Debug.LogWarning("Flood! Move to higher ground!");
        }

        private void HandleFire()
        {
            Debug.LogWarning("Fire spreading! Find water or evacuate!");
        }

        private void ApplyWeatherEffects()
        {
            var effect = weatherEffects[currentWeather];
            var player = GameManager.Instance.Player;
            if (player == null) return;

            if (effect.healthDrain > 0)
                player.GetComponent<UnityEngine.UI.Text>();
        }

        private void UpdateWeatherVisuals()
        {
            if (rainParticles != null)
                rainParticles.gameObject.SetActive(currentWeather == WeatherType.Rain || currentWeather == WeatherType.Storm);
            
            if (snowParticles != null)
                snowParticles.gameObject.SetActive(currentWeather == WeatherType.Snow);
        }

        public WeatherEffect GetCurrentWeatherEffect()
        {
            return weatherEffects.ContainsKey(currentWeather) ? weatherEffects[currentWeather] : weatherEffects[WeatherType.Clear];
        }

        public string GetWeatherDescription()
        {
            return $"{currentWeather} {currentDisaster}";
        }

        public bool IsDisasterActive()
        {
            return currentDisaster != DisasterType.None;
        }
    }
}
