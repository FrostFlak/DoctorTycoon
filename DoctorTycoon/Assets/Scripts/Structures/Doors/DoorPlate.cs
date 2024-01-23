using UnityEngine;
using UnityEngine.Events;
using Player;

public class DoorPlate : MonoBehaviour
{
    [SerializeField] private UnityEvent _onEnter;
    [SerializeField] private UnityEvent _onExit;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharacterMovmentThirdPersonView>())
            _onEnter.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<CharacterMovmentThirdPersonView>())
            _onExit.Invoke();
    }
}
