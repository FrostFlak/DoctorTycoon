using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EventsManager : MonoBehaviour
{
    public event UnityAction OnPeopleSpawned;
    public event UnityAction OnMoneyAdded;
    public event UnityAction OnStayInTriggerZone;
    public event UnityAction OnTimerToAcceptPeopleEnd;

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

    public void OnMoneyAddedEvent()
    {
        OnMoneyAdded?.Invoke();
    }

    public void OnStayInTriggerZoneEvent()
    {
        OnStayInTriggerZone?.Invoke();
    }

    public void OnTimerToAcceptPeopleEndEvent()
    {
        OnTimerToAcceptPeopleEnd?.Invoke();
    }
}
