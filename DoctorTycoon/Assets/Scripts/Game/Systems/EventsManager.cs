using People;
using UnityEngine;
using UnityEngine.Events;

public class EventsManager : MonoBehaviour
{
    #region PeopleEvents
    public event UnityAction<Human> OnPatientLeaveBed;
    public event UnityAction<int> OnPatientEnterHospital;
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
    public event UnityAction OnExperienceValueChanged;
    public event UnityAction OnMoneyValueChanged;
    public event UnityAction OnPillsValueChanged;
    public event UnityAction OnDataReseted;
    #endregion

    #region UIEvents
    public event UnityAction OnOpenLevelsUI;

    #endregion

    #region GameStateEvents
    public event UnityAction OnGameStarted;
    public event UnityAction OnTutorialStarted;
    public event UnityAction OnTutorialEnd;
    #endregion

    #region ShopEvents
    public event UnityAction OnBedPurchased;
    public event UnityAction OnBedUpgraded;
    public event UnityAction OnRegistrationUpgraded;
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
    public void OnPatientEnterHospitalEvent(int count) => OnPatientEnterHospital?.Invoke(count);
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
    public void OnExperienceValueChangedEvent() => OnExperienceValueChanged?.Invoke();
    public void OnMoneyValueChangedEvent() => OnMoneyValueChanged?.Invoke();
    public void OnPillsValueChangedEvent() => OnPillsValueChanged?.Invoke();
    public void OnDataResetedEvent() => OnDataReseted?.Invoke();

    #endregion

    #region UI
    public void OnOpenLevelsUIEvent() => OnOpenLevelsUI?.Invoke();
    #endregion

    #region GameState
    public void OnGameStartedEvent() => OnGameStarted?.Invoke();
    public void OnTutorialStartedEvent() => OnTutorialStarted?.Invoke();
    public void OnTutorialEndEvent() => OnTutorialEnd?.Invoke();
    #endregion

    #region Shop
    public void OnBedPurchasedEvent() => OnBedPurchased?.Invoke();
    public void OnBedUpgradedEvent() => OnBedUpgraded?.Invoke();
    public void OnRegistartionUpgradedEvent() => OnRegistrationUpgraded?.Invoke();

    #endregion



}
