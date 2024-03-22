using System.Collections.Generic;
using UnityEngine;

namespace People
{

    public enum WaypointsPositions
    {
        LeftDownRoom = 0,
        RightDownRoom = 1,
        LeftUpRoom = 2,
        RightUpRoom = 3,
        MiddleDoor = 4,
        QuitHospital = 5,
        EnterHospitalPoint_1 = 6 ,
        EnterHospitalPoint_2 = 7 ,
        EnterHospitalPoint_3 = 8 ,
        EnterHospitalPoint_4 = 9 ,
        EnterHospitalPoint_5 = 10 ,

    }
    public class BedManager : MonoBehaviour
    {
        [SerializeField] private List<Bed> _beds = new List<Bed>();
        [SerializeField] private List<Transform> _waypointPositions;
        private bool _isAvailableBeds;

        private Vector3 _freeBed;
        public Vector3 FreeBed { get { return _freeBed; } set { _freeBed = value; } }
        public List<Transform> WaypointPosition { get { return _waypointPositions; } }

        public bool IsAvailableBeds {  get { return _isAvailableBeds; } }
        public List<Bed> Beds {  get { return _beds; } }

        public bool IsAnyBedAvailable()
        {
            foreach (Bed bed in _beds)
            {
                if (!bed.IsBusy && bed.IsPurchased) return true;
            }
            return false;
        }
    }

}
