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
        [SerializeField] private MoneyWallet _moneyWallet;
        [SerializeField] private LevelUpgrader _levelUpgrader;
        [SerializeField] private BedManager _bedManager;
        #endregion

        #region Private Fields
        private Vector3 _quitQueuePosition;
        private Vector3 _freePlace;
        private bool _inZone;
        private float _minMoney = 75;
        private float _maxMoney = 100;
        private int _minExpirience = 1;
        private int _maxExpirience = 99;
        #endregion

        #region Properties
        public bool InZone { get { return _inZone; } }
        public Vector3 QuitQueuePosition { get { return _quitQueuePosition; } private set { } }
        public Vector3 FreePlace { get { return _freePlace; } set { _freePlace = value; } }
        public List<Waypoint> Points { get { return _points; } set { _points = value; } }
        public float AcceptClientProgress { get { return _acceptClientProgress; } private set { } }
        public float TimeToAcceptClient { get { return _timeToAcceptClient; } set { _timeToAcceptClient = value; } }
        #endregion

        public void Initialize()
        {
            _quitQueuePosition = _points[0].transform.position;
            SetUpgradedSpeed();
        }

        private void Start()
        {
            EventsManager.Instance.OnRegistrationUpgraded += SetUpgradedSpeed;
        }

        private void OnDisable()
        {
            EventsManager.Instance.OnRegistrationUpgraded -= SetUpgradedSpeed;
        }

        private void Update()
        {
            if (!_inZone)
                DecreaseProgress();
            IsSomeoneInQueue();
        }
        private void SetUpgradedSpeed()
        {
            _timeToAcceptClient = SaveSystem.ShopData.CurrentUpgradeRegistartionSpeed;
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
                    ReciveMoney(_minMoney , _maxMoney);
                    ReciveExperience(_minExpirience, _maxExpirience);
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
        
        private void ReciveMoney(float MinMoney , float MaxMoney)
        {
            float claimedMoney = Random.Range(MinMoney, MaxMoney);
            _moneyWallet.AddMoney((long)claimedMoney);
        }

        private void ReciveExperience(int MinExperience , int MaxExperience)
        {
            int claimedExpirience = Random.Range(MinExperience, MaxExperience);
            _levelUpgrader.TryAddExperience(claimedExpirience);
        }

    }

}
