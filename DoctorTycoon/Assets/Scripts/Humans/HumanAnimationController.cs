using UnityEngine;
using UnityEngine.AI;

namespace People
{
    public class HumanAnimationController : MonoBehaviour
    {
        [Header("Agent")]
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Human _human;
        [SerializeField] private HumanBedController _humanBedController;
        [Header("Properties")]
        [SerializeField] private float _lookRotationSpeed;
        [SerializeField] private Animator _animator;
        private BedManager _bedManager;
        private const string IDLE = "Idle";
        private const string WALK = "Walk";
        private const string SLEEP = "Sleep";
        private const string INJURED_WALK = "InjuredWalk";

        private void OnEnable()
        {
            EventsManager.Instance.OnTimerToHealPatinetEnd += TurnOnAgent;
        }

        private void OnDisable()
        {
            EventsManager.Instance.OnTimerToHealPatinetEnd -= TurnOnAgent;
        }
        private void Update()
        {
            if ((_human.IsLaying && _human.IsGoingToBed) || (!_human.IsLaying && !_human.IsGoingToBed))
            {
                TargetFace();
                StartMoveAnimation();
            }
            else if(_human.IsLaying && !_human.LeftBed)
            {
                TargetFaceOnLay();
                StartLayAnimation();
            }
        }

        public void SetBedManager(BedManager bedManager) => _bedManager = bedManager;
        private void TargetFace()
        {
            if (_agent.velocity != Vector3.zero)
            {
                Vector3 direction = (_agent.destination - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x + 0.001f, 0, direction.z + 0.001f));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _lookRotationSpeed);
            }
        }
        private void TargetFaceOnLay()
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(0f , 0f , 0f + 0.00001f));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _lookRotationSpeed);
        }
        private void StartMoveAnimation()
        {
            if (_agent.velocity == Vector3.zero)
                _animator.Play(IDLE);
            else if (_agent.velocity != Vector3.zero && _human.LeftBed)
                _animator.Play(WALK);
            else if (_agent.velocity != Vector3.zero && !_human.LeftBed)
                _animator.Play(INJURED_WALK);
        }

        private void StartLayAnimation()
        { 
            if(_agent.enabled)
            {
                if (_agent.remainingDistance == 0)
                {
                    transform.position = _humanBedController.LayPosition;
                    _agent.ResetPath();
                    _agent.enabled = false;
                    _animator.Play(SLEEP);
                }
            }
        }

        private void TurnOnAgent()
        {
            if (_bedManager.Beds[_humanBedController.Index].CanLeaveBed && _human.IsLaying && !_human.LeftBed) _agent.enabled = true;      
        }

    }

}
