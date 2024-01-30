using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RegistrationTable : MonoBehaviour
{
    [SerializeField] private List<Waypoint> _points = new List<Waypoint>();
    [SerializeField] private Vector3 _quitQueuePosition;
    [SerializeField] private Vector3 _freePlace;
    [SerializeField] private float _acceptClientProgress;
    [SerializeField] private float _timeToAcceptClient;
    public Vector3 QuitQueuePosition { get { return _quitQueuePosition; } private set { } }
    public Vector3 FreePlace { get { return _freePlace; } set { _freePlace = value; } }
    public List<Waypoint> Points { get { return _points; } set { _points = value; } }

    private void Start()
    {
        _quitQueuePosition = _points[0].transform.position;
        
    }

    private void OnTriggerStay(Collider other)
    {
        _acceptClientProgress += Time.deltaTime;
        if(_acceptClientProgress >= _timeToAcceptClient) 
            _acceptClientProgress = 0;
    }






}
