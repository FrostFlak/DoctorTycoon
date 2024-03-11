using UnityEngine;
using TMPro;
using Player;

namespace UI
{
    public class StatsTextShower : MonoBehaviour
    {
        [SerializeField] private TMP_Text _moneyText;
        [SerializeField] private TMP_Text _pillsText;
        [SerializeField] private TMP_Text _patientsText;
        [SerializeField] private TMP_Text _personalText;
        private FormatNumsHelper _formatNumsHelper = new();
        public void Initialize()
        {
            UpdateMoneyText();
            UpdatePillsText();
        }

        private void Start()
        {
            EventsManager.Instance.OnMoneyAdded += UpdateMoneyText;
            EventsManager.Instance.OnPillsAdded += UpdatePillsText;
            EventsManager.Instance.OnPatientSpawned += UpdateClientsText;
            EventsManager.Instance.OnPatientLeaveHospital += UpdateClientsText;
            EventsManager.Instance.OnDataReseted += UpdateMoneyText;

        }

        private void OnDisable()
        {
            EventsManager.Instance.OnMoneyAdded -= UpdateMoneyText;
            EventsManager.Instance.OnPillsAdded -= UpdatePillsText;
            EventsManager.Instance.OnPatientSpawned -= UpdateClientsText;
            EventsManager.Instance.OnPatientLeaveHospital -= UpdateClientsText;
            EventsManager.Instance.OnDataReseted += UpdateMoneyText;

        }
        private void UpdateMoneyText()
        { 
            _moneyText.text = _formatNumsHelper.FormatNum(SaveSystem._playerData.Money);
        }

        private void UpdatePillsText()
        {
            _pillsText.text = SaveSystem._playerData.Pills.ToString();
        }

        private void UpdateClientsText(int count)
        {
            _patientsText.text = count.ToString();
        }
        private void UpdatePersonalText()
        {
            //personal
        }

    }

}
