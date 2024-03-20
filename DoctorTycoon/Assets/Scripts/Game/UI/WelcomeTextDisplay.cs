using Player;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class WelcomeTextDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textComponent;
        [SerializeField] private TMP_Text _continueComponent;
        [SerializeField] private Button _continueButton;
        [SerializeField] private float _letterDelay;
        [SerializeField, TextArea(10, 3)] private string _fullText;
        [SerializeField] private FloatingJoystick _joystick;
        [SerializeField] private Button[] _allButtons;
        private string _name;
        private string _gender;
        private void Start()
        {
            _fullText = _textComponent.text;
            _textComponent.text = "";
            _name = SaveSystem.PlayerData.Name;
            if (SaveSystem.PlayerData.Gender)
                _gender = "Mr.";
            else
                _gender = "Mrs.";

        }
        private void OnEnable()
        {
            _joystick.SwitchToFalseInteractability();
            for(int i = 0; i < _allButtons.Length; i++)
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
                    _textComponent.text += _gender + _name;
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