using Cinemachine;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public class CharacterMovmentFirstPersonView : MonoBehaviour
{
        [SerializeField] private float _lookRotationSpeed;
        [SerializeField] private bool _isWalking;
        [SerializeField] private CinemachineVirtualCamera _camera;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Animator _animator;
        private bool _lockCursor = true;
        private Vector3 _targetVelocity;
        private float _horizontal;
        private float _vertical;
        private float _yaw = 0.0f;
        private float _pitch = 0.0f;

        public bool IsWalking {  get { return _isWalking; } }

        private void Update()
        {
            AssignMoveButtons();
            RotateFace();
            Move();
        }

        private void AssignMoveButtons()
        {
            _horizontal = Input.GetAxisRaw("Horizontal");
            _vertical = Input.GetAxisRaw("Vertical"); 
        }
        private void Move()
        {
        _targetVelocity = new Vector3(_horizontal, 0, _vertical);

            if (_targetVelocity.x != 0 || _targetVelocity.z != 0)
            {
                _isWalking = true;
            }
            else
            {
                _isWalking = false;
            }
            _agent.ResetPath();
            _targetVelocity = transform.TransformDirection(_targetVelocity) * _agent.speed * Time.deltaTime;
            _agent.Move(_targetVelocity);
        }

        private void RotateFace()
        {
            if (_lockCursor)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
            _yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * _lookRotationSpeed;
            transform.localEulerAngles = new Vector3(0, _yaw, 0);

            _pitch = Mathf.Clamp(_pitch, -180, 180);
            _camera.transform.localEulerAngles = new Vector3(_pitch, 0, 0);
        }
}

}
