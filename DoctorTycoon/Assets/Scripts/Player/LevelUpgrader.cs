using UnityEngine;
using Player;
using UnityEngine.Events;

namespace UI
{
    public class LevelUpgrader : MonoBehaviour
    {
        public event UnityAction OnLevelReached;
        public event UnityAction OnExpirienceChanged;


        private void Update()
        {
            ReachNewLevel();
        }
        private bool TryReachNewLevel()
        {
            if (SaveSystem._playerData.Expirience >= SaveSystem._playerData.MaxExpirience)
            {
                return true;
            }
            else return false;
        }

        private void ReachNewLevel()
        {
            OnLevelReached?.Invoke();
            OnExpirienceChanged?.Invoke();
            if (TryReachNewLevel())
            {
                SaveSystem._playerData.CurrentLvl += 1;
                SaveSystem._playerData.Expirience = 0;
            }
        }

        public void AddExpirience(int count) => SaveSystem._playerData.Expirience += count;
    }


}
