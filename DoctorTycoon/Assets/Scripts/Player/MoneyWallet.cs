using UnityEngine;

namespace Player
{
    public class MoneyWallet : MonoBehaviour
    {
        private long _maxLongCapacity = long.MaxValue;
        private int _maxIntCapacity = int.MaxValue;
        private int _fivePills = 1000;
        private int _twentyPills = 8300;
        private int _fiftyPills = 12000;
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
            if(count < _maxLongCapacity && count >= 0) return true;
            else return false;
        }
        public void AddMoney(int count)
        {
            if (TryAddMoney(count))
            {
                SaveSystem._playerData.Money += count;
                EventsManager.Instance.OnMoneyAddedEvent();
            }
        }


        public void AddPills(int count)
        {
            if (TryAddPills(count))
            {
                SaveSystem._playerData.Pills += count;
                EventsManager.Instance.OnPillsAddedEvent();
            }
        }
        public void ReducePills(int pillsCount)
        {
            if (TryReducePills(pillsCount))
            {
                if(pillsCount > 1 && pillsCount <= 5)
                {
                    AddMoney(_fivePills);
                    SaveSystem._playerData.Pills -= pillsCount;
                }
                else if(pillsCount > 5 && pillsCount <= 20)
                {
                    AddMoney(_twentyPills);
                    SaveSystem._playerData.Pills -= pillsCount;
                }
                else if (pillsCount > 20 && pillsCount <= 50)
                {
                    AddMoney(_fiftyPills);
                    SaveSystem._playerData.Pills -= pillsCount;
                }
                EventsManager.Instance.OnPillsAddedEvent();
               
            }
        }

        private bool TryReducePills(int count)
        {
            if (count > SaveSystem._playerData.Pills) return false;
            else return true;
        }
        private bool TryAddPills(int count)
        {
            if (count < _maxIntCapacity && count >= 0) return true;
            else return false;
        }

    }

}
