using Player;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;
using UnityEngine.UI;

namespace UI
{
    public class WelcomeTextDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textComponent;
        [SerializeField] private TMP_Text _continueComponent;
        [SerializeField] private Button _continueButton;
        [SerializeField] private float _letterDelay;
        [SerializeField, TextArea(7, 3)] private string _enText;
        [SerializeField, TextArea(7, 3)] private string _ruText;
        [SerializeField, TextArea(7, 3)] private string _trText;
        [SerializeField] private FloatingJoystick _joystick;
        [SerializeField] private Button[] _allButtons;
        private string _fullText;
        private string _name;
        private Locale _currentSelectedLocale;
        private ILocalesProvider _availableLocales;
        private void Start()
        {
            _currentSelectedLocale = LocalizationSettings.SelectedLocale;
            _availableLocales = LocalizationSettings.AvailableLocales;
            if (_currentSelectedLocale == _availableLocales.GetLocale("en")) _fullText = _enText;
            else if(_currentSelectedLocale == _availableLocales.GetLocale("ru")) _fullText = _ruText; 
            else if(_currentSelectedLocale == _availableLocales.GetLocale("tr")) _fullText = _trText; 

            _textComponent.text = "";
            _name = SaveSystem.PlayerData.Name;
        }
        private void OnEnable()
        {
            _joystick.TurnOffJoystick();
            _continueComponent.gameObject.SetActive(false);
            for (int i = 0; i < _allButtons.Length; i++)
                _allButtons[i].interactable = false;
            StartCoroutine(DisplayText());
        }

        private IEnumerator DisplayText()
        {
            yield return new WaitForSeconds(_letterDelay);
            for (int i = 0; i < _fullText.Length; i++)
            {
                if (_fullText[i] == '{')
                {
                    _textComponent.text += _name;
                    yield return new WaitForSeconds(_letterDelay);
                }
                else
                {
                    _textComponent.text += _fullText[i];
                    yield return new WaitForSeconds(_letterDelay);
                }
            }
            _continueComponent.gameObject.SetActive(true);
            _continueButton.interactable = true;
        }
    }
}