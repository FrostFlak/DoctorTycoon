using UnityEngine;
using UnityEngine.AI;
namespace People
{

[RequireComponent(typeof(CapsuleCollider) , typeof(Rigidbody) , typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator) , typeof(HumanAnimationController) , typeof(HumanQueueController))]
[RequireComponent(typeof(HumanBedController) , typeof(BoxCollider))]
    public abstract class Human : MonoBehaviour
    {
        #region SerializedFields
        [SerializeField] private bool _isLaying;
        [SerializeField] private bool _isInQueue;
        [SerializeField] private bool _leftBed;
        [SerializeField] private bool _isGoingToBed;
        [SerializeField] private bool _leftHospital;
        [SerializeField] private HumanBedController _bedController;
        [SerializeField] private HumanQueueController _queueController;

        #endregion


        #region Properties
        public bool IsLaying { get { return _isLaying; } set { _isLaying = value; } }
        public bool IsInQueue { get { return _isInQueue; } set { _isInQueue = value; } }
        public bool LeftBed { get { return _leftBed; } set { _leftBed = value; } }
        public bool IsGoingToBed { get { return _isGoingToBed; } set { _isGoingToBed = value; } }
        public bool LeftHospital { get {  return _leftHospital; } set { _leftBed = value; } }
        #endregion

        public void OnHumanEndHealing()
        {
            _isLaying = false;
            _isInQueue = false;
            _leftBed = false;
            _isGoingToBed = false;
            _leftHospital = false;
            _bedController.enabled = false;
            _queueController.enabled = true;
        }

        public void EnterInQueue(RegistrationTable registrationTable , BedManager bedManager)
        {
            _queueController.StartCoroutine(_queueController.EnterInQueue(registrationTable , bedManager));
        }

    }

}

