using System.Collections.Generic;
using UnityEngine;

namespace People
{
    public class BedManager : MonoBehaviour
    {
        [SerializeField] private List<Bed> _beds = new List<Bed>();
        [SerializeField] private Vector3 _quitPosition;
        [SerializeField] private bool _isAvailableBeds;
        private Vector3 _freeBed;

        public Vector3 FreeBed { get { return _freeBed; } set { _freeBed = value; } }
        public Vector3 QuitPosition { get { return _quitPosition; } }
        public bool IsAvailableBeds {  get { return _isAvailableBeds; } }
        public List<Bed> Beds {  get { return _beds; } }

        public bool IsAnyBedAvailable()
        {
            foreach (Bed bed in _beds)
            {
                if (!bed.IsBusy) return true;
            }
            return false;
        }
    }

}
