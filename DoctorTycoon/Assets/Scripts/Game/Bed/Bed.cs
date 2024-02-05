using Player;
using UnityEngine;

namespace People
{
    public class Bed : MonoBehaviour
{
        [SerializeField] private bool _isBusy;
        [SerializeField] private bool _isPurchased;
        [SerializeField] private float _timeToHeal;
        [SerializeField] private float _maxTimeToHeal;
        [SerializeField] private bool _inZone;
        public bool IsBusy { get { return _isBusy; } set { _isBusy = value; } }
        public bool IsPurchased { get { return _isPurchased; } set { _isPurchased = value; } }

        private void Update()
        {
            if (!_inZone) 
                DecreaseProgress();
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out Human human))
            {
                _isBusy = true;
                human.IsLaying = true;
            }
            else if(other.TryGetComponent(out MovementType character))
                _inZone = true;
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out MovementType character))
            {
                _timeToHeal += Time.deltaTime;
                if (_timeToHeal >= _maxTimeToHeal)
                {
                    EventsManager.Instance.OnTimerToPeopleLayEndEvent();
                    _timeToHeal = 0;
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out MovementType character))
                _inZone = false;
            else if(other.TryGetComponent(out Human human))
                human.IsLaying = false;
        }
        private void DecreaseProgress()
        {
            _timeToHeal -= Time.deltaTime;
            if (_timeToHeal < 0)
            {
                _timeToHeal = 0;
            }
        }
    }

}