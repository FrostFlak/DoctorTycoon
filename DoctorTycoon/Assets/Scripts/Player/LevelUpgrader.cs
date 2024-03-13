using UnityEngine;

namespace Player
{
    public class LevelUpgrader : MonoBehaviour
    {
        public void TryAddExperience(int count)
        {
            if(count > 100 || count < 0)
            {
                throw new System.Exception("Cant Add Expirience");  
            }
            else
            {
                SaveSystem.PlayerData.Experience += count;
                TryReachLevel(count);
                EventsManager.Instance.OnExperienceValueChangedEvent();
            }
        }

        private void TryReachLevel(int count)
        {
            if(SaveSystem.PlayerData.Experience >= SaveSystem.PlayerData.MaxExperience)
            {
                SaveSystem.PlayerData.CurrentLvl += 1;
                SaveSystem.PlayerData.Experience -= SaveSystem.PlayerData.MaxExperience;
                EventsManager.Instance.OnLevelReachedEvent();
            }

        }
    }


}
