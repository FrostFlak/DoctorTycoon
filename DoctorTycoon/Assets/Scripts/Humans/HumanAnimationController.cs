using UnityEngine;
using UnityEngine.AI;

namespace People
{
    public class HumanAnimationController : MonoBehaviour
    {
        [Header("Agent")]
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Human _human;
        [SerializeField] private CapsuleCollider _capsuleCollider;
        [Header("Properties")]
        [SerializeField] private float _lookRotationSpeed;
        [SerializeField] private Animator _animator;
        private const string IDLE = "Idle";
        private const string WALK = "Walk";
        private const string LAYINGDOWN = "LayingDown";

        private void Update()
        {
            if (!_human.IsLaying)
            {
                FaceTarget();
                StartMoveAnimation();
            }
            else
            {
                OnLayTargetFace();
                StartLayAnimation();
            }
        }

        private void FaceTarget()
        {
            if (_agent.velocity != Vector3.zero)
            {
                Vector3 direction = (_agent.destination - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x + 0.001f, 0, direction.z + 0.001f));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _lookRotationSpeed);
            }
        }
        private void OnLayTargetFace()
        {
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(0f , 0f , 0f + 0.00001f));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * _lookRotationSpeed);
        }


        private void StartMoveAnimation()
        {
            if (_agent.velocity == Vector3.zero)
            {
                _animator.Play(IDLE);
            }
            else
            {
                _animator.Play(WALK);
            }
        }

        private void StartLayAnimation()
        { 
            if(_agent.enabled)
            {
                if (_agent.remainingDistance == 0)
                {
                    _animator.Play(LAYINGDOWN);
                    _agent.ResetPath();
                    _agent.enabled = false;
                }
            }

        }
    }

}
