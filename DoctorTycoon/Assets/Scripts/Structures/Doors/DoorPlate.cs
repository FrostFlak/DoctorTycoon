using UnityEngine;
using UnityEngine.Events;
using Player;
using People;

public class DoorPlate : MonoBehaviour
{
    [SerializeField] private UnityEvent _onEnter;
    [SerializeField] private UnityEvent _onExit;
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out CameraViewChanger player) || other.TryGetComponent(out Human human))
            _onEnter.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out CameraViewChanger player) || other.TryGetComponent(out Human human))
            _onExit.Invoke();
    }
}
