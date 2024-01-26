using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RegistrationTable : MonoBehaviour
{
    [SerializeField] private List<StayWayPoint> _points = new List<StayWayPoint>();
    [SerializeField] private List<Transform> _freePlaces = new List<Transform>();
    public List<StayWayPoint> Points { get { return _points; } set { _points = value; } }
    public List<Transform> FreePlaces { get { return _freePlaces; } set { _freePlaces = value; } }

    private void Update()
    {
        FindFreePlaces();
    }
    public void FindFreePlaces()
    {
        foreach(StayWayPoint point in _points)
        {
            if (point._isBusy)
            {
                point._canCome = false;
                if (_freePlaces.Count >= 1 && !point._canCome)
                {
                    _freePlaces.Remove(point._position);
                    _freePlaces = _freePlaces.Distinct().ToList();
                }
            }
            else
            {
                point._canCome = true;
                if (point._canCome && _freePlaces.Count < _points.Count)
                {
                    _freePlaces.Add(point._position);
                    _freePlaces = _freePlaces.Distinct().ToList();
                }
            }
        }   
    }
    
}

[Serializable]
public class StayWayPoint
{
    public Transform _position;
    public bool _isBusy;
    public bool _canCome;
}
