using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using Player;

namespace UI
{
    public class SensitivityShower : MonoBehaviour
    {
        [SerializeField] private TMP_Text _sliderValue;
        [SerializeField] private Slider _slider;
        [SerializeField] private CharacterMovmentFirstPersonView _firstPersonController;

        public void Initialize()
        {
            _firstPersonController.LookRotationSpeedProperty = _slider.value;
            _sliderValue.text = (Math.Round(_slider.value, 2)).ToString();
        }
        public void ShowSliderValue()
        {
            if (_slider != null)
            {
                _sliderValue.text = (Math.Round(_slider.value , 2)).ToString();
            }
        }

        public void ChangeSensitivity()
        {
            _firstPersonController.LookRotationSpeedProperty = _slider.value;
        }


    }



}
