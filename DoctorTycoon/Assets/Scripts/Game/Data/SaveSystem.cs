using UnityEngine;
using System.IO;
using System;


namespace Player
{
    public class SaveSystem : MonoBehaviour
    {
        public static SaveSystem Instance { get; private set; }
        public static PlayerData _playerData = new PlayerData() ;
        private string saveFilePath;
        [SerializeField] private long _money;
        [SerializeField] private int _pills;
        [SerializeField] private int _expirience;

        private void Start()
        {
            EventsManager.Instance.OnMoneyAdded += SaveGame;
            EventsManager.Instance.OnPillsAdded += SaveGame;
            EventsManager.Instance.OnExpirienceAdded += SaveGame;
            EventsManager.Instance.OnLevelReached += SaveGame;
        }

        private void OnDisable()
        {
            EventsManager.Instance.OnMoneyAdded -= SaveGame;
            EventsManager.Instance.OnPillsAdded -= SaveGame;
            EventsManager.Instance.OnExpirienceAdded -= SaveGame;
            EventsManager.Instance.OnLevelReached -= SaveGame;
        }

        private void Update()
        {
            _money = _playerData.Money;
            _expirience = _playerData.Expirience;
            _pills = _playerData.Pills;
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
        }
        public void AssignFilePath()
        {
            saveFilePath = Application.persistentDataPath + "/PlayerData.json";
        }

        public void SaveGame()
        {
            string savePlayerData = JsonUtility.ToJson(_playerData);
            File.WriteAllText(saveFilePath, savePlayerData);
            if (File.Exists(saveFilePath))
                Debug.Log("Data Saved");
            else
                Debug.Log("Save file created at: " + saveFilePath);
        }

        public void LoadGame()
        {
            if (File.Exists(saveFilePath))
            {
                string loadPlayerData = File.ReadAllText(saveFilePath);
                _playerData = JsonUtility.FromJson<PlayerData>(loadPlayerData);

                Debug.Log("Load game complete! \n" +
                    "Player Expirience: " + _playerData.Expirience +
                    " Player Current Lvl: " + _playerData.CurrentLvl +
                    " Player Current Money: " + _playerData.Money + 
                    " Player Current Pills: " + _playerData.Pills);
                EventsManager.Instance.OnMoneyAddedEvent();
            }
            else
                Debug.Log("There is no save files to load!");

        }

        public void ResetData()
        {
            _playerData.Name = "";
            _playerData.Expirience = 0;
            _playerData.CurrentLvl = 1;
            _playerData.Money = 0;
            _playerData.Pills = 0;
            EventsManager.Instance.OnDataResetedEvent();
            SaveGame();
        }
        public void DeleteSaveFile()
        {
            if (File.Exists(saveFilePath))
            {
                File.Delete(saveFilePath);

                Debug.Log("Save file deleted!");
            }
            else
                Debug.Log("There is nothing to delete!");
        }
    }

    [Serializable]
    public class PlayerData
    {
        public string _name = "Anonymous";
        public int _expirience = 0;
        public const int _maxExpirience = 100;
        public int _currentLvl = 1;
        public long _money = 0;
        public int _pills = 0;

        #region Properties
        public string Name { get { return _name; } set { _name = value; } }
        public int Expirience
        {
            get { return _expirience; }
            set { _expirience = value;}
        }

        public int MaxExpirience { get { return _maxExpirience; } private set { } }
        public int CurrentLvl { get { return _currentLvl; } 
            set { 
                if(value < 0)
                    throw new ArgumentOutOfRangeException(nameof(_currentLvl));
                else _currentLvl = value;
            } }
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

        #endregion
    }
}
