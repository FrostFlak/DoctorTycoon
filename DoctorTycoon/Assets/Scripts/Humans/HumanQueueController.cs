using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace People
{
    public class HumanQueueController : MonoBehaviour
{
        #region SerializedFields
        [Header("Human")]
        [SerializeField] private Human _human;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private HumanBedController _humanBedController;
        [SerializeField] private HumanAnimationController _humanAnimationController;
        [Header("Places")]
        [SerializeField] private Vector3 _enterFreePlace;
        [SerializeField] private Vector3 _nextFreePlace;
        #endregion

        #region PrivateFields
        private RegistrationTable _registrationTable;
        private BedManager _bedManager;
        private int _positionIndex;
        private float _timeIntervalBtwClaimPlace = 1.5f;
        private bool _humanClaimedPosition;
        private float _timeIntervalBtwCheckQueuePlaces = 5f;
        private WaitForSeconds _waitIntervalBtwCheckQueuePlace;
        #endregion


        #region MonoBehaviour
        private void Start()
        {
            _waitIntervalBtwCheckQueuePlace = new WaitForSeconds(_timeIntervalBtwCheckQueuePlaces);
        }

        private void OnEnable()
        {
            EventsManager.Instance.OnTimerToAcceptPeopleEnd += QuitQueue;
        }

        private void OnDisable()
        {
            EventsManager.Instance.OnTimerToAcceptPeopleEnd -= QuitQueue;
        }

        #endregion

        #region Enter && Release Queue

        private bool CheckEnoughSpaceInQueue()
        {
            if (_registrationTable.Points[_registrationTable.Points.Count - 1].IsBusy)
                return false;
            else
                return true;
        }

        public IEnumerator EnterInQueue(RegistrationTable registrationTable , BedManager bedManager)
        {
            _bedManager = bedManager;
            _registrationTable = registrationTable;
            StartCoroutine(GoToStartPointPositions(_timeIntervalBtwClaimPlace));
            _humanAnimationController.SetBedManager(bedManager);
            _humanClaimedPosition = true;
            yield return new WaitForSeconds(_timeIntervalBtwClaimPlace);
            if (CheckEnoughSpaceInQueue() && !_human.IsInQueue)
            {
                Debug.Log("Enter");
                for (int i = 0; i < _registrationTable.Points.Count; i++)
                {
                    if (_registrationTable.Points[i].IsBusy)
                    {
                        Debug.Log($"Place : {_registrationTable.Points[i].name} is Busy");
                    }
                    else
                    {
                        Debug.Log($"Free Place : {_registrationTable.Points[i].name}");
                        _enterFreePlace = _registrationTable.Points[i].transform.position;
                        TakePositionInQueue(i);
                        _positionIndex = i;
                        SetAgentDestination(_enterFreePlace);
                        yield break;
                    }
                }
            }
            else
            {
                yield return _waitIntervalBtwCheckQueuePlace;
                StartCoroutine(EnterInQueue(registrationTable, bedManager));
            }
        }
        private void ReleasePlaceInQueue(int index) 
        {
            if (_registrationTable.Points[index].IsBusy) _registrationTable.Points[index].IsBusy = false;
        }
        private void TakePositionInQueue(int index)
        {
            _registrationTable.Points[index].IsBusy = true;
            _human.IsInQueue = true;
        }
        private void SetAgentDestination(Vector3 position) => _agent.SetDestination(position);
        private IEnumerator GoToStartPointPositions(float timeInterval)
        {
            if (!_humanClaimedPosition)
            {
                WaitForSeconds waitForSeconds = new WaitForSeconds(timeInterval);
                SetAgentDestination(_bedManager.WaypointPosition[Random.Range((int)WaypointsPositions.EnterHospitalPoint_1 , (int)WaypointsPositions.EnterHospitalPoint_5)].transform.position);
                yield return waitForSeconds;
            }
        }
        #endregion

        #region Next Queue Position
        private IEnumerator CheckNextPositionInQueue()
        {
            if (_human.IsInQueue)
            {
                Debug.Log("Check Next Position");
                int number = 1;
                for (int i = number; i < _registrationTable.Points.Count; i++)
                {
                    if (!_registrationTable.Points[i - number].IsBusy)
                    {
                        Debug.Log($"Next Position : {_registrationTable.Points[i - number].name}");
                        _nextFreePlace = _registrationTable.Points[i - number].transform.position;
                        SetAgentDestination(_nextFreePlace);
                        TakePositionInQueue(i - number);
                        ReleasePlaceInQueue(i);
                        _positionIndex = i - number;
                        yield break;
                    }
                }
            }
        }


        #endregion

        #region Quit Queue
    
        private bool CanQuitQueue()
        {
            if (IsOnQuitQueuePosition(_positionIndex) && _human.IsInQueue && _bedManager.IsAnyBedAvailable())
            {
                ReleasePlaceInQueue(_positionIndex);
                return true;
            }
            else
            {
                StartCoroutine(CheckNextPositionInQueue());
                return false;
            }
        }

        private bool IsOnQuitQueuePosition(int index)
        {
            if (_registrationTable.Points[index].IsExit)
            {
                Debug.Log("This Position Is Exit");
                return true;
            }
            else
            {
                return false;
            }
        }

        private void QuitQueue()
        {
            if (CanQuitQueue())
            {
                Debug.Log("Quiting Queue");
                _human.IsInQueue = false;
                _humanBedController.enabled = true;
                _human.IsGoingToBed = true;
                _humanBedController.StartCoroutine(_humanBedController.TakeBed(_bedManager));
                enabled = false;
            }

        }

        #endregion

    }

}
