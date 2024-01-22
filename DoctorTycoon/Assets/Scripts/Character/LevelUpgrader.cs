using UnityEngine;
using Player;

namespace UI
{
    public class LevelUpgrader : MonoBehaviour
    {
        [SerializeField] protected PlayerStats _playerStats;
        private void Update()
        {
            ReachNewLevel(_playerStats);
        }
        private void ReachNewLevel(PlayerStats playerStats)
        {
            if(playerStats.PlayerData.Expirience >= 100) 
            {
                playerStats.PlayerData.CurrentLvl += 1;
                playerStats.PlayerData.Expirience = 0;
            }
        }

        public void AddEx()
        {
            _playerStats.PlayerData.Expirience += 10;
        }
    
    
    
    
    
    
    }


}
