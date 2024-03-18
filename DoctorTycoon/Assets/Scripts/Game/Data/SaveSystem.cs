using UnityEngine;
using System.IO;
using System;
using UnityEngine.SceneManagement;


namespace Player
{
    public class SaveSystem : MonoBehaviour
    {
        public static SaveSystem Instance { get; private set; }
        public static PlayerData PlayerData = new PlayerData();
        public static LevelData[] LevelsData;
        private string _playerSaveFilePath;
        private string _levelSaveFilePath;
        [SerializeField] private string _name;
        [SerializeField] private bool _gender;
        [SerializeField] private bool _firstPlay;
        [SerializeField] private long _money;
        [SerializeField] private int _pills;
        [SerializeField] private int _experience;
        [SerializeField] private int _currentLevel;
        //private string saveLevelData;

        private void Start()
        {
            SavePlayerData();
            SaveLevelData();
            EventsManager.Instance.OnMoneyValueChanged += SavePlayerData;
            EventsManager.Instance.OnPillsValueChanged += SavePlayerData;

            EventsManager.Instance.OnExperienceValueChanged += SavePlayerData;
            EventsManager.Instance.OnExperienceValueChanged += SaveLevelData;

            EventsManager.Instance.OnLevelReached += SavePlayerData;
            EventsManager.Instance.OnLevelReached += SaveLevelData;
        }

        private void OnDisable()
        {
            EventsManager.Instance.OnMoneyValueChanged -= SavePlayerData;
            EventsManager.Instance.OnPillsValueChanged -= SavePlayerData;

            EventsManager.Instance.OnExperienceValueChanged -= SavePlayerData;
            EventsManager.Instance.OnExperienceValueChanged -= SaveLevelData;
            
            EventsManager.Instance.OnLevelReached -= SavePlayerData;
            EventsManager.Instance.OnLevelReached -= SaveLevelData;

        }

        private void Update()
        {
            _name = PlayerData.Name;
            _gender = PlayerData.Gender;
            _money = PlayerData.Money;
            _experience = PlayerData.Experience;
            _pills = PlayerData.Pills;
            _currentLevel = PlayerData.CurrentLvl;
            _firstPlay = PlayerData.IsFirstPlay;
        }
        public void Initialize()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            PlayerData.IsFirstPlay = false;
        }

        #region PlayerData
        public void AssignPlayerDataFilePath()
        {
            _playerSaveFilePath = Application.persistentDataPath + "/PlayerData.json";
        }

        public void SavePlayerData()
        {
            string savePlayerData = JsonUtility.ToJson(PlayerData);
            File.WriteAllText(_playerSaveFilePath, savePlayerData);
            if (File.Exists(_playerSaveFilePath))
                Debug.Log("Player Data Saved");
            else
                Debug.Log("Save file created at: " + _playerSaveFilePath);
        }

        public void LoadPlayerData()
        {
            if (File.Exists(_playerSaveFilePath))
            {
                string loadPlayerData = File.ReadAllText(_playerSaveFilePath);
                PlayerData = JsonUtility.FromJson<PlayerData>(loadPlayerData);

                Debug.Log("Load PlayerData Completed! \n" +
                    "Player Experience: " + PlayerData.Experience +
                    " Player Current Lvl: " + PlayerData.CurrentLvl +
                    " Player Current Money: " + PlayerData.Money + 
                    " Player Current Pills: " + PlayerData.Pills);
                EventsManager.Instance.OnMoneyValueChangedEvent();
            }
            else
                Debug.Log("There is no save files to load!");

        }

        public void ResetPlayerData()
        {
            PlayerData.Name = "";
            PlayerData.Gender = false;
            PlayerData.Experience = 0;
            PlayerData.CurrentLvl = 1;
            PlayerData.Money = 0;
            PlayerData.Pills = 0;
            PlayerData.IsFirstPlay = true;
            EventsManager.Instance.OnDataResetedEvent();
            SavePlayerData();
            //for test 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void DeletePlayerDataSaveFiles()
        {
            if (File.Exists(_playerSaveFilePath))
            {
                ResetPlayerData();
                File.Delete(_playerSaveFilePath);
                Debug.Log("Player Save file deleted!");
            }
            else
                Debug.Log("There is nothing to delete!");
        }

        #endregion

        #region LevelData
        public void AssignLevelDataFilePath()
        {
            _levelSaveFilePath = Application.persistentDataPath + "/LevelData.json";
        }
        public void SaveLevelData()
        {
            string saveLevelData = JsonHelper.ToJson(LevelsData, true);
            File.WriteAllText(_levelSaveFilePath, saveLevelData); 
            if (File.Exists(_levelSaveFilePath))
                Debug.Log("Level Data Saved");
            else
                Debug.Log("Save file created at: " + _levelSaveFilePath);

        }
        public void LoadLevelData()
        {
            if (File.Exists(_levelSaveFilePath))
            {
                string loadLevelData = File.ReadAllText(_levelSaveFilePath);
                LevelsData = JsonHelper.FromJson<LevelData>(loadLevelData);
                Debug.Log("Load Levels Data Completed");
            }
            else
                Debug.Log("There is no save files to load!");

        }
        public void ResetLevelData()
        {
            foreach(var level in LevelsData)
            {
                level.Lvl = 1;
                level.HasRecivedReward = false;
            }
            SaveLevelData();
        }
        public void DeleteLevelDataSaveFiles()
        {
            if (File.Exists(_levelSaveFilePath))
            {
                ResetLevelData();
                File.Delete(_levelSaveFilePath);
                Debug.Log("Level Save file deleted!");
            }
            else
                Debug.Log("There is nothing to delete!");
        }
        #endregion

    }

    [Serializable]
    public class PlayerData
    {
        public string _name = "Anonymous";
        public bool _gender = false;
        public int _experience = 0;
        public const int _maxExperience = 100;
        public int _currentLvl = 1;
        public long _money = 0;
        public int _pills = 0;
        public bool _isFirstPlay = true;

        #region Properties
        public string Name { get { return _name; } set { _name = value; } }
        public int Experience
        {
            get { return _experience; }
            set { _experience = value;}
        }

        public int MaxExperience { get { return _maxExperience; } private set { } }
        public int CurrentLvl
        {
            get { return _currentLvl; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(_currentLvl));
                else _currentLvl = value;
            }
        }
        public long Money { get { return _money; } 
            set { 
                if(value < 0)
                    throw new ArgumentOutOfRangeException(nameof(_money));
                else _money = value; 
            } }
        public int Pills
        {
            get { return _pills; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(_pills));
                else _pills = value;
            }
        }
        public bool Gender { get { return _gender; } set { _gender = value; } }
        public bool IsFirstPlay { get { return _isFirstPlay; } set { _isFirstPlay = value; } }


        #endregion
    }

    [Serializable]
    public class LevelData
    {
        public int _lvl = 1;
        public bool _hasRecivedReward = false;

        #region Properties
        public int Lvl
        {
            get { return _lvl; }
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(_lvl));
                else _lvl = value;
            }
        }

        public bool HasRecivedReward
        {
            get { return _hasRecivedReward; }
            set { _hasRecivedReward = value; }
        }
        #endregion
    }
}
