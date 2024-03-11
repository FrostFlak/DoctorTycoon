using People;
using UnityEngine;
using UnityEngine.Events;

public class EventsManager : MonoBehaviour
{
    #region PeopleEvents
    public event UnityAction<Human> OnPatientLeaveBed;
    public event UnityAction<int> OnPatientSpawned;
    public event UnityAction<int> OnPatientLeaveHospital;
    #endregion

    #region TimersEvents
    public event UnityAction OnTimerToAcceptPeopleEnd;
    public event UnityAction OnTimerToHealPatinetEnd;
    #endregion

    #region PlayerEvents
    public event UnityAction OnStayInRegistrationTriggerZone;
    public event UnityAction OnStayInBedTriggerZone;
    public event UnityAction OnExitRegistartionTriggerZone;
    public event UnityAction OnExitBedTriggerZone;
    #endregion

    #region PlayerDataEvents
    public event UnityAction OnLevelReached;
    public event UnityAction OnExpirienceAdded;
    public event UnityAction OnMoneyAdded;
    public event UnityAction OnPillsAdded;
    public event UnityAction OnDataReseted;
    #endregion


    public static EventsManager Instance { get; private set; }

    #region Mono
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

    #endregion

    #region People
    public void OnPatientSpawnedEvent(int count) => OnPatientSpawned?.Invoke(count);
    public void OnPatientLeaveHospitalEvent(int count) => OnPatientLeaveHospital?.Invoke(count);
    public void OnPatientLeaveBedEvent(Human human) => OnPatientLeaveBed?.Invoke(human);
    #endregion

    #region Timers
    public void OnTimerToAcceptPeopleEndEvent() => OnTimerToAcceptPeopleEnd?.Invoke();
    public void OnTimerToHealPatinetEndEvent() => OnTimerToHealPatinetEnd?.Invoke();
    #endregion

    #region Player
    public void OnStayInRegistrationTriggerZoneEvent() => OnStayInRegistrationTriggerZone?.Invoke();
    public void OnStayInBedTriggerZoneEvent() => OnStayInBedTriggerZone?.Invoke();
    public void OnExitRegistartionTriggerZoneEvent() => OnExitRegistartionTriggerZone?.Invoke();
    public void OnExitBedTriggerZoneEvent() => OnExitBedTriggerZone?.Invoke();
    public void OnLevelReachedEvent() => OnLevelReached?.Invoke();
    public void OnExpirienceAddedEvent() => OnExpirienceAdded?.Invoke();
    public void OnMoneyAddedEvent() => OnMoneyAdded?.Invoke();
    public void OnPillsAddedEvent() => OnPillsAdded?.Invoke();
    public void OnDataResetedEvent() => OnDataReseted?.Invoke();

    #endregion


    


}
