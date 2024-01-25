using UnityEngine;
using TMPro;
using Player;

namespace UI
{
    public class StatsTextShower : MonoBehaviour
    {
        [SerializeField] private TMP_Text _moneyText;
        [SerializeField] private TMP_Text _patientsText;
        [SerializeField] private TMP_Text _personalText;
        [SerializeField] private MoneyWallet _moneyWallet;
        [SerializeField] private FormatNumsHelper _formatNumsHelper = new();
        public void Initialize()
        {
            UpdateMoneyText();
        }
        private void OnEnable()
        {
            _moneyWallet.OnMoneyAdded += UpdateMoneyText;
        }

        private void OnDisable()
        {
            _moneyWallet.OnMoneyAdded -= UpdateMoneyText;
        }
        private void UpdateMoneyText()
        { 
            _moneyText.text = _formatNumsHelper.FormatNum(SaveSystem._playerData.Money) + " $";
        }

    }

}