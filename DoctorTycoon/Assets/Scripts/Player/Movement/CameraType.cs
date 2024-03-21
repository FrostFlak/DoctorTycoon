using UnityEngine;
using UnityEngine.AI;

public enum CameraTypes
{
    ThirdPerson = 0,
    FirstPerson = 1,
}
namespace Player
{
    [RequireComponent(typeof(Rigidbody), typeof(NavMeshAgent), typeof(Animator))]
    [RequireComponent(typeof(CameraViewChanger), typeof(CharacterMovmentFirstPersonView) , typeof(CharacterJoystickMovement))]
    [RequireComponent(typeof(CharacterAnimationController) , typeof(SensitivityChanger))]
    public class CameraType : MonoBehaviour
    {
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private LayerMask _thirdPersonViewableLayers;
        [SerializeField] private LayerMask _firstPersonViewableLayers;
        [SerializeField] private CameraViewChanger _cameraViewChanger;
        [SerializeField] private CharacterMovmentFirstPersonView _characterMovmentFirstPersonView;
        [SerializeField] private CharacterJoystickMovement _characterJoystickMovement;
        [SerializeField] private FloatingJoystick _joystick;
        [SerializeField] private GameObject _crosshair;

        private void Update()
        {
            CheckViewType();
        }
        private void CheckViewType()
        {
            if(_cameraViewChanger.CurrentCameraIndex == (int)CameraTypes.ThirdPerson)
            {
                _characterMovmentFirstPersonView.IsWalking = false;
                _characterJoystickMovement.enabled = true;
                _joystick.TurnOnJoystick();
                _mainCamera.cullingMask = _thirdPersonViewableLayers;
                _characterMovmentFirstPersonView.enabled = false;
                _crosshair.SetActive(false);
            }
            else if(_cameraViewChanger.CurrentCameraIndex == (int)CameraTypes.FirstPerson)
            {
                _characterJoystickMovement.IsWalking = false;
                _joystick.TurnOffJoystick();
                _characterMovmentFirstPersonView.enabled = true;
                _mainCamera.cullingMask = _firstPersonViewableLayers;
                _characterJoystickMovement.enabled = false;
                _crosshair.SetActive(true);
            }
        }
    }

}
