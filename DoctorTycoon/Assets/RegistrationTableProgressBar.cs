using Player;
using UnityEngine;
using UnityEngine.UI;

namespace People
{
    public class RegistrationTableProgressBar : MonoBehaviour
{
        [SerializeField] private RegistrationTable _table;
        [SerializeField] private Image _progressBar;

        public void Initialize()
        {
            _progressBar.fillAmount = 0f;
        }
        private void Start()
        {
            EventsManager.Instance.OnStayInTriggerZone += IncreaseProgressBar;
        }

        private void OnDisable()
        {
            EventsManager.Instance.OnStayInTriggerZone -= IncreaseProgressBar;
        }

        private void Update()
        {
            if(!_table.InZone)
            {
                DecreaseProgressBar();
            }
        }
        private void IncreaseProgressBar()
        {
            _progressBar.fillAmount = (_table.AcceptClientProgress / _table.TimeToAcceptClient) / 1f;
            if(_progressBar.fillAmount == 1)
            {
                _progressBar.fillAmount = 0f;
            }
        }
        private void DecreaseProgressBar()
        {
            _progressBar.fillAmount = (_table.AcceptClientProgress / _table.TimeToAcceptClient) / 1f;

        }
    }

}
