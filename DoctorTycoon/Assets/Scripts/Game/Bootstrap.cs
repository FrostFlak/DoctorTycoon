using UnityEngine;
using Player;
using UI;
using People;

public class Bootstrap : MonoBehaviour
{
    [Header("Systems")]
    [SerializeField] private SaveSystem _saveSystem;
    [SerializeField] private HumansManager _humansManager;
    [SerializeField] private CharacterClickMovmentThirdPersonView _characterClickMovmentThirdPersonView;
    [SerializeField] private CameraViewChanger _movementType;
    [SerializeField] private RegistrationTable _registrationTable;

    [Header("Stats")]
    [SerializeField] private StatsTextShower _statsText;

    [Header("ProgressBars")]
    [SerializeField] private LevelProgressBar _levelProgressBar;
    [SerializeField] private RegistrationTableProgressBar _registrationTableProgressBar;
    public static Bootstrap Instance { get; private set; }
    private void Awake()
    {
        Initialize();
        InitializeSaveSystem();
        InitializePlayer();
        InitializeUI();
        InitializePeoplesSystems();
    }

    private void Initialize()
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

    private void InitializeUI()
    {
        _statsText.Initialize();
        _levelProgressBar.Initialize();
        _registrationTableProgressBar.Initialize();
    }

    private void InitializePlayer()
    {
        _characterClickMovmentThirdPersonView.Initialize();
    }

    private void InitializePeoplesSystems()
    {
        _humansManager.Initialize();
    }
}
