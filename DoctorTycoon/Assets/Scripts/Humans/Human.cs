using UnityEngine;
using UnityEngine.AI;
namespace People
{

[RequireComponent(typeof(CapsuleCollider) , typeof(Rigidbody) , typeof(NavMeshAgent))]
[RequireComponent(typeof(Animator) , typeof(HumanAnimationController) , typeof(HumanQueueController))]
[RequireComponent(typeof(HumanBedController))]
    public abstract class Human : MonoBehaviour
    {
        #region SerializedFields
        [SerializeField] private bool _isLaying;
        [SerializeField] private bool _isInQueue;
        [SerializeField] private bool _leftBed;
        [SerializeField] private bool _isGoingToBed;
        #endregion

        #region Private Fields

        #endregion

        #region Properties
        public bool IsLaying { get { return _isLaying; } set { _isLaying = value; } }
        public bool IsInQueue { get { return _isInQueue; } set { _isInQueue = value; } }
        public bool LeftBed { get { return _leftBed; } set { _leftBed = value; } }
        public bool IsGoingToBed { get { return _isGoingToBed; } set { _isGoingToBed = value; } }
        #endregion



    }

}

