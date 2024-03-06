using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace UI
{
    public class SensitivityShower : MonoBehaviour
    {
        [SerializeField] private TMP_Text _sliderValue;
        [SerializeField] private Slider _slider;

        public Slider Slider { get { return _slider; } set {  _slider = value; } }
        public void Initialize()
        {
            _sliderValue.text = Math.Round(_slider.value, 2).ToString();
        }
        public void ShowSliderValue()
        {
            if (_slider != null)
            {
                _sliderValue.text = Math.Round(_slider.value, 2).ToString();
            }
        }


    }



}
