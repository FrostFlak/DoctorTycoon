using Player;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCreatorUI : MonoBehaviour
{
    [SerializeField] private Toggle _manToggle;
    [SerializeField] private Toggle _womanToggle;
    [SerializeField] private Toggle _tutorialToggle;
    [SerializeField] private GameObject _tutorialPanel;
    private string _name;
    private bool _gender;
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
            _tutorialPanel.SetActive(true);
            EventsManager.Instance.OnTutorialStartedEvent();
        }
        else
        {
            EventsManager.Instance.OnGameStartedEvent();
        }
        
    }
}
