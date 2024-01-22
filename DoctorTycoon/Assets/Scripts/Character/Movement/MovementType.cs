using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    [RequireComponent(typeof(Rigidbody), typeof(NavMeshAgent), typeof(Animator))]
    [RequireComponent(typeof(CameraType), typeof(CharacterMovmentThirdPersonView), typeof(CharacterMovmentFirstPersonView))]
    public class MovementType : MonoBehaviour
{
    private CameraType _cameraType;
    private CharacterMovmentFirstPersonView _characterMovmentFirstPersonView;
    private CharacterMovmentThirdPersonView _characterMovmentThirdPersonView;
    private void Awake()
    {
        _cameraType = GetComponent<CameraType>();
        _characterMovmentFirstPersonView = GetComponent<CharacterMovmentFirstPersonView>();
        _characterMovmentThirdPersonView = GetComponent<CharacterMovmentThirdPersonView>();
    }
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
