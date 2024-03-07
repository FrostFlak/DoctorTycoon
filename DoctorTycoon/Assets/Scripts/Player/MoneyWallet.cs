using UnityEngine;

namespace Player
{
    public class MoneyWallet : MonoBehaviour
    {
        private long _maxCapacity = long.MaxValue;
        public void AddMoney(long count)
        {
            if (TryAddMoney(count))
            {
                SaveSystem._playerData.Money += count;
                EventsManager.Instance.OnMoneyAddedEvent();
            }
        }
        private bool TryAddMoney(long count)
        {
            if(count < _maxCapacity && count >= 0) return true;
            else return false;
        }
    }

}
