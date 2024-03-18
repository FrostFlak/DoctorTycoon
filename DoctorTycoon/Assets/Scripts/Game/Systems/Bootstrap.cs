using UnityEngine;
using Player;
using UI;
using People;

public class Bootstrap : MonoBehaviour
{
    [Header("Systems")]
    [SerializeField] private GameStateController _gameStateController;
    [SerializeField] private SaveSystem _saveSystem;
    [SerializeField] private HumansManager _humansManager;
    [SerializeField] private RegistrationTable _registrationTable;
    [SerializeField] private CharacterJoystickMovement _joystickMovement;

    [Header("UI")]
    [SerializeField] private StatsTextShower _statsText;
    [SerializeField] private SensitivityShower _sensitivityShower;
    [SerializeField] private GameObject _playerCreatePanelBackGround;
    [SerializeField] private GameObject _playerCreatePanel;

    [Header("ProgressBars")]
    [SerializeField] private LevelProgressBar _levelProgressBar;
    [SerializeField] private RegistrationTableProgressBar _registrationTableProgressBar;

    public static Bootstrap Instance { get; private set; }
    private void Awake()
    {
        InitializeBootstrap();
        InitializeSaveSystem();
        InitializeGameStateController();
        InitializeGameSystems();
        InitializeBasicSystems();
        InitializeUI();
        InitializePeoplesSystems();
    }

    private void InitializeBootstrap()
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

    private void InitializeSaveSystem()
    {
        _saveSystem.Initialize();
        _saveSystem.AssignPlayerDataFilePath();
        _saveSystem.AssignLevelDataFilePath();
        _saveSystem.LoadPlayerData();
        _saveSystem.LoadLevelData();
    }

    private void InitializeGameStateController() => _gameStateController.Initialize();
    public void InitializeGameSystems()
    {
        if (SaveSystem.PlayerData.IsFirstPlay)
        {
            InitializeFirstTimePlay();
            _gameStateController.FirstPlaySetting = true;
            _gameStateController.Started = false;
            //lauch tutorial panel
        }
        else
        {
            _gameStateController.Tutorial = false;
            _gameStateController.FirstPlaySetting = false;
            _gameStateController.Started = true;
        }
            
    }
    private void InitializeFirstTimePlay()
    {
        if (SaveSystem.PlayerData.IsFirstPlay)
        {
            Debug.Log("Is Fp");
            _playerCreatePanelBackGround.SetActive(true);
            _playerCreatePanel.SetActive(true);
            SaveSystem.PlayerData.IsFirstPlay = false;
        }
        else
        {
            Debug.Log("!Is Fp");
            _playerCreatePanelBackGround.SetActive(false);
            _playerCreatePanel.SetActive(false);
        }
    }
    private void InitializeBasicSystems()
    {
        _registrationTable.Initialize();
    }

    private void InitializeUI()
    {
        _statsText.Initialize();
        _levelProgressBar.Initialize();
        _registrationTableProgressBar.Initialize();
        _sensitivityShower.Initialize();
    }


    private void InitializePeoplesSystems()
    {
        _humansManager.Initialize();
    }
}
