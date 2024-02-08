using Player;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private CharacterMovmentFirstPersonView _firstPersonController;
    private const string IDLE = "Idle";
    private const string WALK = "Walk";
    private const string SEARCH = "SearchingFiles";
    private const string SEARCHSPEED = "SearchSpeed";
    private const string HEAL = "Shake";
    private const string HEALSPEED = "HealSpeed";
    private int _searchSpeed = 1;
    private int _healSpeed = 1;
    private bool _isPlayingRegistrationAnimation;
    private bool _isPlayingHealingAnimation;

    private void Start()
    {
        EventsManager.Instance.OnStayInRegistrationTriggerZone += StartRegistrationAnimation;
        EventsManager.Instance.OnStayInBedTriggerZone += StartHealAnimation;
        EventsManager.Instance.OnExitRegistartionTriggerZone += SwitchRegistrationAnimationState;
        EventsManager.Instance.OnExitBedTriggerZone += SwitchHealingAnimationState;
        EventsManager.Instance.OnTimerToAcceptPeopleEnd += SwitchRegistrationAnimationState;

    }

    private void OnDisable()
    {
        EventsManager.Instance.OnStayInRegistrationTriggerZone -= StartRegistrationAnimation;
        EventsManager.Instance.OnStayInBedTriggerZone -= StartHealAnimation;
        EventsManager.Instance.OnExitRegistartionTriggerZone -= SwitchRegistrationAnimationState;
        EventsManager.Instance.OnExitBedTriggerZone -= SwitchHealingAnimationState;
        EventsManager.Instance.OnTimerToAcceptPeopleEnd -= SwitchRegistrationAnimationState;
    }
    void Update()
    {
        if (!_isPlayingRegistrationAnimation && !_isPlayingHealingAnimation)
            StartAnimation();
    }
    private void StartAnimation()
    {
        if (_agent.velocity != Vector3.zero || _firstPersonController.IsWalking)
        {
            _animator.Play(WALK);
        }
        else
        {
            _animator.Play(IDLE);
        }
    }

    private void StartRegistrationAnimation()
    {
        _isPlayingRegistrationAnimation = true;
        _animator.SetFloat(SEARCHSPEED, _searchSpeed);
        _animator.Play(SEARCH);
    }

    private void SwitchRegistrationAnimationState() => _isPlayingRegistrationAnimation = false;

    private void StartHealAnimation()
    {
        _isPlayingHealingAnimation = true;
        _animator.SetFloat(HEALSPEED, _healSpeed);
        _animator.Play(HEAL);
    }
    private void SwitchHealingAnimationState() => _isPlayingHealingAnimation = false;
}
