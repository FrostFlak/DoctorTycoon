using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LoaderProgressBar : MonoBehaviour
    {
        [SerializeField] private Image _barFill;
        [SerializeField] private LoaderSceneChanger _sceneChanger;

        private void Update()
        {
            FillProgressBar();
        }
        private void FillProgressBar()
        {
            _barFill.fillAmount = _sceneChanger.LoadOperation.progress + 0.9f;
        }
    }

}
