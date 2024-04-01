using Player;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace People
{
    public class Bed : MonoBehaviour
{
        [SerializeField] private bool _isBusy;
        [SerializeField] private bool _isPurchased;
        [SerializeField] private float _timeToHeal;
        [SerializeField] private float _maxTimeToHeal;
        [SerializeField] private bool _playerInZone;
        [SerializeField] private Image _progressBar;
        [SerializeField] private GameObject _progressBarParent;
        [SerializeField] private GameObject _bedObject;
        [SerializeField] private ParticleSystem _heartParticle;
        private bool _canLeaveBed;

        public bool IsBusy { get { return _isBusy; } set { _isBusy = value; } }
        public bool IsPurchased { get { return _isPurchased; } set { _isPurchased = value; } }
        public bool CanLeaveBed { get { return _canLeaveBed; } set { _canLeaveBed = value; } }
        public float TimeToHeal { get { return _timeToHeal; } }
        public float MaxTimeToHeal { get { return _maxTimeToHeal; } set { _maxTimeToHeal = value; } }
        public GameObject BedObject {  get { return _bedObject; } set { _bedObject = value; } }

        private void Start()
        {
            _progressBarParent.SetActive(false);
        }

        private void Update()
        {
            if (!_playerInZone)
            {
                DecreaseProgress();
                DecreaseProgressBar();
            }
            if (_canLeaveBed)
                _isBusy = false;
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out CameraViewChanger player))
            {
                _playerInZone = true;
                if(_isBusy)
                    _progressBarParent.SetActive(true);
            }
            else if(other.TryGetComponent(out Human human))
                human.IsGoingToBed = false;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out CameraViewChanger player) && _isBusy)
            {
                EventsManager.Instance.OnStayInBedTriggerZoneEvent();
                _timeToHeal += Time.deltaTime;
                IncreaseProgressBar();
                if (_timeToHeal >= _maxTimeToHeal)
                {
                    _canLeaveBed = true;
                    EventsManager.Instance.OnTimerToHealPatinetEndEvent();
                    _heartParticle.gameObject.SetActive(true);
                    _heartParticle.Play();
                    _timeToHeal = 0;
                    _progressBarParent.SetActive(false);
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out CameraViewChanger player))
            {
                EventsManager.Instance.OnExitBedTriggerZoneEvent();
                _playerInZone = false;
                _progressBarParent.SetActive(false);
            }
        }

        private void DecreaseProgress()
        {
            _timeToHeal -= Time.deltaTime;
            if (_timeToHeal < 0)
            {
                _timeToHeal = 0;
            }
        }

        private void IncreaseProgressBar()
        {
            _progressBar.fillAmount = (_timeToHeal / _maxTimeToHeal) / 1f;
            if (_progressBar.fillAmount == 1)
            {
                _progressBar.fillAmount = 0f;
            }
        }
        private void DecreaseProgressBar()
        {
            _progressBar.fillAmount = (_timeToHeal / _maxTimeToHeal) / 1f;
        }
    }

}