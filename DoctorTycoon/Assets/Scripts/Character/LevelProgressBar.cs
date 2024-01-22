using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelProgressBar : LevelUpgrader
    {
        [SerializeField] private Image _levelProgressBar;
        [SerializeField] private TMP_Text _oldLevel;
        [SerializeField] private TMP_Text _newLevel;

        private void Update()
        {
            UpdateProgressBar(_playerStats.PlayerData.Expirience);
            UpdateLevelText(_playerStats.PlayerData.CurrentLvl , _playerStats.PlayerData.CurrentLvl + 1);

        }

        private void UpdateProgressBar(int expirienceCount)
        {
            _levelProgressBar.fillAmount = expirienceCount / 100f;
        }

        private void UpdateLevelText(int oldValue , int newValue) 
        {
            _oldLevel.text = oldValue.ToString();
            _newLevel.text = newValue.ToString();
        }
    }
}
    
