using Cinemachine;
using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    public class CharacterMovmentFirstPersonView : CharacterMovment
    {
        #region SerializedFields
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        #endregion

        #region PrivateFields
        private bool _isCursorLocked;
        private Vector3 _targetVelocity;
        private float _horizontal;
        private float _vertical;
        private string _stringHorizontalAxis = "Horizontal";
        private string _stringVerticalAxis = "Vertical";
        private string _mouseXAxis = "Mouse X";
        private float _yaw = 0.0f;
        private float _pitch = 0.0f;
        #endregion

        #region Properties
        public bool IsCursorLocked {  get { return _isCursorLocked; } set {  _isCursorLocked = value; } }
        public CinemachineVirtualCamera VirtualCamera { get { return _virtualCamera; } set { _virtualCamera = value; } }
        #endregion

        private void OnEnable()
        {
            _isCursorLocked = true;
            //Cursor.lockState = CursorLockMode.Locked;
        }

        private void OnDisable()
        {
            _isCursorLocked = false;
            Cursor.lockState = CursorLockMode.None;
        }

        private void Update()
        {
            AssignInputs();
            TargetFace();
            Move(Agent);
        }

        public override void AssignInputs()
        {
            _horizontal = Input.GetAxisRaw(_stringHorizontalAxis);
            _vertical = Input.GetAxisRaw(_stringVerticalAxis); 
        }
        public override void Move(NavMeshAgent agent)
        {
            _targetVelocity = new Vector3(_horizontal, 0, _vertical);

            if ((_targetVelocity.x != 0 || _targetVelocity.z != 0) && CameraType.CurrentCameraIndex == (int)CameraTypes.FirstPerson)
            {
                IsWalking = true;
            }
            else
            {
                IsWalking = false;
            }
            agent.ResetPath();
            _targetVelocity = transform.TransformDirection(_targetVelocity) * agent.speed * Time.deltaTime;
            agent.Move(_targetVelocity);
        }

        public override void TargetFace()
        {
            _yaw = transform.localEulerAngles.y + Input.GetAxis(_mouseXAxis) * LookRotationSpeed;
            transform.localEulerAngles = new Vector3(0, _yaw, 0);

            _pitch = Mathf.Clamp(_pitch, -180, 180);
            _virtualCamera.transform.localEulerAngles = new Vector3(_pitch, 0, 0);

        }

    }

}
