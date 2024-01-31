using People;
using UnityEngine;

namespace People
{
    public class Bed : MonoBehaviour
{
        [SerializeField] private bool _isBusy;
        [SerializeField] private bool _isPurchased;
        public bool IsBusy { get { return _isBusy; } set { _isBusy = value; } }
        public bool IsPurchased { get { return _isPurchased; } set { _isPurchased = value; } }

        private void OnTriggerEnter(Collider other)
        {
            _isBusy = true;
            if(other.TryGetComponent(out Human human))
            {
                human.StartLayAnimation();
            }
            //start Animation event
        }
}

}