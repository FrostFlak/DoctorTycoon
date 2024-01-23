using UnityEngine;
using UnityEngine.AI;

namespace Player
{
    [RequireComponent(typeof(Rigidbody), typeof(NavMeshAgent), typeof(Animator))]
    [RequireComponent(typeof(CameraType), typeof(CharacterMovmentThirdPersonView), typeof(CharacterMovmentFirstPersonView))]
    public class MovementType : MonoBehaviour
    {
        [SerializeField] private CameraType _cameraType;
        [SerializeField] private CharacterMovmentFirstPersonView _characterMovmentFirstPersonView;
        [SerializeField] private CharacterMovmentThirdPersonView _characterMovmentThirdPersonView;

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
