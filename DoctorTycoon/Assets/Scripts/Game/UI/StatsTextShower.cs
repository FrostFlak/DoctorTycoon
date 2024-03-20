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
            EventsManager.Instance.OnMoneyValueChanged += UpdateMoneyText;
            EventsManager.Instance.OnPillsValueChanged += UpdatePillsText;
            EventsManager.Instance.OnPatientEnterHospital += UpdateClientsText;
            EventsManager.Instance.OnPatientLeaveHospital += UpdateClientsText;
            EventsManager.Instance.OnDataReseted += UpdateMoneyText;
            EventsManager.Instance.OnDataReseted += UpdatePillsText;
        }

        private void OnDisable()
        {
            EventsManager.Instance.OnMoneyValueChanged -= UpdateMoneyText;
            EventsManager.Instance.OnPillsValueChanged -= UpdatePillsText;
            EventsManager.Instance.OnPatientEnterHospital -= UpdateClientsText;
            EventsManager.Instance.OnPatientLeaveHospital -= UpdateClientsText;
            EventsManager.Instance.OnDataReseted += UpdateMoneyText;
            EventsManager.Instance.OnDataReseted -= UpdatePillsText;
        }
        private void UpdateMoneyText()
        { 
            _moneyText.text = _formatNumsHelper.FormatNum(SaveSystem.PlayerData.Money);
        }

        private void UpdatePillsText()
        {
            _pillsText.text = SaveSystem.PlayerData.Pills.ToString();
        }

        private void UpdateClientsText(int count)
        {
            _patientsText.text = count.ToString();
        }


    }

}
