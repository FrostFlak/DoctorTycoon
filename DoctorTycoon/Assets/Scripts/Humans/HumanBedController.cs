using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace People
{
    public class HumanBedController : MonoBehaviour
    {
        [SerializeField] private Human _human;
        [SerializeField] private NavMeshAgent _agent;
        private BedManager _bedsManager;

        private void Start()
        {
            _bedsManager = FindObjectOfType<BedManager>();
            EventsManager.Instance.OnTimerToPeopleLayEnd += OnTimerToHealPeopleEnd;
            StartCoroutine(TakeBed());
        }

        private void OnDisable()
        {
            EventsManager.Instance.OnTimerToPeopleLayEnd -= OnTimerToHealPeopleEnd;
        }
        private void OnTimerToHealPeopleEnd()
        {
            QuitBed();
        }

        private IEnumerator TakeBed()
        {
            for (int i = 0; i < _bedsManager.Beds.Count; i++)
            {
                if (_bedsManager.Beds[i].IsBusy)
                {
                    Debug.Log($"Place : {_bedsManager.Beds[i].name} is Busy");
                }
                else
                {
                    Debug.Log($"Free Bed : {_bedsManager.Beds[i].name}");
                    _bedsManager.Beds[i].IsBusy = true;
                    _bedsManager.FreeBed = _bedsManager.Beds[i].transform.position;
                    SetAgentDestination(_bedsManager.FreeBed);
                    yield break;
                }
            }
        }

        private void SetAgentDestination(Vector3 position)
        {
            _agent.SetDestination(position);
        }

        private void ReleaseBed(int index)
        {
            if (_bedsManager.Beds[index].IsBusy)
            {
                _bedsManager.Beds[index].IsBusy = false;
            }
        }

        private bool CanQuitQueue()
        {
            //if character filled the healing progress bar at the bed
            return true;
        }

        private void QuitBed()
        {
            throw new NotImplementedException();
        }


    }

}
