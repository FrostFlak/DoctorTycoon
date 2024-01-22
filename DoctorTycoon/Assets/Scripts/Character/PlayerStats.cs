using System;
using UnityEngine;

namespace Player
{
    public class PlayerStats : MonoBehaviour
    {
        #region Fields
        [SerializeField] private string _name;
        [SerializeField , Range(0 ,100)] private int _expirience;
        [SerializeField] private int _currentLvl;
        private PlayerData _playerData = new PlayerData();


        #endregion

        #region Properties
        public PlayerData PlayerData {  get { return _playerData; } }
        #endregion

        #region MonoBehaviour

        private void Update()
        {
            _playerData.Name = _name;
            _playerData.Expirience = _expirience;
            _playerData.CurrentLvl = _currentLvl;
        
        }

        #endregion
    }

[Serializable]
    public class PlayerData
    {
        private string _name;
        private int _expirience = 0;
        private int _currentLvl = 0;

        #region Properties
        public string Name { get { return _name; } set { _name = value; } }
        public int Expirience { get { return _expirience; }
            set {
                if (value > 100 || value < 0)
                {
                    throw new ArgumentOutOfRangeException("Incorrect Expirience Value");
                }
                else 
                    _expirience = value;
                } 
        }
        public int CurrentLvl { get { return _currentLvl; } set { _currentLvl = value; } }

        #endregion
    }
}


