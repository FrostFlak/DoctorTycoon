using UnityEngine;
using Cinemachine;


namespace Player
{
    public class CameraType : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera[] _cameras;
        [SerializeField] private int _currentCameraIndex;
        [SerializeField] private bool _canChange;
        [SerializeField] private float _timeInterval = 2;


        public int CurrentCameraIndex { get { return _currentCameraIndex; } private set { } }

        private void Start()
        {
            _canChange = true;
        }
        private void Update()
        {
            if (Input.GetKey(KeyCode.C) && _canChange)
            {
                ChangeCameraPriority();
                _canChange = false;
            }
            else
                _timeInterval -= Time.deltaTime;
            if(_timeInterval <= 0 && !_canChange)
            {
                _canChange = true;
                _timeInterval = 2;
            }
            else if(_timeInterval < 0)
            {
                _timeInterval = 2;
            }
        }


        private void ChangeCameraPriority()
        {
            _cameras[_currentCameraIndex].Priority = 0;
            _currentCameraIndex++;
            if (_currentCameraIndex >= _cameras.Length) _currentCameraIndex = 0;
            _cameras[_currentCameraIndex].Priority = 1;  
        }
    }

}
