using UnityEngine;
using UnityEngine.Events;



public class EventsManager : MonoBehaviour
{
    public event UnityAction OnPeopleSpawned;
    public static EventsManager Instance { get; private set; }

    private void Awake()
    {
        Initialize();
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
    public void OnPeopleSpawnedEvent()
    {
        OnPeopleSpawned?.Invoke();
    }
}
