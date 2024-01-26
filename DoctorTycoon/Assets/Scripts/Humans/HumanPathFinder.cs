using UnityEngine;
using UnityEngine.AI;

public class HumanPathFinder : MonoBehaviour
{
    [Header("Agent")]
    [SerializeField] private NavMeshAgent _agent;
    [Header("WayPoints")]
    [SerializeField] private Transform[] _corridorWayPoints;
    [SerializeField] private int _corridorWayPointIndex;
    [SerializeField] private RegistrationTable _registrationTable;



    private Vector3 _targetPosition;
    [SerializeField] private Vector3 _freePlace;


    private void Start()
    {
        _registrationTable = FindObjectOfType<RegistrationTable>();
        FindFreePlace();
    }
    private void FixedUpdate()
    {
        GoToFreePlace();

    }

    private void FindFreePlace()
    {
        Debug.Log(123);
        foreach(Transform freePlace in _registrationTable.FreePlaces)
        {
            Debug.Log($"{freePlace.name}");
            _freePlace = new Vector3(freePlace.transform.position.x , 0f, freePlace.transform.position.z);
            foreach (StayWayPoint point in _registrationTable.Points)
            {
                point._isBusy = true;
                return;
            }
        }
        
    }

    private void GoToFreePlace()
    {
        _agent.SetDestination(_freePlace);
    }
    private void UpdateDestination()
    {
        _targetPosition = _corridorWayPoints[_corridorWayPointIndex].position;
        _agent.SetDestination(_targetPosition);
    }

    private void ItterateWaypointIndex()
    {
        _corridorWayPointIndex++;
        if (_corridorWayPointIndex == _corridorWayPoints.Length)
        {
            _agent.isStopped = true;
        }
        else
        {
            _agent.isStopped = false;
        }
    }

}
