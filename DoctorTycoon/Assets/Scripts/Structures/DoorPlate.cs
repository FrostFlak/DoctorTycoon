using UnityEngine;
using UnityEngine.Events;

public class DoorPlate : MonoBehaviour
{
    [SerializeField] private UnityEvent _onEnter;
    [SerializeField] private UnityEvent _onExit;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterMovment>())
            _onEnter.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharacterMovment>())
            _onExit.Invoke();
    }
}
