using Player;
using System;
using UnityEngine;

namespace People
{
    public class BedPurchaseSystem : MonoBehaviour
    {
        [SerializeField] private BedManager _bedManager;
        private float _multiplier = 1.3f;
        public void PurchaseBed(int price)
        {
            for(int i = 0; i < _bedManager.Beds.Count; i++)
            {
                //price *= Convert.ToInt64(_multiplier); ?
                if (!_bedManager.Beds[i].IsPurchased)
                {
                    _bedManager.Beds[i].IsPurchased = true;
                    ReduceMoneyCount(price);
                    Debug.Log($"Purchased Bed:{i + 1}. Price:{price}");
                    EventsManager.Instance.OnMoneyValueChangedEvent();
                    return;
                }
                
            }
        }

        private void ReduceMoneyCount(int price) => SaveSystem.PlayerData.Money -= price;
    }
}