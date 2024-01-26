using UnityEngine;
using UnityEngine.AI;

public class HumanAnimationController : MonoBehaviour
{
    [Header("Agent")]
    [SerializeField] private NavMeshAgent _agent;
    [Header("Properties")]
    [SerializeField] private float _lookRotationSpeed;
    [SerializeField] private Animator _animator;
    private const string IDLE = "Idle";
    private const string WALK = "Walk";

    private void Update()
    {
        FaceTarget();
        StartAnimation();
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

    private void StartAnimation()
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
}
