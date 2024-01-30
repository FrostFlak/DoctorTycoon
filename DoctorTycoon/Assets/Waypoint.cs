using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private Vector3 _position;
    [SerializeField] private bool _isBusy = false;

    public bool IsBusy { get { return _isBusy; } set {  _isBusy = value; } }

    private void Start()
    {
        _position = transform.position;
    }
}
