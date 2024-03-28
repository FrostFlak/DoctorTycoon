using UnityEngine;

public class ObjectStateSwitcher : MonoBehaviour
{
    public void TurnOffObject() => gameObject.SetActive(false);
    public void TurnOnObject() => gameObject.SetActive(true);
}
