using Player;
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

        public void Initialize()
        {
            _levelProgressBar.fillAmount = SaveSystem._playerData.Expirience / 100f;
        }
        private void OnEnable()
        {
            OnExpirienceChanged += UpdateProgressBar;
            OnLevelReached += UpdateLevelText;
        }

        private void OnDisable()
        {
            OnExpirienceChanged -= UpdateProgressBar;
            OnLevelReached -= UpdateLevelText;
        }
 
        private void UpdateProgressBar() => _levelProgressBar.fillAmount = SaveSystem._playerData.Expirience / 100f;

        private void UpdateLevelText() 
        {
            _oldLevel.text = SaveSystem._playerData.CurrentLvl.ToString();
            _newLevel.text = (SaveSystem._playerData.CurrentLvl + 1).ToString();
        }
    }
}
    
