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
                SaveSystem.PlayerData.Money += count;
                EventsManager.Instance.OnMoneyValueChangedEvent();
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
                SaveSystem.PlayerData.Money += count;
                EventsManager.Instance.OnMoneyValueChangedEvent();
            }
        }


        public void AddPills(int count)
        {
            if (TryAddPills(count))
            {
                SaveSystem.PlayerData.Pills += count;
                EventsManager.Instance.OnPillsValueChangedEvent();
            }
        }
        public void ReducePills(int pillsCount)
        {
            if (TryReducePills(pillsCount))
            {
                if(pillsCount > 1 && pillsCount <= 5)
                {
                    AddMoney(_fivePills);
                    SaveSystem.PlayerData.Pills -= pillsCount;
                }
                else if(pillsCount > 5 && pillsCount <= 20)
                {
                    AddMoney(_twentyPills);
                    SaveSystem.PlayerData.Pills -= pillsCount;
                }
                else if (pillsCount > 20 && pillsCount <= 50)
                {
                    AddMoney(_fiftyPills);
                    SaveSystem.PlayerData.Pills -= pillsCount;
                }
                EventsManager.Instance.OnPillsValueChangedEvent();
               
            }
        }

        private bool TryReducePills(int count)
        {
            if (count > SaveSystem.PlayerData.Pills) return false;
            else return true;
        }
        private bool TryAddPills(int count)
        {
            if (count < _maxIntCapacity && count >= 0) return true;
            else return false;
        }

    }

}
