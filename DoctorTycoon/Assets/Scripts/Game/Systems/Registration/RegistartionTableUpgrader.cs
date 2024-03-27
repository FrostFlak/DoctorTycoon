using Player;
using System.Linq;
using UnityEngine;

namespace People
{
    public class RegistartionTableUpgrader : MonoBehaviour
    {
        [SerializeField] private RegistrationTable _registartionTable;
        [SerializeField] private int _registrationUpgradePrice;
        [SerializeField] private float _registrationUpgradeSpeed;
        [SerializeField] private float _priceMultiplier = 2;
        private float _currentRegistartionSpeed;

        public int RegistrationUpgradePrice
        {
            get { return _registrationUpgradePrice; }
            set { _registrationUpgradePrice = value; }
        }
        public float CurrentRegistrationSpeed { get { return _currentRegistartionSpeed; } }
        public void Initialize()
        {
            _registrationUpgradePrice = SaveSystem.ShopData.CurrentUpgradeRegistartionPrice;
            _registartionTable.TimeToAcceptClient = SaveSystem.ShopData.CurrentUpgradeRegistartionSpeed;
            _currentRegistartionSpeed = SaveSystem.ShopData.CurrentUpgradeRegistartionSpeed;
        }

        public void UpgradeRegistartionTable()
        {
            SaveSystem.ShopData.CurrentUpgradeRegistartionSpeed -= _registrationUpgradeSpeed;
            SaveSystem.ShopData.CurrentUpgradeRegistartionPrice = _registrationUpgradePrice;
            ReduceMoneyCount(_registrationUpgradePrice);
            Debug.Log($"Upgraded Registartion. Price: {_registrationUpgradePrice}");
            _registrationUpgradePrice = MultiplyPrice(_registrationUpgradePrice, _priceMultiplier);
            SaveSystem.ShopData.CurrentUpgradeRegistartionPrice = _registrationUpgradePrice;
            _currentRegistartionSpeed = SaveSystem.ShopData.CurrentUpgradeRegistartionSpeed;
            EventsManager.Instance.OnRegistartionUpgradedEvent();
            EventsManager.Instance.OnMoneyValueChangedEvent();
        }

        private int MultiplyPrice(int price, float multiplier)
        {
            return Mathf.RoundToInt(price * multiplier);
        }
        private void ReduceMoneyCount(int price) => SaveSystem.PlayerData.Money -= price;
    }
}