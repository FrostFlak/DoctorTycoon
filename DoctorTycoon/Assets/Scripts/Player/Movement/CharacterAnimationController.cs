using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public class CharacterAnimationController : MonoBehaviour
    {
        #region SerializedFields
        [SerializeField] private Animator _animator;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private CharacterMovmentFirstPersonView _firstPersonController;
        [SerializeField] private CharacterJoystickMovement _characterJoystickMovement;
        #endregion

        #region PrivateFields
        private int _searchSpeed = 1;
        private int _healSpeed = 1;
        private bool _isPlayingRegistrationAnimation;
        private bool _isPlayingHealingAnimation;
        #endregion

        #region Constants
        private const string IDLE = "Idle";
        private const string WALK = "Walk";
        private const string SEARCH = "SearchingFiles";
        private const string SEARCHSPEED = "SearchSpeed";
        private const string HEAL = "Shake";
        private const string HEALSPEED = "HealSpeed";

        #endregion
        private void Start()
        {

            EventsManager.Instance.OnStayInRegistrationTriggerZone += StartRegistrationAnimation;
            EventsManager.Instance.OnExitRegistartionTriggerZone += OffRegistrationAnimation;
            EventsManager.Instance.OnTimerToAcceptPeopleEnd += OffRegistrationAnimation;
            
            EventsManager.Instance.OnStayInBedTriggerZone += StartHealAnimation;
            EventsManager.Instance.OnTimerToHealPatinetEnd += OffHealingAnimation;
            EventsManager.Instance.OnExitBedTriggerZone += OffHealingAnimation;
        }

        private void OnDisable()
        {
            EventsManager.Instance.OnStayInRegistrationTriggerZone -= StartRegistrationAnimation;
            EventsManager.Instance.OnExitRegistartionTriggerZone -= OffRegistrationAnimation;
            EventsManager.Instance.OnTimerToAcceptPeopleEnd -= OffRegistrationAnimation;
            
            EventsManager.Instance.OnStayInBedTriggerZone -= StartHealAnimation;
            EventsManager.Instance.OnTimerToHealPatinetEnd -= OffHealingAnimation;
            EventsManager.Instance.OnExitBedTriggerZone -= OffHealingAnimation;
        }
        private void Update()
        {
            if (!_isPlayingRegistrationAnimation && !_isPlayingHealingAnimation)
                StartWalkAnimation();
        }

        private void StartWalkAnimation()
        {
            if (_firstPersonController.IsWalking || _characterJoystickMovement.IsWalking)
                _animator.Play(WALK);
            else
                _animator.Play(IDLE);
        }

        private void StartRegistrationAnimation()
        {
            _isPlayingRegistrationAnimation = true;
            _animator.SetFloat(SEARCHSPEED, _searchSpeed);
            _animator.Play(SEARCH);
        }
        private void OffRegistrationAnimation() => _isPlayingRegistrationAnimation = false;

        private void StartHealAnimation()
        {
            _isPlayingHealingAnimation = true;
            _animator.SetFloat(HEALSPEED, _healSpeed);
            _animator.Play(HEAL);
        }
        private void OffHealingAnimation() => _isPlayingHealingAnimation = false;

    }
}