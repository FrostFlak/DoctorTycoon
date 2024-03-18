using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace People
{
    public class HumanBedController : MonoBehaviour
    {
        #region SerializedFields
        [SerializeField] private Human _human;
        [SerializeField] private NavMeshAgent _agent;
        #endregion

        #region PrivateFieleds
        private Vector3 _layPosition;
        private Vector3 _releasePosition;
        private BedManager _bedManager;
        private int _index;
        private const float _onQuitWaitingTimeInterval = 2.5f;
        private const float _onEntraceWaitingTimeInterval = 2f;
        #endregion

        #region Properties
        public int Index { get { return _index; } }
        public Vector3 LayPosition {  get { return _layPosition; } }
        public Vector3 ReleasePosition { get { return _releasePosition; } }

        #endregion

        private void OnEnable()
        {
            EventsManager.Instance.OnTimerToHealPatinetEnd += QuitBed;
        }

        private void OnDisable()
        {
            EventsManager.Instance.OnTimerToHealPatinetEnd -= QuitBed;
        }

        public IEnumerator TakeBed(BedManager bedManager)
        {

            _bedManager = bedManager;
            for (int i = 0; i < _bedManager.Beds.Count; i++)
            {
                if (_bedManager.Beds[i].IsBusy)
                {
                    Debug.Log($"Place : {_bedManager.Beds[i].name} is Busy");
                }
                else
                {
                    Debug.Log($"Free Bed : {_bedManager.Beds[i].name}");
                    _bedManager.Beds[i].IsBusy = true;
                    SetPositions(i);
                    _human.IsLaying = true;
                    _index = i;
                    StartCoroutine(GoToWayPointPosition(i, _onEntraceWaitingTimeInterval, _bedManager.FreeBed));
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
            _bedManager.FreeBed = _bedManager.Beds[i].transform.position;
            _layPosition = _bedManager.Beds[i].transform.position;
            _releasePosition = _bedManager.Beds[i].transform.position - new Vector3(-2f, 0f, 0f);
        }
        private void ReleaseBed(int index)
        {
            if (_bedManager.Beds[index].IsBusy)
            {
                _bedManager.Beds[index].IsBusy = false;
            }
        }

        private IEnumerator GoToWayPointPosition(int index , float timeInterval ,Vector3 position)
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds(timeInterval);
            if (index >= 0 && index <= 2)
            {
                SetAgentDestination(_bedManager.WaypointPosition[(int)WaypointsPositions.LeftDownRoom].transform.position);
                yield return waitForSeconds;
                SetAgentDestination(position);
            }
            else if (index >= 3 && index <= 5)
            {
                SetAgentDestination(_bedManager.WaypointPosition[(int)WaypointsPositions.RightDownRoom].transform.position);
                yield return waitForSeconds;
                SetAgentDestination(position);
            }
            else if (index >= 6 && index <= 8)
            {
                SetAgentDestination(_bedManager.WaypointPosition[(int)WaypointsPositions.LeftUpRoom].transform.position);
                yield return waitForSeconds;
                SetAgentDestination(position);
            }
            else if (index >= 9 && index <= 11)
            {
                SetAgentDestination(_bedManager.WaypointPosition[(int)WaypointsPositions.RightUpRoom].transform.position);
                yield return waitForSeconds;
                SetAgentDestination(position);
            }
            else if (index > 11)
            {
                yield return waitForSeconds;
                SetAgentDestination(position);
            }
        }
        private IEnumerator GoToWayPointPosition(int index, float timeInterval, Vector3 firstPosition , Vector3 secondPosition)
        {
            WaitForSeconds waitForSeconds = new WaitForSeconds(timeInterval);
            if (index >= 0 && index <= 2)
            {
                SetAgentDestination(_bedManager.WaypointPosition[(int)WaypointsPositions.LeftDownRoom].transform.position);
                yield return waitForSeconds;
                SetAgentDestination(firstPosition);
                yield return waitForSeconds;
                SetAgentDestination(secondPosition);
            }
            else if (index >= 3 && index <= 5)
            {
                SetAgentDestination(_bedManager.WaypointPosition[(int)WaypointsPositions.RightDownRoom].transform.position);
                yield return waitForSeconds;
                SetAgentDestination(firstPosition);
                yield return waitForSeconds;
                SetAgentDestination(secondPosition); 
            }
            else if (index >= 6 && index <= 8)
            {
                SetAgentDestination(_bedManager.WaypointPosition[(int)WaypointsPositions.LeftUpRoom].transform.position);
                yield return waitForSeconds;
                SetAgentDestination(firstPosition);
                yield return waitForSeconds;
                SetAgentDestination(secondPosition);
            }
            else if (index >= 9 && index <= 11)
            {
                SetAgentDestination(_bedManager.WaypointPosition[(int)WaypointsPositions.RightUpRoom].transform.position);
                yield return waitForSeconds;
                SetAgentDestination(firstPosition);
                yield return waitForSeconds;
                SetAgentDestination(secondPosition);
            }
            else if (index > 11)
            {
                yield return waitForSeconds;
                SetAgentDestination(firstPosition);
                yield return waitForSeconds;
                SetAgentDestination(secondPosition);
            }
        }
        private void QuitBed()
        {
            if (_bedManager.Beds[_index].CanLeaveBed && _human.IsLaying)
            {
                ReleaseBed(_index);
                _human.IsLaying = false;
                _human.LeftBed = true;
                _bedManager.Beds[_index].CanLeaveBed = false;
                EventsManager.Instance.OnPatientLeaveBedEvent(_human);
                StartCoroutine(GoToWayPointPosition(_index,
                    _onQuitWaitingTimeInterval,
                    _bedManager.WaypointPosition[(int)WaypointsPositions.MiddleDoor].transform.position,
                    _bedManager.WaypointPosition[(int)WaypointsPositions.QuitHospital].transform.position));
                
            }
        }

        

    }

}
