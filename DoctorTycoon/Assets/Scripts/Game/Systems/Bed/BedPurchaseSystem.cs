using Player;
using UnityEngine;
using System.Linq;

namespace People
{
    public class BedPurchaseSystem : MonoBehaviour
    {
        [SerializeField] private BedManager _bedManager;
        [SerializeField] private GameObject _notEnoughMoneyPanel;
        [SerializeField] private float _priceMultiplier;
        [SerializeField] private int _bedPrice;
        [SerializeField] private int _bedUpgradePrice;
        [SerializeField] private float _bedUpgradeSpeed;
        private float _currentHealSpeed;
        public int BedPrice { get { return _bedPrice; }  }
        public int BedUpgradePrice { get { return _bedUpgradePrice; }  }
        public float CurrentHealSpeed { get { return _currentHealSpeed; }  }
        public void Initialize()
        {
            _bedManager.CurrentPurchasedBedsCount = SaveSystem.BedsData.Count(n => n.Purchased == true);
            _currentHealSpeed = SaveSystem.BedsData[0].MaxTimeToHeal;
            _bedPrice = SaveSystem.ShopData.CurrentBedPrice;
            _bedUpgradePrice = SaveSystem.ShopData.CurrentUpgradeBedPrice;
        }

        public void PurchaseBed()
        {
            for(int i = 0; i < _bedManager.Beds.Count; i++)
            {
                if (!SaveSystem.BedsData[i].Purchased && SaveSystem.PlayerData.Money >= _bedPrice)
                {
                    SaveSystem.BedsData[i].Purchased = true;
                    SaveSystem.ShopData.CurrentBedPrice = _bedPrice;
                    _bedManager.CurrentPurchasedBedsCount = SaveSystem.BedsData.Count(n => n.Purchased == true);
                    ReduceMoneyCount(_bedPrice);
                    Debug.Log($"Purchased Bed: {i + 1}. Price: {_bedPrice}");
                    _bedPrice = MultiplyPrice(_bedPrice, _priceMultiplier);
                    SaveSystem.ShopData.CurrentBedPrice = _bedPrice;
                    EventsManager.Instance.OnBedPurchasedEvent();
                    EventsManager.Instance.OnMoneyValueChangedEvent();
                    return;
                }
                else if(!SaveSystem.BedsData[i].Purchased && !(SaveSystem.PlayerData.Money >= _bedPrice))
                {
                    Vector2 currentMousePosition = Input.mousePosition;
                    Vector2 appearPosition = new Vector2(currentMousePosition.x , currentMousePosition.y + 125f);
                    _notEnoughMoneyPanel.transform.position = appearPosition;
                    _notEnoughMoneyPanel.SetActive(true);
                    return;
                }
            }
        }

        public void UpgradeBed()
        {
            if (SaveSystem.BedsData[0].Purchased && SaveSystem.PlayerData.Money >= _bedUpgradePrice)
            {
                for (int i = 0; i < _bedManager.Beds.Count; i++)
                {
                    SaveSystem.BedsData[i].MaxTimeToHeal -= _bedUpgradeSpeed;
                    SaveSystem.ShopData.CurrentUpgradeBedPrice = _bedUpgradePrice;
                    _currentHealSpeed = SaveSystem.BedsData[i].MaxTimeToHeal;
                }
                ReduceMoneyCount(_bedUpgradePrice);
                _bedUpgradePrice = MultiplyPrice(_bedUpgradePrice + _bedUpgradePrice, _priceMultiplier);
                SaveSystem.ShopData.CurrentUpgradeBedPrice = _bedUpgradePrice;
                EventsManager.Instance.OnBedUpgradedEvent();
                EventsManager.Instance.OnMoneyValueChangedEvent();
            }
            else if (SaveSystem.BedsData[0].Purchased && !(SaveSystem.PlayerData.Money >= _bedPrice))
            {
                Vector2 currentMousePosition = Input.mousePosition;
                Vector2 appearPosition = new Vector2(currentMousePosition.x, currentMousePosition.y + 125f);
                _notEnoughMoneyPanel.transform.position = appearPosition;
                _notEnoughMoneyPanel.SetActive(true);
                return;
            }
        }

        private int MultiplyPrice(int price ,float multiplier)
        {
            return Mathf.RoundToInt(price * multiplier);
        }
        private void ReduceMoneyCount(int price) => SaveSystem.PlayerData.Money -= price;
    }
}