using Cinemachine;
using UnityEngine;

public class TutorialCameraChanger : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _tableCamera;
    [SerializeField] private CinemachineVirtualCamera _bedsCamera;


    public void TurnOnTableCamera() => _tableCamera.Priority = 5;
    public void TurnOnBedsCamera() => _bedsCamera.Priority = 6;
    public void TurnOffTableCamera() => _tableCamera.Priority = -3;
    public void TurnOffBedsCamera() => _bedsCamera.Priority = -4;
}
