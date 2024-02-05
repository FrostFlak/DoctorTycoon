using System.Collections.Generic;
using UnityEngine;

namespace People
{
    public class BedManager : MonoBehaviour
    {
        [SerializeField] private List<Bed> _beds = new List<Bed>();
        private Vector3 _freeBed;

        public Vector3 FreeBed { get { return _freeBed; } set { _freeBed = value; } }
        public List<Bed> Beds {  get { return _beds; } }

        private void Start()
        {

        }
 
}

}
