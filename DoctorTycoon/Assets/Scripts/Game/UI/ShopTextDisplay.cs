using People;
using System;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ShopTextDisplay : MonoBehaviour
    {
        [Header("Beds")]
        [SerializeField] private TMP_Text _bedPrice;
        [SerializeField] private TMP_Text _bedCount;
        [SerializeField] private TMP_Text _bedUpgradePrice;
        [SerializeField] private TMP_Text _bedUpgradedSpeed;
        [SerializeField] private BedManager _bedManager;
        [SerializeField] private BedPurchaseSystem _bedPurchaseSystem;
        [Header("Registartion")]
        [SerializeField] private RegistartionTableUpgrader _registartionTableUpgrader;
        [SerializeField] private TMP_Text _registartionUpgradePrice;
        [SerializeField] private TMP_Text _registartionUpgradeSpeed;



        private void Start()
        {
            EventsManager.Instance.OnBedPurchased += ShowBedsCount;
            EventsManager.Instance.OnBedPurchased += ShowBedsPrice;
            EventsManager.Instance.OnBedUpgraded += ShowBedsUpgradePrice;
            EventsManager.Instance.OnBedUpgraded += ShowBedsUpgradedSpeed;
            EventsManager.Instance.OnRegistrationUpgraded += ShowRegistartionUpgradedPrice;
            EventsManager.Instance.OnRegistrationUpgraded += ShowRegistartionUpgradedSpeed;
        }

        private void OnDisable()
        {
            EventsManager.Instance.OnBedPurchased -= ShowBedsCount;
            EventsManager.Instance.OnBedPurchased -= ShowBedsPrice;
            EventsManager.Instance.OnBedUpgraded -= ShowBedsUpgradePrice;
            EventsManager.Instance.OnBedUpgraded -= ShowBedsUpgradedSpeed;
            EventsManager.Instance.OnRegistrationUpgraded -= ShowRegistartionUpgradedPrice;
            EventsManager.Instance.OnRegistrationUpgraded -= ShowRegistartionUpgradedSpeed;
        }

        public void Initialize()
        {
            ShowBedsCount();
            ShowBedsPrice();
            ShowBedsUpgradePrice();
            ShowBedsUpgradedSpeed();
            ShowRegistartionUpgradedPrice();
            ShowRegistartionUpgradedSpeed();
        }
        private void ShowBedsCount() => _bedCount.text = _bedManager.CurrentPurchasedBedsCount.ToString();
        private void ShowBedsPrice() => _bedPrice.text = _bedPurchaseSystem.BedPrice.ToString() + "$";
        private void ShowBedsUpgradePrice() => _bedUpgradePrice.text = _bedPurchaseSystem.BedUpgradePrice.ToString() + "$";
        private void ShowBedsUpgradedSpeed() => _bedUpgradedSpeed.text = "1 Client / " + MathF.Round(_bedPurchaseSystem.CurrentHealSpeed , 2 , MidpointRounding.ToEven).ToString() + " sec";
        private void ShowRegistartionUpgradedPrice() => _registartionUpgradePrice.text = _registartionTableUpgrader.RegistrationUpgradePrice.ToString() + "$";
        private void ShowRegistartionUpgradedSpeed() => _registartionUpgradeSpeed.text = "1 Client / " + MathF.Round(_registartionTableUpgrader.CurrentRegistrationSpeed, 2, MidpointRounding.ToEven).ToString() + " sec";
    }
}