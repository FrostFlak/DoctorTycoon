using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace People
{
    public class HumanPathFinder : MonoBehaviour
{
        [Header("Human")]
        [SerializeField] private Human _human;
        [SerializeField] private NavMeshAgent _agent;
        private RegistrationTable _registrationTable;

        #region MonoBehaviour
        private void Start()
        {
            _registrationTable = FindObjectOfType<RegistrationTable>();
            StartCoroutine(TakePlaceInQueue());
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
        private IEnumerator TakePlaceInQueue()
        {
            if (CheckEnoughSpaceInQueue())
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
                        EnterInFreePlaceInQueue();
                        if (IsQuitQueuePosition(i))
                        {
                            StartCoroutine(QuitQueue(i));
                        }
                        yield break;
                    }
                }
            }
            else
            {
                yield return new WaitForSeconds(4f);
                StartCoroutine(TakePlaceInQueue());
            }
        }

        private void ReleasePlaceInQueue(int index) 
        {
            if (_registrationTable.Points[index].IsBusy)
            {
                _registrationTable.Points[index].IsBusy = false;
            }
        }

        private void EnterInFreePlaceInQueue()
        {
            _agent.SetDestination(_registrationTable.FreePlace);
        }

        #endregion

        #region Next Queue Position
        private IEnumerator CheckNextPositionInQueue()
        {
            Debug.Log("Check");
            yield return new WaitForSeconds(5f);
            for (int i = 1; i < _registrationTable.Points.Count; i++)
            {
                if (!_human.LeftQueue && !_registrationTable.Points[i - 1].IsBusy)
                {
                    Debug.Log($"Next Position : {_registrationTable.Points[i - 1].name}");
                    _registrationTable.FreePlace = _registrationTable.Points[i - 1].transform.position;
                    GoNextPositionInQueue();
                    _registrationTable.Points[i - 1].IsBusy = true;
                    ReleasePlaceInQueue(i);
                    if (IsQuitQueuePosition(i - 1))
                    {
                        StartCoroutine(QuitQueue(i - 1));
                    }
                    yield break;
                }
            }
        }
        
        private void GoNextPositionInQueue()
        {
            _agent.SetDestination(_registrationTable.FreePlace);
        }

        #endregion

        #region Quit Queue
        private bool IsQuitQueuePosition(int index)
        {
            if (_registrationTable.Points[index].IsExit)
            {
                Debug.Log("Is Exit");
                return true;
            }
            else
            {
                StartCoroutine(CheckNextPositionInQueue());
                return false;
            }
        }

        private bool CheckQuitPosibility()
        {   // if is enough beds ret true && is waited enough time????
            /*   if (Mathf.Round(_registrationTable.AcceptClientProgress) >= _registrationTable.TimeToAcceptClient)
               {
                   Debug.Log("Yep");
                   return true;
               }
               else
               {
                   Debug.Log("none");
                   return false;
               }

   */
            return true;
        }

        private IEnumerator QuitQueue(int index)
        {
            if (CheckQuitPosibility())
            {
                yield return new WaitForSeconds(4f);
                Debug.Log("Quited");
                ReleasePlaceInQueue(index);
                _human.LeftQueue = true;
                _agent.SetDestination(transform.position + new Vector3(0, 0, 3f));
            //CheckFreeBedsAvailability
            //GoToFreeBed
            //instead of v
            }
        }

        #endregion

        #region Bed
        private void GoToFreeBed()
        {
            //_agent.SetDestination() free bed
        }

        #endregion
    }

}
