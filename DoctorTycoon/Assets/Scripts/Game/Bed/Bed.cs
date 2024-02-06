﻿using Player;
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
        [SerializeField] private bool _canLeaveBed;
        public bool IsBusy { get { return _isBusy; } set { _isBusy = value; } }
        public bool IsPurchased { get { return _isPurchased; } set { _isPurchased = value; } }
        public bool CanLeaveBed { get { return _canLeaveBed; } set { _canLeaveBed = value; } }

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
            if (other.TryGetComponent(out MovementType character) && _isBusy)
            {
                _timeToHeal += Time.deltaTime;
                if (_timeToHeal >= _maxTimeToHeal)
                {
                    _canLeaveBed = true;
                    EventsManager.Instance.OnTimerToPeopleLayEndEvent();
                    _timeToHeal = 0;
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out MovementType character))
                _inZone = false;
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