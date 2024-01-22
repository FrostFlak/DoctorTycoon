using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Player
{
    public class CharacterMovmentThirdPersonView : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] private LayerMask _clickableLayer;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _lookRotationSpeed;
    [SerializeField] private ParticleSystem _clickParticle;
    private bool _lockCursor = false;
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
            if (_clickParticle != null)
                Instantiate(_clickParticle, hit.point += new Vector3(0f, 0.1f, 0f), _clickParticle.transform.rotation);
        }
    }

    private void FaceTarget()
    {
        if (!_lockCursor)
        {
            Cursor.lockState = CursorLockMode.None;
        }
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

}
