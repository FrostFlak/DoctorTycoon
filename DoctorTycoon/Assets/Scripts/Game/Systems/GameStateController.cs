using UnityEngine;

public class GameStateController : MonoBehaviour
{
    [SerializeField] private bool _started;
    [SerializeField] private bool _paused;
    [SerializeField] private bool _tutorial;

    public bool Started { get { return _started; } set { _started = value; } }
    public bool Paused { get { return _paused; } set { _paused = value; } }
    public bool Tutorial { get { return _tutorial; } set { _tutorial = value; } }

    public static GameStateController Instance { get; private set; }

    #region 
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

        _tutorial = true;
    }
    #endregion
}
