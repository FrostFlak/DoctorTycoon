using UnityEngine;

public class Door : MonoBehaviour
{
    private Animator _animator;
    private readonly string _open = "DoorOpen";
    private readonly string _close = "DoorClose";

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        _animator.Play(_open);
    }

    public void CloseDoor()
    {
        _animator.Play(_close);
    }



}
