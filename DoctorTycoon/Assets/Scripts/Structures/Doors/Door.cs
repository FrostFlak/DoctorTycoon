using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private bool _canSwitch = true;
    private readonly string _open = "DoorOpen";
    private readonly string _close = "DoorClose";
    public void OpenDoor()
    {
        if (_canSwitch)
            _animator.Play(_open);
    }

    public void CloseDoor()
    {
        if (!_canSwitch)
            _animator.Play(_close);
    }

    public void SwitchState() => _canSwitch = !_canSwitch;

}
