using Player;
using System.Collections.Generic;
using UnityEngine;

namespace People
{
    public class RegistrationTable : MonoBehaviour
{
        #region Serialized Fields
        [SerializeField] private List<Waypoint> _points = new List<Waypoint>();
        [SerializeField] private float _acceptClientProgress;
        [SerializeField] private float _timeToAcceptClient;
        #endregion

        #region Private Fields
        private BedManager _bedManager;
        private Vector3 _quitQueuePosition;
        private Vector3 _freePlace;
        private bool _inZone;
        #endregion

        #region Properties
        public bool InZone { get { return _inZone; } }
        public Vector3 QuitQueuePosition { get { return _quitQueuePosition; } private set { } }
        public Vector3 FreePlace { get { return _freePlace; } set { _freePlace = value; } }
        public List<Waypoint> Points { get { return _points; } set { _points = value; } }
        public float AcceptClientProgress { get { return _acceptClientProgress; } private set { } }
        public float TimeToAcceptClient { get { return _timeToAcceptClient; } }
        #endregion

        private void Start()
        {
            _bedManager = FindObjectOfType<BedManager>();
            _quitQueuePosition = _points[0].transform.position;
        }

        private void Update()
        {
            if (!_inZone)
                DecreaseProgress();
            IsSomeoneInQueue();
        }

        private bool IsSomeoneInQueue()
        {
            for(int i = 0; i < _points.Count; i++)
            {
                if (_points[i].IsBusy)
                    return true;
                else
                    return false;
            }
            return false;
        }

        private void OnTriggerStay(Collider other)
        {
            if(other.TryGetComponent(out CameraViewChanger player) && IsSomeoneInQueue() && _bedManager.IsAnyBedAvailable() && _points[0].IsBusy)
            {
                _inZone = true;
                EventsManager.Instance.OnStayInRegistrationTriggerZoneEvent();
                _acceptClientProgress += Time.deltaTime;
                if (_acceptClientProgress >= _timeToAcceptClient)
                {
                    EventsManager.Instance.OnTimerToAcceptPeopleEndEvent();
                    _acceptClientProgress = 0;
                }
            }
            
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.TryGetComponent(out CameraViewChanger player))
            {
                _inZone = false;
                EventsManager.Instance.OnExitRegistartionTriggerZoneEvent();
            }
        }

        private void DecreaseProgress()
        {
            _acceptClientProgress -= Time.deltaTime;
            if (_acceptClientProgress < 0)
            {
                _acceptClientProgress = 0;
            }
        }






    }

}
