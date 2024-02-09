using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    [RequireComponent(typeof(Rigidbody), typeof(NavMeshAgent), typeof(Animator))]
    [RequireComponent(typeof(CameraType), typeof(CharacterClickMovmentThirdPersonView), typeof(CharacterMovmentFirstPersonView))]
    [RequireComponent(typeof(CharacterAnimationController))]
    public class CameraViewChanger : MonoBehaviour
    {
        [SerializeField] private CameraType _cameraType;
        [SerializeField] private CharacterMovmentFirstPersonView _characterMovmentFirstPersonView;
        [SerializeField] private CharacterClickMovmentThirdPersonView _characterMovmentThirdPersonView;
        [SerializeField] private CharacterJoystickMovement _characterJoystickMovement;

        private void Update()
        {
            CheckViewType();
        }
        private void CheckViewType()
        {
            if(_cameraType.CurrentCameraIndex == 0)
            {
                _characterMovmentThirdPersonView.enabled = true;
                _characterMovmentFirstPersonView.enabled = false;
            }
            else if(_cameraType.CurrentCameraIndex == 1)
            {
                _characterMovmentFirstPersonView.enabled = true;
                _characterMovmentThirdPersonView.enabled= false;
            }
        }
}

}
