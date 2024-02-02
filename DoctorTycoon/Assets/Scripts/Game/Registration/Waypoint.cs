using UnityEngine;

namespace People
{
    public class Waypoint : MonoBehaviour
{
    [SerializeField] private Vector3 _position;
    [SerializeField] private bool _isBusy = false;
    [SerializeField] private bool _isExit;

    public bool IsBusy { get { return _isBusy; } set {  _isBusy = value; } }
    public bool IsExit { get { return _isExit; } }

    private void Start()
    {
        _position = transform.position;
    }
}

}
