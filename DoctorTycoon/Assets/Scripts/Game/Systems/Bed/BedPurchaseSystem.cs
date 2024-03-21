using Player;
using UnityEngine;

namespace People
{
    public class BedPurchaseSystem : MonoBehaviour
    {
        [SerializeField] private BedManager _bedManager;


        public void PurchaseBed(int price)
        {
            for(int i = 0; i < _bedManager.Beds.Count; i++)
            {
                if (!_bedManager.Beds[i].IsPurchased)
                {
                    _bedManager.Beds[i].IsPurchased = true;
                    ReduceMoneyCount(price);
                    return;
                }
            }
        }

        private void ReduceMoneyCount(int count) => SaveSystem.PlayerData.Money -= count;
    }
}