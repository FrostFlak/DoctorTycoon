using Player;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

namespace People
{
    public class RegistrationTable : MonoBehaviour
{
        [SerializeField] private List<Waypoint> _points = new List<Waypoint>();
        [SerializeField] private Vector3 _quitQueuePosition;
        [SerializeField] private float _acceptClientProgress;
        [SerializeField] private float _timeToAcceptClient;
        private Vector3 _freePlace;
        private bool _inZone;
        private bool _canQuitQueue;
        public event UnityAction OnStayInTriggerZone;
        public bool InZone { get { return _inZone; } }
        public bool CanQuitQueue { get { return _canQuitQueue; } set { _canQuitQueue = value; } }
        public Vector3 QuitQueuePosition { get { return _quitQueuePosition; } private set { } }
        public Vector3 FreePlace { get { return _freePlace; } set { _freePlace = value; } }
        public List<Waypoint> Points { get { return _points; } set { _points = value; } }
        public float AcceptClientProgress { get { return _acceptClientProgress; } private set { } }
        public float TimeToAcceptClient { get { return _timeToAcceptClient; } }

        private void Start()
        {
            _quitQueuePosition = _points[0].transform.position;
        
        }

        private void OnTriggerStay(Collider other)
        {
            if(other.TryGetComponent(out MovementType player))
            {
                for (int i = 0; i < Points.Count; i++)
                {
                    if (!Points[i].IsBusy)
                    {
                        _inZone = true;
                        OnStayInTriggerZone?.Invoke();
                        _acceptClientProgress += Time.deltaTime;
                            _canQuitQueue = true;
                        if (_acceptClientProgress >= _timeToAcceptClient)
                            _acceptClientProgress = 0;
                        break;
                    }   
                }
            }
            
        }

        private void OnTriggerExit(Collider other)
        {
            if(other.TryGetComponent(out MovementType player))
            {
                _inZone = false;
                _canQuitQueue = false;
            }
        }

        private void Update()
        {
            if (!_inZone)
                DecreaseProgress();
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
