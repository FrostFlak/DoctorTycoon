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
        private bool _inZone;
        [SerializeField] private int _minMoney = 15;
        [SerializeField] private int _maxMoney = 30;
        private int _minExpirience = 1;
        private int _maxExpirience = 10;
        [SerializeField] private float _multiplier; 
        #endregion

        #region Properties
        public bool InZone { get { return _inZone; } }
        public Vector3 QuitQueuePosition { get { return _quitQueuePosition; } private set { } }
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
            EventsManager.Instance.OnTimerToAcceptPeopleEnd += IncreaseRecivedMoney;
        }

        private void OnDisable()
        {
            EventsManager.Instance.OnRegistrationUpgraded -= SetUpgradedSpeed;
            EventsManager.Instance.OnTimerToAcceptPeopleEnd -= IncreaseRecivedMoney;

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

        private void IncreaseRecivedMoney()
        {
            _minMoney = Mathf.RoundToInt(_multiplier * _minMoney);
            _maxMoney = Mathf.RoundToInt(_multiplier * _maxMoney);
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
        
        private void ReciveMoney(int MinMoney , int MaxMoney)
        {
            int claimedMoney = UnityEngine.Random.Range(MinMoney, MaxMoney);
            _moneyWallet.AddMoney(claimedMoney);
        }

        private void ReciveExperience(int MinExperience , int MaxExperience)
        {
            int claimedExpirience = UnityEngine.Random.Range(MinExperience, MaxExperience);
            _levelUpgrader.TryAddExperience(claimedExpirience);
        }

    }

}
