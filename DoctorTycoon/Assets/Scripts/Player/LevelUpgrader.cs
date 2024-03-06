using UnityEngine;

namespace Player
{
    public class LevelUpgrader : MonoBehaviour
    {
        private bool TryReachNewLevel()
        {
            if (SaveSystem._playerData.Expirience >= SaveSystem._playerData.MaxExpirience)
            {
                return true;
            }
            else return false;
        }

        public void ReachNewLevel()
        {
            EventsManager.Instance.OnLevelReachedEvent();
            EventsManager.Instance.OnExpirienceChangedEvent();
            if (TryReachNewLevel())
            {
                SaveSystem._playerData.CurrentLvl += 1;
                SaveSystem._playerData.Expirience = 0;
            }
        }
        //test
        public void AddExpirience(int count)
        {
            SaveSystem._playerData.Expirience += count;
            ReachNewLevel();
        }
    }


}
