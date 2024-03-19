using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TextDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textComponent;
        [SerializeField] private TMP_Text _continueComponent;
        [SerializeField] private Button _continueButton;
        [SerializeField] private float _letterDelay;
        [SerializeField,TextArea(10 , 3)] private string _fullText;

        private void Start()
        {
            _fullText = _textComponent.text;
            _textComponent.text = ""; 
        }
        private void OnEnable()
        {
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