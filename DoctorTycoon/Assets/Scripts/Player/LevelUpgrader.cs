using UnityEngine;

namespace Player
{
    public class LevelUpgrader : MonoBehaviour
    {
        public void TryAddExpirience(int count)
        {
            if(count > 100 || count < 0)
            {
                throw new System.Exception("Cant Add Expirience");  
            }
            else
            {
                SaveSystem._playerData.Expirience += count;
                TryReachLevel(count);
                EventsManager.Instance.OnExpirienceAddedEvent();
            }
        }

        private void TryReachLevel(int count)
        {
            if(SaveSystem._playerData.Expirience >= SaveSystem._playerData.MaxExpirience)
            {
                SaveSystem._playerData.CurrentLvl += 1;
                SaveSystem._playerData.Expirience -= SaveSystem._playerData.MaxExpirience;
                EventsManager.Instance.OnLevelReachedEvent();
            }

        }
    }


}
