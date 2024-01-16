using System;
using UnityEngine;
using UnityEngine.AI;

public class CharacterMovment : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private LayerMask _clickableLayer;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _lookRotationSpeed;
    private const string IDLE = "Idle";
    private const string WALK = "Walk";
    private Mouse _input;
    private NavMeshAgent _agent;
    private Animator _animator;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        _input = new Mouse();
        AssignInputs();
    }
    private void Update()
    {
        FaceTarget();
        StartAnimation();
    }
  

    private void AssignInputs()
    {
        _input.Main.Move.performed += ctx => ClickToMove();
    }

    private void ClickToMove()
    {
        RaycastHit hit;
        if(Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out hit, 100, _clickableLayer))
        {
            _agent.destination = hit.point;   
        }
    }

    void FaceTarget()
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
        if(_agent.velocity == Vector3.zero)
        {
            _animator.Play(IDLE);
        }
        else
        {
            _animator.Play(WALK);
        }
    }
    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

}
