using UnityEngine;
using Player;
using UI;
using People;

public class Bootstrap : MonoBehaviour
{
    [Header("Systems")]
    [SerializeField] private SaveSystem _saveSystem;
    [SerializeField] private HumansManager _humansManager;
    [SerializeField] private CameraViewChanger _movementType;
    [SerializeField] private RegistrationTable _registrationTable;

    [Header("UI")]
    [SerializeField] private StatsTextShower _statsText;
    [SerializeField] private SensitivityShower _sensitivityShower;

    [Header("ProgressBars")]
    [SerializeField] private LevelProgressBar _levelProgressBar;
    [SerializeField] private RegistrationTableProgressBar _registrationTableProgressBar;

    public static Bootstrap Instance { get; private set; }
    private void Awake()
    {
        InitializeBootstrap();
        InitializeSaveSystem();
        InitializePlayer();
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
        _saveSystem.AssignFilePath();
        _saveSystem.LoadGame();
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

    private void InitializePlayer()
    {
    }

    private void InitializePeoplesSystems()
    {
        _humansManager.Initialize();
    }
}
