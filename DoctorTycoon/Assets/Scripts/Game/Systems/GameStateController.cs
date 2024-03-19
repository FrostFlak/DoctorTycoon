using UnityEngine;

public class GameStateController : MonoBehaviour
{
    [SerializeField] private bool _started;
    [SerializeField] private bool _firstPlaySetting;
    [SerializeField] private bool _tutorial;

    public bool Started { get { return _started; } set { _started = value; } }
    public bool FirstPlaySetting { get { return _firstPlaySetting; } set { _firstPlaySetting = value; } }
    public bool Tutorial { get { return _tutorial; } set { _tutorial = value; } }

    public static GameStateController Instance { get; private set; }

    #region Mono
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

    private void Start()
    {
        EventsManager.Instance.OnGameStarted += StartGame;
        EventsManager.Instance.OnTutorialStarted += StartTutorioal;
    }

    private void OnDisable()
    {
        EventsManager.Instance.OnGameStarted -= StartGame;
        EventsManager.Instance.OnTutorialStarted -= StartTutorioal;
    }
    #endregion

    public void StartGame() => _started = true;
    public void StartTutorioal() => _tutorial = true;
    public void TurnOffFirstPlaySetting() => _firstPlaySetting = false;
    public void TurnOffTutorial() => _tutorial = false;

}
