using People;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;

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


        private Locale _currentSelectedLocale;
        private ILocalesProvider _availableLocales;
        public BedPurchaseSystem BedPurchaseSystem { get { return _bedPurchaseSystem; } }


        private void Start()
        {
            EventsManager.Instance.OnBedPurchased += ShowBedsCount;
            EventsManager.Instance.OnBedPurchased += ShowBedsPrice;
            EventsManager.Instance.OnBedUpgraded += ShowBedsUpgradePrice;
            EventsManager.Instance.OnBedUpgraded += ShowBedsUpgradedSpeed;
            EventsManager.Instance.OnRegistrationUpgraded += ShowRegistartionUpgradedPrice;
            EventsManager.Instance.OnRegistrationUpgraded += ShowRegistartionUpgradedSpeed;
            EventsManager.Instance.OnLanguageChanged += ShowBedsUpgradedSpeed;
            EventsManager.Instance.OnLanguageChanged += ShowRegistartionUpgradedSpeed;
        }

        private void OnDisable()
        {
            EventsManager.Instance.OnBedPurchased -= ShowBedsCount;
            EventsManager.Instance.OnBedPurchased -= ShowBedsPrice;
            EventsManager.Instance.OnBedUpgraded -= ShowBedsUpgradePrice;
            EventsManager.Instance.OnBedUpgraded -= ShowBedsUpgradedSpeed;
            EventsManager.Instance.OnRegistrationUpgraded -= ShowRegistartionUpgradedPrice;
            EventsManager.Instance.OnRegistrationUpgraded -= ShowRegistartionUpgradedSpeed;
            EventsManager.Instance.OnLanguageChanged -= ShowBedsUpgradedSpeed;
            EventsManager.Instance.OnLanguageChanged -= ShowRegistartionUpgradedSpeed;
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
        private void ShowBedsUpgradedSpeed()
        {
            _currentSelectedLocale = LocalizationSettings.SelectedLocale;
            _availableLocales = LocalizationSettings.AvailableLocales;
            if (_currentSelectedLocale == _availableLocales.GetLocale("en"))
            {
                _bedUpgradedSpeed.fontStyle = FontStyles.Normal;
                _bedUpgradedSpeed.text = "1 Client / " + MathF.Round(_bedPurchaseSystem.CurrentHealSpeed, 2, MidpointRounding.ToEven).ToString() + " sec";
            }
            else if(_currentSelectedLocale == _availableLocales.GetLocale("ru"))
            {
                _bedUpgradedSpeed.fontStyle = FontStyles.Bold;
                _bedUpgradedSpeed.text = "1 Клиент / " + MathF.Round(_bedPurchaseSystem.CurrentHealSpeed, 2, MidpointRounding.ToEven).ToString() + " сек";
            }
            else if(_currentSelectedLocale == _availableLocales.GetLocale("tr"))
            {
                _bedUpgradedSpeed.fontStyle = FontStyles.Bold;
                _bedUpgradedSpeed.text = "1 Müşteri / " + MathF.Round(_bedPurchaseSystem.CurrentHealSpeed, 2, MidpointRounding.ToEven).ToString() + " sn";
            }
        }
        private void ShowRegistartionUpgradedPrice() => _registartionUpgradePrice.text = _registartionTableUpgrader.RegistrationUpgradePrice.ToString() + "$";
        private void ShowRegistartionUpgradedSpeed()
        {
            _currentSelectedLocale = LocalizationSettings.SelectedLocale;
            _availableLocales = LocalizationSettings.AvailableLocales;
            if (_currentSelectedLocale == _availableLocales.GetLocale("en"))
            {
                _bedUpgradedSpeed.fontStyle = FontStyles.Normal;
                _registartionUpgradeSpeed.text = "1 Client / " + MathF.Round(_registartionTableUpgrader.CurrentRegistrationSpeed, 2, MidpointRounding.ToEven).ToString() + " sec";
            }
            else if (_currentSelectedLocale == _availableLocales.GetLocale("ru"))
            {
                _bedUpgradedSpeed.fontStyle = FontStyles.Bold;
                _registartionUpgradeSpeed.text = "1 Клиент / " + MathF.Round(_registartionTableUpgrader.CurrentRegistrationSpeed, 2, MidpointRounding.ToEven).ToString() + " сек";
            }
            else if (_currentSelectedLocale == _availableLocales.GetLocale("tr"))
            {
                _bedUpgradedSpeed.fontStyle = FontStyles.Bold;
                _registartionUpgradeSpeed.text = "1 Müşteri / " + MathF.Round(_registartionTableUpgrader.CurrentRegistrationSpeed, 2, MidpointRounding.ToEven).ToString() + " sn";
            }
            
        }
    }
}