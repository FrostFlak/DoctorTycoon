using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace People
{
    public class HumanPathFinder : MonoBehaviour
{
        [Header("Agent")]
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Vector3 _goPos;
        private RegistrationTable _registrationTable;
        private void Start()
        {
            _registrationTable = FindObjectOfType<RegistrationTable>();
            StartCoroutine(CheckPlaceDisponibility());
            GoToFreePlace();
            _goPos = _registrationTable.FreePlace;
            if (CheckQuitQueuePosition())
            {
                QuitQueue();
            }
        }

        private void Update()
        {

        }
        private IEnumerator CheckPlaceDisponibility()
        {
            for (int i = 0; i < _registrationTable.Points.Count; i++)
            {
                if (_registrationTable.Points[i].IsBusy)
                { 
                    Debug.Log($"Place : {_registrationTable.Points[i].name} is Busy");
                }
                else
                {
                    Debug.Log($"Current index : {i}");
                    Debug.Log($"Free Place : {_registrationTable.Points[i].name}");
                    _registrationTable.FreePlace = _registrationTable.Points[i].transform.position;
                    _registrationTable.Points[i].IsBusy = true;
                    yield break;
                }
            }
        }

        private void GoToFreePlace()
        {
            _agent.SetDestination(_registrationTable.FreePlace);
        }

        private bool CheckQuitQueuePosition()
        {
            if (_registrationTable.Points[0].transform)
            {
                Debug.Log($"Table pos: { _registrationTable.Points[0].transform.position}");
                //Debug.Log($"Character end pos: {_agent.pathEndPosition}");

                return true;
            }
            return false;
        }

        private void QuitQueue()
        {
            Debug.Log("Quited");
            //_agent.SetDestination(transform.position + new Vector3(0 , 0 , 3f));
        }



    }

}
