using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

namespace UI
{
    public class TextDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textComponent;
        [SerializeField] private TMP_Text _continueComponent;
        [SerializeField] private Button _continueButton;
        [SerializeField] private float _letterDelay;
        [SerializeField, TextArea(7, 3)] private string _enText;
        [SerializeField, TextArea(7, 3)] private string _ruText;
        [SerializeField, TextArea(7, 3)] private string _trText;
        private string _fullText;
        private Locale _currentSelectedLocale;
        private ILocalesProvider _availableLocales;
        private void Start()
        {
            _currentSelectedLocale = LocalizationSettings.SelectedLocale;
            _availableLocales = LocalizationSettings.AvailableLocales;
            if (_currentSelectedLocale == _availableLocales.GetLocale("en")) _fullText = _enText;
            else if (_currentSelectedLocale == _availableLocales.GetLocale("ru")) _fullText = _ruText;
            else if (_currentSelectedLocale == _availableLocales.GetLocale("tr")) _fullText = _trText;
            _textComponent.text = ""; 
        }
        private void OnEnable()
        {
            _continueComponent.gameObject.SetActive(false);
            StartCoroutine(DisplayText());
        }

        private IEnumerator DisplayText()
        {
            yield return new WaitForSeconds(_letterDelay);
            for (int i = 0; i < _fullText.Length; i++)
            {
                _textComponent.text += _fullText[i];
                yield return new WaitForSeconds(_letterDelay);
            }
            _continueComponent.gameObject.SetActive(true);
            _continueButton.interactable = true;
        }
    }
}