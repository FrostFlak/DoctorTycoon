using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LoaderProgressBar : MonoBehaviour
    {
        [SerializeField] private Image _barFill;

        private void Awake()
        {
            _barFill.fillAmount = 0f;
        }
        private void Update()
        {
            FillProgressBar();
        }
        private void FillProgressBar()
        {
            _barFill.fillAmount += Time.deltaTime;
        }
    }

}
