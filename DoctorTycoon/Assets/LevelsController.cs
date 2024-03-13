using UI;
using UnityEngine;

namespace Player
{
    public class LevelsController : MonoBehaviour
    {
        [SerializeField] private Level[] _levels;
        [SerializeField] private UILevelController[] _uiControllers;

        private void Start()
        {
            EventsManager.Instance.OnLevelReached += OpenLevels;
            EventsManager.Instance.OnDataReseted += LockAllLevels;
        }
        private void OnDisable()
        {
            EventsManager.Instance.OnLevelReached -= OpenLevels;
            EventsManager.Instance.OnDataReseted -= LockAllLevels;
        }

        private int GetCurrentLevelIndex() {
            return SaveSystem.PlayerData.CurrentLvl - 1;
        }
        private void OpenLevels()
        {
            print("Open Lvl");

            _levels[GetCurrentLevelIndex()].Reached = true;
            _uiControllers[GetCurrentLevelIndex()].OpenLevelUI();


            for (int i = 0; i >= 0 && i <= SaveSystem.PlayerData.CurrentLvl - 1; i++) 
            {
                _levels[i].Reached = true;
                _uiControllers[i].OpenLevelUI();
                SaveSystem.LevelsData[i].Lvl = i + 1;
            }


        }
        public void LockAllLevels()
        {
            for (int i = 0; i < _levels.Length; i++)
            {
                _levels[i].Reached = false;
                _levels[i].RecivedReward = false;
                _uiControllers[i].LockLevelUI();
            }
            _levels[0].Reached = true;
            _levels[0].RecivedReward = false;
            _uiControllers[0].OpenLevelUI();
        }
        public void CheckRecivedRewards()
        {
            for (int i = 0; i >= 0 && i <= SaveSystem.PlayerData.CurrentLvl - 1; i++)
            {
                if (_levels[i].RecivedReward)
                {
                    SaveSystem.LevelsData[i].HasRecivedReward = true;
                    _uiControllers[i].Button.interactable = false;
                }
            }
        }

    }

}
