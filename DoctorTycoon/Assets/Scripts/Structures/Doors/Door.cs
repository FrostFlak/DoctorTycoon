using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private readonly string _open = "DoorOpen";
    private readonly string _close = "DoorClose";
    public void OpenDoor() => _animator.Play(_open);

    public void CloseDoor() => _animator.Play(_close);


}
