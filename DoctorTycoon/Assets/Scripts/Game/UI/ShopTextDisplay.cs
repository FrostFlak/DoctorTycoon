using People;
using TMPro;
using UnityEngine;

namespace UI
{
    public class ShopTextDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _bedPrice;
        [SerializeField] private TMP_Text _bedCount;
        [SerializeField] private TMP_Text _bedUpgradePrice;
        [SerializeField] private TMP_Text _bedUpgradedSpeed;
        [SerializeField] private BedManager _bedManager;
        [SerializeField] private BedPurchaseSystem _bedPurchaseSystem;


        private void Start()
        {
            EventsManager.Instance.OnBedPurchased += ShowBedsCount;
            EventsManager.Instance.OnBedPurchased += ShowBedsPrice;
        }

        private void OnDisable()
        {
            EventsManager.Instance.OnBedPurchased -= ShowBedsCount;
            EventsManager.Instance.OnBedPurchased -= ShowBedsPrice;
        }

        public void Initialize()
        {
            ShowBedsCount();
            ShowBedsPrice();
        }
        private void ShowBedsCount()
        {
            _bedCount.text = _bedManager.CurrentPurchasedBedsCount.ToString();
        }
        private void ShowBedsPrice()
        {
            _bedPrice.text = _bedPurchaseSystem.BedPrice.ToString() + "$";
        }

    }
}