using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Shooter
{
    /// <summary>
    /// Component that interacts directly with UI/Screen
    /// plan to improve/note: try to separate logic with ui, maybe use event in the future?
    /// </summary>
    public sealed class PlayerUI : MonoBehaviour
    {
        public void Initialize(Character character)
        {
            _character = character;
            healthSlider.value = healthSlider.maxValue = _character.CurrentHealth;
        }

        public void OnRegenerate()
        {
            StopCoroutine(HealthSliderCoroutine());
            StartCoroutine(HealthSliderCoroutine());
        }

        public void OnHurt()
        {
            StopAllCoroutines();
            StartCoroutine(HealthSliderCoroutine());
            StartCoroutine(DamageImageCoroutine());
        }

        private IEnumerator HealthSliderCoroutine()
        {
            float startingValue = healthSlider.value;
            int finalValue = _character.CurrentHealth;

            float _time = 0f;
            float _percentage;

            while (_time < HEALTH_SLIDER_LERP_DURATION)
            {
                _time += Time.deltaTime;
                if (_time >= HEALTH_SLIDER_LERP_DURATION)
                {
                    _time = HEALTH_SLIDER_LERP_DURATION;
                }

                _percentage = _time / HEALTH_SLIDER_LERP_DURATION;
                healthSlider.value = Mathf.Lerp(startingValue, finalValue, _percentage);

                yield return null;
            }
        }

        private IEnumerator DamageImageCoroutine()
        {
            damageImage.color = flashColour; //Visualize first

            float _time = 0f;
            float _percentage;

            while (_time < flashDuration)
            {
                _time += Time.deltaTime;
                if (_time >= flashDuration)
                {
                    _time = flashDuration;
                }

                _percentage = _time / flashDuration;
                damageImage.color = Color.Lerp(flashColour, Color.clear, _percentage); //slowly recover screen

                yield return null;
            }
        }

        [SerializeField] private Slider healthSlider = default;
        [SerializeField] private Image damageImage = default;

        [Space]
        [SerializeField] private Color flashColour = default;
        [SerializeField] private float flashDuration = 5f;

        private Character _character;
        private const float HEALTH_SLIDER_LERP_DURATION = .4f;
    }
}