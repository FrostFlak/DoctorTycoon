using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace People
{
    public class HumanBedController : MonoBehaviour
    {
        [SerializeField] private Human _human;
        [SerializeField] private NavMeshAgent _agent;
        private Vector3 _layPosition;
        private Vector3 _releasePosition;
        private BedManager _bedsManager;
        private int _index;
        public int Index { get { return _index; } }
        public Vector3 LayPosition {  get { return _layPosition; } }
        public Vector3 ReleasePosition { get { return _releasePosition; } }

        private void Start()
        {
            _bedsManager = FindObjectOfType<BedManager>();
            EventsManager.Instance.OnTimerToHealPatinetEnd += OnTimerToHealPeopleEnd;
            StartCoroutine(TakeBed());
        }

        private void OnDisable()
        {
            EventsManager.Instance.OnTimerToHealPatinetEnd -= OnTimerToHealPeopleEnd;
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
                    SetPositions(i);
                    _human.IsLaying = true;
                    _index = i;
                    SetAgentDestination(_bedsManager.FreeBed);
                    yield break;
                }
            }
        }

        private void SetAgentDestination(Vector3 position)
        {
            _agent.SetDestination(position);
        }

        private void SetPositions(int i)
        {
            _bedsManager.FreeBed = _bedsManager.Beds[i].transform.position;
            _layPosition = _bedsManager.Beds[i].transform.position;
            _releasePosition = _bedsManager.Beds[i].transform.position - new Vector3(-2f, 0f, 0f);
        }

        private void ReleaseBed(int index)
        {
            if (_bedsManager.Beds[index].IsBusy)
            {
                _bedsManager.Beds[index].IsBusy = false;
            }
        }

        private void QuitBed()
        {
            if (_bedsManager.Beds[_index].CanLeaveBed && _human.IsLaying)
            {
                ReleaseBed(_index);
                _human.IsLaying = false;
                _human.LeftBed = true;
                _bedsManager.Beds[_index].CanLeaveBed = false;
                EventsManager.Instance.OnPatientLeaveBedEvent(_human);
                SetAgentDestination(_bedsManager.QuitPosition);
            }
        }

        

    }

}
