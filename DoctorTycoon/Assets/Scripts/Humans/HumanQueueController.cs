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
        private RegistrationTable _registrationTable;
        private int _positionIndex;


        #region MonoBehaviour
        private void Start()
        {
            _registrationTable = FindObjectOfType<RegistrationTable>();
            EventsManager.Instance.OnTimerToAcceptPeopleEnd += OnTimerToAcceptPeopleEnd;
            StartCoroutine(EnterInQueue());
            StartCoroutine(CheckNextPositionInQueue());
            
        }

        private void OnDisable()
        {
            EventsManager.Instance.OnTimerToAcceptPeopleEnd -= OnTimerToAcceptPeopleEnd;
        }

        private void OnTimerToAcceptPeopleEnd()
        {
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

        private IEnumerator EnterInQueue()
        {
            if (CheckEnoughSpaceInQueue() && !_human.LeftQueue)
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
        }
        private void SetAgentDestination(Vector3 position)
        {
            _agent.SetDestination(position);
        }


        #endregion

        #region Next Queue Position
        //????
        private IEnumerator CheckNextPositionInQueue()
        {
            if (!_human.LeftQueue)
            {
                Debug.Log("Check Next Position");
                int number = 1;
                for (int i = number; i < _registrationTable.Points.Count; i++)
                {
                    if (!_registrationTable.Points[i - number].IsBusy)
                    {
                        if (!(i - number >= number))
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
            yield return new WaitForSeconds(2f);
            StartCoroutine(CheckNextPositionInQueue());
        }
        

        #endregion

        #region Quit Queue

        private bool CanQuitQueue()
        {
            //if is available beds
            //if character filled the progress bar on table
            if (IsOnQuitQueuePosition(_positionIndex))
            {
                ReleasePlaceInQueue(_positionIndex);
                return true;
            }
            else return false;
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
            if (CanQuitQueue() && !_human.LeftQueue)
            {
                Debug.Log("Quiting Queue");
                _human.LeftQueue = true;
                //Set to free bed
                //give money and ex
                SetAgentDestination(transform.position + new Vector3(0, 0, 3f));
            }
            
        }

        #endregion

        #region Bed
        private void GoToFreeBed()
        {
            //_agent.SetDestination() free bed
            //CheckFreeBedsAvailability
            //GoToFreeBed
            //instead of v
        }

        #endregion
    }

}
