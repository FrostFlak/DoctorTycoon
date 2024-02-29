using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    [RequireComponent(typeof(Rigidbody), typeof(NavMeshAgent), typeof(Animator))]
    [RequireComponent(typeof(CameraType), typeof(CharacterMovmentFirstPersonView))]
    [RequireComponent(typeof(CharacterAnimationController))]
    public class CameraViewChanger : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private LayerMask _thirdPersonViewableLayers;
        [SerializeField] private LayerMask _firstPersonViewableLayers;
        [SerializeField] private CameraType _cameraType;
        [SerializeField] private CharacterMovmentFirstPersonView _characterMovmentFirstPersonView;
        [SerializeField] private CharacterJoystickMovement _characterJoystickMovement;

        private void Update()
        {
            CheckViewType();
        }
        private void CheckViewType()
        {
            if(_cameraType.CurrentCameraIndex == 0)
            {
                _characterJoystickMovement.enabled = true;
                _mainCamera.cullingMask = _thirdPersonViewableLayers;
                _characterMovmentFirstPersonView.enabled = false;
            }
            else if(_cameraType.CurrentCameraIndex == 1)
            {
                _characterMovmentFirstPersonView.enabled = true;
                _mainCamera.cullingMask = _firstPersonViewableLayers;
                _characterJoystickMovement.enabled= false;
            }
        }
}

}
