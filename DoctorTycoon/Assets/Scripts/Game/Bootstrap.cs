using UnityEngine;
using Player;
using UI;
using People;

public class Bootstrap : MonoBehaviour
{
    [SerializeField] private SaveSystem _saveSystem;
    [SerializeField] private StatsTextShower _statsText;
    [SerializeField] private LevelProgressBar _levelProgressBar;
    [SerializeField] private CharacterMovmentThirdPersonView _characterMovmentThirdPersonView;
    [SerializeField] private MovementType _movementType;
    [SerializeField] private HumansManager _humansManager;
    public static Bootstrap Instance { get; private set; }
    private void Awake()
    {
        Initialize();
        InitializeSaveSystem();
        InitializeUI();
        InitializePlayer();
        //InitializePeoplesSystems();
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
    }

    private void InitializePlayer()
    {
        _characterMovmentThirdPersonView.Initialize();
    }

    private void InitializePeoplesSystems()
    {
    }
}
