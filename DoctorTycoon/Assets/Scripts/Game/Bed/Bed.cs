using Player;
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
        [SerializeField] private bool _inZone;
        [SerializeField] private bool _canLeaveBed;
        [SerializeField] private Image _progressBar;
        [SerializeField] private GameObject _progressBarParent;

        public bool IsBusy { get { return _isBusy; } set { _isBusy = value; } }
        public bool IsPurchased { get { return _isPurchased; } set { _isPurchased = value; } }
        public bool CanLeaveBed { get { return _canLeaveBed; } set { _canLeaveBed = value; } }

        public float TimeToHeal { get { return _timeToHeal; } }

        private void Start()
        {
            _progressBarParent.SetActive(false);
        }
        private void Update()
        {
            if (!_inZone)
            {
                DecreaseProgress();
                DecreaseProgressBar();
            }
            if(_canLeaveBed)
                _isBusy = false;
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out CameraViewChanger player))
            {
                _inZone = true;
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
                    _timeToHeal = 0;
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out CameraViewChanger player))
            {
                EventsManager.Instance.OnExitBedTriggerZoneEvent();
                _inZone = false;
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