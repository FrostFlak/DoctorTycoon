using Player;
using System;
using UnityEngine;

namespace People
{
    public class BedPurchaseSystem : MonoBehaviour
    {
        [SerializeField] private BedManager _bedManager;
        [SerializeField] private float _multiplier = 1.3f;
        public void PurchaseBed(int price)
        {
            for(int i = 0; i < _bedManager.Beds.Count; i++)
            {
                float roundedPrice = price * _multiplier;
                if (!SaveSystem.BedsData[i].Purchased && SaveSystem.PlayerData.Money >= price)
                {
                    SaveSystem.BedsData[i].Purchased = true;
                    _bedManager.CurrentPurchasedBedsCount++;
                    //?????
                    EventsManager.Instance.OnBedPurchasedEvent();
                    ReduceMoneyCount((int)Math.Round(roundedPrice));
                    Debug.Log($"Purchased Bed: {i + 1}. Price: {roundedPrice}");
                    EventsManager.Instance.OnMoneyValueChangedEvent();
                    return;
                }
                
            }
        }

        private void ReduceMoneyCount(int price) => SaveSystem.PlayerData.Money -= price;
    }
}