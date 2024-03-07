using Player;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelProgressBar : LevelUpgrader
    {
        [SerializeField] private Image _levelProgressBar;
        [SerializeField] private TMP_Text _currentLevel;

        public void Initialize()
        {
            _levelProgressBar.fillAmount = SaveSystem._playerData.Expirience / 100f;
            _currentLevel.text = SaveSystem._playerData.CurrentLvl.ToString();

        }
        private void Start()
        {
            EventsManager.Instance.OnExpirienceAdded += UpdateProgressBar;
            EventsManager.Instance.OnLevelReached += UpdateLevelText;
            EventsManager.Instance.OnDataReseted += UpdateLevelText;
            EventsManager.Instance.OnDataReseted += UpdateProgressBar;
        }

        private void OnDisable()
        {
            EventsManager.Instance.OnExpirienceAdded -= UpdateProgressBar;
            EventsManager.Instance.OnLevelReached -= UpdateLevelText;
            EventsManager.Instance.OnDataReseted -= UpdateLevelText;
            EventsManager.Instance.OnDataReseted -= UpdateProgressBar;

        }

        private void UpdateProgressBar() => _levelProgressBar.fillAmount = SaveSystem._playerData.Expirience / 100f;
        private void UpdateLevelText() => _currentLevel.text = SaveSystem._playerData.CurrentLvl.ToString();
    }
}
    
