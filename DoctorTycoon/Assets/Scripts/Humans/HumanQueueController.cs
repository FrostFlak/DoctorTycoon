using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace People
{
    public class HumanQueueController : MonoBehaviour
{
        [Header("Human")]
        [SerializeField] private Human _human;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private HumanBedController _humanBedController;
        private RegistrationTable _registrationTable;
        private BedManager _bedsManager;
        private int _positionIndex;


        #region MonoBehaviour
        private void Start()
        {
            _registrationTable = FindObjectOfType<RegistrationTable>();
            _bedsManager = FindObjectOfType<BedManager>();
            EventsManager.Instance.OnTimerToAcceptPeopleEnd += OnTimerToAcceptPeopleEnd;
            StartCoroutine(EnterInQueue());
        }

        private void OnDisable()
        {
            EventsManager.Instance.OnTimerToAcceptPeopleEnd -= OnTimerToAcceptPeopleEnd;
        }

        private void OnTimerToAcceptPeopleEnd()
        {
            Debug.Log(123);
            QuitQueue();
        }
        #endregion

        #region Enter && Relase Queue

        private bool CheckEnoughSpaceInQueue()
        {
            if (_registrationTable.Points[_registrationTable.Points.Count - 1].IsBusy)
                return false;
            else
                return true;
        }

        public IEnumerator EnterInQueue()
        {
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
                        Debug.Log($"Current index : {i}");
                        Debug.Log($"Free Place : {_registrationTable.Points[i].name}");
                        _registrationTable.FreePlace = _registrationTable.Points[i].transform.position;
                        TakePositionInQueue(i);
                        _positionIndex = i;
                        SetAgentDestination(_registrationTable.FreePlace);
                        yield break;
                    }
                }
            }
            else
            {
                yield return new WaitForSeconds(3f);
                StartCoroutine(EnterInQueue());
            }
        }

        private void ReleasePlaceInQueue(int index) 
        {
            if (_registrationTable.Points[index].IsBusy)
            {
                _registrationTable.Points[index].IsBusy = false;
            }
        }
        private void TakePositionInQueue(int index)
        {
            _registrationTable.Points[index].IsBusy = true;
            _human.IsInQueue = true;
        }
        private void SetAgentDestination(Vector3 position)
        {
            _agent.SetDestination(position);
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
                        _registrationTable.FreePlace = _registrationTable.Points[i - number].transform.position;
                        SetAgentDestination(_registrationTable.FreePlace);
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
            if (IsOnQuitQueuePosition(_positionIndex) && _human.IsInQueue && _bedsManager.IsAnyBedAvailable())
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
                enabled = false;
                //give money and ex
            }
            
        }

        #endregion

    }

}
