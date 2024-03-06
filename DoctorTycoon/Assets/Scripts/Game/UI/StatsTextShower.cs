using UnityEngine;
using TMPro;
using Player;
using Unity.VisualScripting;

namespace UI
{
    public class StatsTextShower : MonoBehaviour
    {
        [SerializeField] private TMP_Text _moneyText;
        [SerializeField] private TMP_Text _patientsText;
        [SerializeField] private TMP_Text _personalText;
        private FormatNumsHelper _formatNumsHelper = new();
        public void Initialize()
        {
            UpdateMoneyText();
        }

        private void Start()
        {
            EventsManager.Instance.OnMoneyAdded += UpdateMoneyText;
            EventsManager.Instance.OnPatientSpawned += UpdateClientsText;
            EventsManager.Instance.OnPatientLeaveHospital += UpdateClientsText;

        }

        private void OnDisable()
        {
            EventsManager.Instance.OnMoneyAdded -= UpdateMoneyText;
            EventsManager.Instance.OnPatientSpawned -= UpdateClientsText;
            EventsManager.Instance.OnPatientLeaveHospital -= UpdateClientsText;
        }
        private void UpdateMoneyText()
        { 
            _moneyText.text = _formatNumsHelper.FormatNum(SaveSystem._playerData.Money);
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
