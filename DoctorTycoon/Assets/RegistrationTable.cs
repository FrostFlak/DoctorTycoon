using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RegistrationTable : MonoBehaviour
{
    [SerializeField] private List<Vector3> _points = new List<Vector3>();
    [SerializeField] private Transform _endPosition;
    public List<Vector3> Points { get { return _points; } set { _points = value; } }

    private void Start()
    {
       // ??????????
        //_endPosition = _points[_points.Count - 1];
    }
    
}
