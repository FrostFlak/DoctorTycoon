using Player;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCreatorUI : MonoBehaviour
{
    [SerializeField] private string _name;
    [SerializeField] private bool _gender;
    [SerializeField] private Toggle _manToggle;
    [SerializeField] private Toggle _womanToggle;
    [SerializeField] private Toggle _tutorialToggle;
    public void ReadPlayerNameInput(string name) => _name = name;  

    public void ReadPlayerGender()
    {
        if (_manToggle.isOn)
            _gender = true;
        else if (_womanToggle.isOn)
            _gender = false;
    }

    private void SavePlayerInputs()
    {
        SaveSystem.PlayerData.Name = _name;
        SaveSystem.PlayerData.Gender = _gender;
    }
    public void StartGame()
    {
        Bootstrap.Instance.InitializeGameSystems();
        SavePlayerInputs();
        SaveSystem.Instance.SavePlayerData();
        if (_tutorialToggle.isOn)
        {
            print("Tutorial:");
            //ui
            EventsManager.Instance.OnGameStartedEvent();
        }
        else
        {
            EventsManager.Instance.OnGameStartedEvent();
        }
        
    }
}
