using Cinemachine;
using UI;
using UnityEngine;

namespace Player
{
    public class SensitivityChanger : MonoBehaviour
{
        [SerializeField] private CharacterMovmentFirstPersonView _firstPersonController;
        [SerializeField] private SensitivityShower _sensitivityShower;
        public void Start()
        {
            _firstPersonController.LookRotationSpeedProperty = _sensitivityShower.Slider.value;
            _firstPersonController.VirtualCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = _sensitivityShower.Slider.value;
        }

        public void LockFacingRotation()
        {
            _firstPersonController.LookRotationSpeedProperty = 0;
            _firstPersonController.VirtualCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = 0;
        }
        public void UnLockFacingRotation()
        {
            _firstPersonController.LookRotationSpeedProperty = _sensitivityShower.Slider.value;
            _firstPersonController.VirtualCamera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = _sensitivityShower.Slider.value;
        }
}

}
