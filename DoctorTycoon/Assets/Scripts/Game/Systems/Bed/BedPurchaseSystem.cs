using Player;
using UnityEngine;
using System.Linq;

namespace People
{
    public class BedPurchaseSystem : MonoBehaviour
    {
        [SerializeField] private BedManager _bedManager;
        [SerializeField] private float _multiplier = 2;
        [SerializeField] private int _bedPrice;
        [SerializeField] private int _bedUpgradePrice;
        //?
        public int BedPrice { get { return _bedPrice; }  }
        public void Initialize()
        {
            _bedManager.CurrentPurchasedBedsCount = SaveSystem.BedsData.Count(n => n.Purchased == true);
        }

        public void PurchaseBed()
        {
            for(int i = 0; i < _bedManager.Beds.Count; i++)
            {
                if (!SaveSystem.BedsData[i].Purchased && SaveSystem.PlayerData.Money >= _bedPrice)
                {
                    SaveSystem.BedsData[i].Purchased = true;
                    _bedManager.CurrentPurchasedBedsCount = SaveSystem.BedsData.Count(n => n.Purchased == true);
                    ReduceMoneyCount(_bedPrice);
                    _bedPrice *= Mathf.RoundToInt(_multiplier); 
                    Debug.Log($"Purchased Bed: {i + 1}. Price: {_bedPrice}");
                    EventsManager.Instance.OnBedPurchasedEvent();
                    EventsManager.Instance.OnMoneyValueChangedEvent();
                    return;
                }
                
            }
        }


        private void ReduceMoneyCount(int price) => SaveSystem.PlayerData.Money -= price;
    }
}