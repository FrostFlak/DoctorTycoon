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
        [SerializeField] private bool _leftQueue;
        #endregion

        #region Private Fields

        #endregion

        #region Properties
        public bool IsLaying { get { return _isLaying; } set { _isLaying = value; } }
        public bool LeftQueue { get { return _leftQueue; } set { _leftQueue = value; } }
        #endregion




    }

}

